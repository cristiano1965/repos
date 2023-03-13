using static System.Console;
using static System.IO.Directory;
using static System.IO.Path;
using static System.Environment;

//WorkWithDirectories();
WorkWithFiles();

static void WorkWithDirectories()
{
    //definisce una directory path per un nuovo folder 
    //che parte dal quello dell'utente
    string newFolder = Combine(
        GetFolderPath(SpecialFolder.Personal),
        "Code","Chapter09","NewFolder"
        );
    WriteLine($"Working with: {newFolder}");

    if (Exists(newFolder))  
        Delete(newFolder, recursive: true); // la  cancella

    //check if it exists
    WriteLine($"Does it exist ? {Exists(newFolder)}");
    CreateDirectory(newFolder);
    WriteLine($"L'ho creata; Does it exist ? {Exists(newFolder)}");
    Write("Confirm the directory exists, and press ENTER: ");
    ReadLine();


    //delete directory
    WriteLine("Deleting it...");
    Delete(newFolder, recursive: true); // la  cancella
    WriteLine($"L'ho cancellata; Does it exist ? {Exists(newFolder)}");

}


static void WorkWithFiles()
{
    //definisce una directory path per un nuovo folder in cui scrivere file
    //che parte dal quello dell'utente
    string dir = Combine(
        GetFolderPath(SpecialFolder.Personal),
        "Code", "Chapter09", "OutPutFiles"
        );
    
    CreateDirectory(dir);

    //define file paths 
    string textFile = Combine(dir,"Dummy.txt");
    string backupFile = Combine(dir, "Dummy.bak");
    WriteLine($"Working with: {textFile}");

    //check if a file exists
    WriteLine($"Does it exist ? {File.Exists(textFile)}");

    //crea i file e scrivici qualcosa
    StreamWriter textWriter = File.CreateText(textFile);
    textWriter.WriteLine("Hello, C#!");
    textWriter.Close(); // close file and release resources
    WriteLine($"Does it exist ? {File.Exists(textFile)}");

    //copy the file and overwrite if it already exists
    File.Copy(textFile, backupFile, true);
    WriteLine($"Does {backupFile} exist ? {File.Exists(backupFile)}");
    Write("Confirm the backup file exists, and press ENTER: ");
    ReadLine();

    //delete file
    File.Delete(textFile);
    WriteLine($"Does it exist ? {File.Exists(textFile)}");

    //read form text backup file
    WriteLine($"Reading contents of {backupFile}");
    StreamReader textReader = File.OpenText(backupFile);
    WriteLine(textReader.ReadToEnd());
    textReader.Close();

    //gestione del path
    WriteLine($"Folder Name: {GetDirectoryName(textFile)}");
    WriteLine($"File Name: {GetFileName(textFile)}");
    WriteLine($"File Name without extension: {GetFileNameWithoutExtension(textFile)}");
    WriteLine($"File Name extension: {GetExtension(textFile)}");
    WriteLine($"Random File: {GetRandomFileName()}"); //ritorna un nome file casuale ma non lo crea
    WriteLine($"Temporary file name: {GetTempFileName()}"); //il file viene creato vuoto
    WriteLine($"Temporary path: {GetTempPath()}");

    if (File.Exists(backupFile))
    {
        FileInfo infobk = new(backupFile);
        WriteLine($"{backupFile}:");
        WriteLine($"Contains {infobk.Length} bytes");
        WriteLine($"Data ultimo accesso {infobk.LastAccessTime}");
        WriteLine($"Readonly impostato a {infobk.IsReadOnly}");



    }


}