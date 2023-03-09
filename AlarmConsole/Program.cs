// See https://aka.ms/new-console-template for more information
using AlarmConsole;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Xml.Linq;

string seturl = (Environment.GetCommandLineArgs().Count() > 1)? Environment.GetCommandLineArgs()[1]:"";

if (seturl != "seturl")
{
    string url = new UrlService().GetURl();

    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
    {
        url = url.Replace("&", "^&");
        Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
        Console.WriteLine($"Alarm Console started at url {url}");

    }
    return;
}

string c = "";

do
{
    int count = 0;
    foreach (var item in new UrlService().GetAllURLs())
    {
        Console.WriteLine($"Id: {++count} name: {item.Key} url: {item.Value}");
    }

    Console.WriteLine("");
    Console.WriteLine("Press 1 to Add url.");
    Console.WriteLine("Press 2 to Remove url.");
    Console.WriteLine("Press 3 Open url.");
    int opt;
    int.TryParse(Console.ReadLine(),out opt);

    if (opt == 1)
    {
        string name, url;
        Console.WriteLine("Enter Name");
        name = Console.ReadLine();
        Console.WriteLine("Enter URL");
        url = Console.ReadLine();

        await new UrlService().AddNewURL(name, url);

    }
    else if (opt ==2)
    {
        int id=0,delId=0;
        Console.WriteLine("Enter Id to delete");
        int.TryParse(Console.ReadLine(),out id);

        foreach (var item in new UrlService().GetAllURLs())
        {
            if (++delId== id)
            {
                await new UrlService().DelOneURL(item.Key,item.Value);
            }
        }
    }
    else if (opt ==3)
    {
        int id = 0;
        Console.WriteLine("Enter Id to open.");
        int.TryParse(Console.ReadLine(), out id);

        string url = new UrlService().GetURl(id);

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            url = url.Replace("&", "^&");
            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            Console.WriteLine($"Alarm Console started at url {url}");

        }
    }

    Console.WriteLine("Start over?");
    c = Console.ReadLine();
    Console.Clear();
} while (c == "y");