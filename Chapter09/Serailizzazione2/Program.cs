using System.Text.Json; // JSON serializer
using System.Text.Json.Serialization; // [JsonInclude]
using static System.Console;
using static System.Environment;
using static System.IO.Path;


Book csharp10 = new(title: "C# 10 and .NET 6 - Modern Cross-platform Development")
{
    Author = "Mark J Price",
    PublishDate = new(2021, 11, 9),
    Pages = 823,
    Created = DateTimeOffset.UtcNow,
};

JsonSerializerOptions options = new()
{
    IncludeFields = true, //include all fields
    PropertyNameCaseInsensitive = true,
    WriteIndented = true,
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase

};

string file = Combine(CurrentDirectory,"book.json");

using (Stream filestream = File.Create(file))
{
    JsonSerializer.Serialize<Book>(utf8Json: filestream, value:csharp10, options:options);
}

WriteLine($"Written {new FileInfo(file).Length} bytes of JSON to {file}");
WriteLine();

// display the serialized object graph
WriteLine(File.ReadAllText(file));

public class Book
{

    //costruttore per settare una proprietà non-nullable
    public Book(string title)
    {
        Title = title;
    }

    //properties
    public string Title { get; set; }
    public string Author { get; set; }

    //fields
    [JsonInclude]   //include questo campo
    public DateTime PublishDate;

    [JsonInclude]   //include questo campo
    public DateTimeOffset Created;

    public ushort Pages;

}

