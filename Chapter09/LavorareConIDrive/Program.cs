using static System.Console;
using static System.IO.Directory;
using static System.IO.Path;
using static System.Environment;

WriteLine("{0, -30} | {1, -10} | {2, -7} | {3, 18} | {4, 18}",  "Name","TYPE","FORMAT","SIZE (BYTES)","FREE SPACE");

foreach (DriveInfo disco in DriveInfo.GetDrives())
{
    if (disco.IsReady)
    {
        WriteLine("{0, -30} | {1, -10} | {2, -7} | {3, 18} | {4, 18}",
            disco.Name,
            disco.DriveType,
            disco.DriveFormat,
            disco.TotalSize,
            disco.AvailableFreeSpace);
    }
    else 
    {
        WriteLine("{0, -30} | {1, -10}",
            disco.Name,
            disco.DriveType);
           
    }
}