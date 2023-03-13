using static System.Console;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

string imagesFolder = Path.Combine(Environment.CurrentDirectory, "Immagini");

IEnumerable<string> images = Directory.EnumerateFiles(imagesFolder);

foreach (string imagePath in images)
{ 
    string thumbnailPath = Path.Combine(imagesFolder, Path.GetFileNameWithoutExtension(imagePath) +"-thumbNail"+Path.GetExtension(imagePath));

    using (Image image = Image.Load(imagePath))
    {
        image.Mutate(x => x.Resize(image.Width / 10, image.Height/10));
        image.Mutate(x => x.Grayscale());
        image.Save(thumbnailPath);

    }
}

WriteLine("Images processing complete. View the images folder.");


