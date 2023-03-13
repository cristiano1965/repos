using Microsoft.AspNetCore.Mvc;
using Northwind.mvc.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Packt.Shared; // AddNorthwindContext estension method
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace Northwind.mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NorthwindContext db;
        private readonly IHttpClientFactory clientFactory;

        public HomeController(ILogger<HomeController> logger, NorthwindContext injectedContext, IHttpClientFactory HttpClientFactory)
        {
            _logger = logger;
            db = injectedContext;
            clientFactory = HttpClientFactory;
        }

        [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any)]
        public IActionResult Index(bool VisitorCount)
        {
            /*
            _logger.LogError("This is a serious error (not really!)");
            _logger.LogWarning("This is your first warning!");
            _logger.LogWarning("Second warning");
            _logger.LogInformation("I am in the index method of the Home controller)");
            */

            HomeIndexViewModel model = new
            (
                VisitorCount: (new Random()).Next(1, 1001),
                Categories: db.Categories.ToList(),
                Products: db.Products.ToList()
            );
            return View(model);
        }
        [Route("private")]
        [Authorize(Roles = "Administrators")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult ProductDetail(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("You must pass a product ID in the route, for example /Home/ProductDetail/21");
            }
            /* join every product to its category and return 77 matches (questa genera una inner join tra Products e Categories)
            var model =
                db.Products.Join(
                inner: db.Categories,
                outerKeySelector: product => product.CategoryId,
                innerKeySelector: category => category.CategoryId,
                resultSelector: (p, c) =>
                    new { c.CategoryId, c.CategoryName, p.ProductName, p.ProductId, p.UnitPrice, p.UnitsInStock })
                .SingleOrDefault(p => p.ProductId == id);
            */

            Product? model = db.Products.Include(p => p.Category).SingleOrDefault(p => p.ProductId == id);

            if (model is null)
            {
                return NotFound($"Product with ID={id} was not found");
            }
            //model.Category = db.Categories.SingleOrDefault(c=>c.CategoryId == model.CategoryId);

            return View(model); // pass model to view and return result
        }

        public IActionResult ModelBinding()
        {
            return View(); // the page with the form to submit
        }

        [HttpPost]
        public IActionResult ModelBinding(Thing thing)
        {
            HomeModelBindingViewModel model = new(
                thing,
                !ModelState.IsValid,
                ModelState.Values
                .SelectMany(state => state.Errors)
                .Select(error => error.ErrorMessage)
                );
            return View(model); // show the model bound thing
        }
        public IActionResult ProductsThatCostMoreThan(decimal? price)
        {

            if (!price.HasValue)
            {
                return BadRequest("You must pass a product price in the query string, for example: /Home/ProductsThatCostMoreThan?price=50");
            }
            IEnumerable<Product> model = db.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .Where(p => p.UnitPrice > price);

            if (!model.Any())// Any() = se ci sono elementi
            {
                return NotFound($"Nessun prodotto costa più di {price:C}");
            }

            ViewData["MaxPrice"] = price.Value.ToString("C");
            return View(model); // pass model to view
        }

        public async Task<IActionResult> Customers(string country)
        {
            string uri;

            if (string.IsNullOrEmpty(country))
            {
                ViewData["Title"] = "All Customers worldwide";
                uri = "api/customers/";
            }
            else
            {
                ViewData["Title"] = "All Customers in " + country;
                uri = $"api/customers/?country={country}";
            }

            HttpClient client = clientFactory.CreateClient(name: "Northwind.Webapi");

            HttpRequestMessage request = new HttpRequestMessage(method: HttpMethod.Get, requestUri: uri);

            HttpResponseMessage response = await client.SendAsync(request);

            IEnumerable<Customer>? model = await response.Content.ReadFromJsonAsync<IEnumerable<Customer>>();

            return View(model);


        }

        public async Task<IActionResult> Services()
        {
            try
            {
                string uri = "catalog/products/?$filter=startswith(ProductName, 'Cha')&$select=ProductId,ProductName,UnitPrice";
                HttpClient client = clientFactory.CreateClient(name: "Northwind.OData");

                HttpRequestMessage request = new HttpRequestMessage(method: HttpMethod.Get, requestUri: uri);

                HttpResponseMessage response = await client.SendAsync(request);

                Product[]? model = (await response.Content.ReadFromJsonAsync<ODataProducts>())?.Value; //prendiamo cosa viene restituito dal JSON nell'attributo VALUE

                ViewData["productsCha"] = model;
            }
            catch (Exception ex)
            {
                _logger.LogWarning($"Northwind.OData service exception: {ex.Message}");
            }

            return View();
        }
    }

}