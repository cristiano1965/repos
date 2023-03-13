using static System.Console;
using static System.IO.Path;
using static System.Environment;
using System.Xml;
using System.IO.Compression; // BrotliStream, GZipStream, CompressionMode

Console.OutputEncoding = System.Text.Encoding.UTF8;

//WorkWithText();
//WorkWithXml();
//WorkWithCompression(); //usa GZIP
WorkWithCompression(true); //usa BROTLI

static void WorkWithText()
{
    //define a file to write to
    string textFile = Combine(CurrentDirectory,"streams.txt");

    // create a text file and return a helper writer
    StreamWriter text = File.CreateText(textFile);

    //enumerate the strings, writing each one to the stream on a separate line
    foreach (string item in Viper.CallSigns)
    { 
        text.WriteLine(item);
    }
    text.Close(); //release resources

    //output the contents of the file 
    WriteLine($"{textFile} contains {new FileInfo(textFile).Length} bytes.");
    WriteLine(File.ReadAllText(textFile));


}


static void WorkWithXml()
{
    FileStream? xmlFileStream = null;
    XmlWriter? xml = null;
    try
    {
        //define a file to write to
        string xmlFile = Combine(CurrentDirectory, "streams.xml");

        //create a file stream
        xmlFileStream = File.Create(xmlFile);

        //wrap the file stream  in an XML writer helper
        //and automatically indent nested elements
        xml = XmlWriter.Create(xmlFileStream, new XmlWriterSettings { Indent = true });

        //write the xml declaration
        xml.WriteStartDocument();

        //write a root element
        xml.WriteStartElement("callsigns");

        //write every string to the stream
        foreach (string item in Viper.CallSigns)
        {
            xml.WriteElementString("nome_del_viper", item);
        }

        //write a close root element
        xml.WriteEndElement();

        //close helper and stream
        xml.Close();
        xmlFileStream.Close();

        //output the contents of the file
        WriteLine($"{xmlFile} contains {new FileInfo(xmlFile).Length} bytes.");
        WriteLine(File.ReadAllText(xmlFile));
    }
    catch (Exception ex)
    {
        //if the path doesn't exist the exception willbe caught
        WriteLine($"{ex.GetType()} says {ex.Message}");
    }
    finally 
    {
        if (xml is not null) //buona pratica di controllare che l'oggetto non sia null prima di farne il Dispose()
        {
            xml.Dispose();
            WriteLine("The XML writer's unmanaged resources have been dispopsed.");

            if (xmlFileStream is not null)
            {
                xmlFileStream.Dispose();
                WriteLine("The file stream's unmanaged resources have been dispopsed.");
            }
        }
    }



}

static void WorkWithCompression(bool useBrotli = false) // parametro opzionale per usare il compressore brotli, più efficiente del 20% rispetto a GZIP
{

    string fileExt = useBrotli == true ? "brotli" : "gzip";

    //compress the xml output
    string filePath = Combine(CurrentDirectory, "streams." + fileExt);

    FileStream file = File.Create(filePath);


    Stream compressor;

    if (useBrotli)
        compressor = new BrotliStream(file, CompressionMode.Compress);
    else
        compressor = new GZipStream(file, CompressionMode.Compress);

    using (compressor)  // lo using fa automaticamente il dispose della risorsa
    {
        using (XmlWriter xml = XmlWriter.Create(compressor, new XmlWriterSettings { Indent = true/*, Encoding= System.Text.Encoding.UTF8* è già dei default UTF8/ }))
        {
            //write the xml declaration
            xml.WriteStartDocument();

            //write a root element
            xml.WriteStartElement("callsigns");

            //write every string to the stream
            foreach (string item in Viper.CallSigns2)
            {
                xml.WriteElementString("nome_del_viper", item);
            }
            // la normale call WriteEndElement non è necessaria
            // perchè quando XmlWriter disposes, automaticamente chiuderà ogni elemento di ogni profondità
        }
    } // chiude anche lo stream sottotante

    //output all the contents of the compressed file (chiaramentre è compresso quindi si vedranno tutti caratteri esadecimali)
    WriteLine($"{filePath} contains {new FileInfo(filePath).Length} bytes.");
    WriteLine("The compressed contents:");
    WriteLine(File.ReadAllText(filePath));

    WriteLine("The compressed XML file:");
    file = File.Open(filePath, FileMode.Open);

    Stream decompressor;

    if (useBrotli)
        decompressor = new BrotliStream(file, CompressionMode.Decompress);
    else
        decompressor = new GZipStream(file, CompressionMode.Decompress);

    using (decompressor)  // lo using fa automaticamente il dispose della risorsa
    {
        using (XmlReader reader = XmlReader.Create(decompressor))
        {
            while (reader.Read())  // read the next XML node
            {
                //check if we are on an element node named "nome_del_viper"
                if (reader.NodeType == XmlNodeType.Element && reader.Name == "nome_del_viper")
                {
                    reader.Read(); //ora legge il testo dentro l'elemento "nome_del_viper"
                    WriteLine(reader.Value); //visualizza il valore letto
                    
                }
            }
        }

    }
}
static class Viper
{
    // define an array ofViper pilot call signs
    public static string[] CallSigns = new[]
    {
        "Husker", "Starbuck","Apollo","Boomer",
        "Bulldog","Athena","Helo","Racetrack"
    };
    public static string[] CallSigns2 = new[]
    {
        "Huskerà", "Starbuck","Apollo","Boomer",
        "Bulldog","Athena","Helo","Racetrack€"
    };
}
