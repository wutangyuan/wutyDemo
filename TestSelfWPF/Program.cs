// See https://aka.ms/new-console-template for more information

using System.Windows;

class Program
{

    [STAThread]
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        Application app = new Application();

        var window = new Window();
        window.Title = "Test";

        app.Run(window);
        Console.ReadKey();
    }
}


