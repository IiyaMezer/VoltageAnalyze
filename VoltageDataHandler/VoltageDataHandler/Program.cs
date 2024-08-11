using System.Diagnostics;
using System.Security.AccessControl;
using VoltageDataHandler.DataHandlers;
using VoltageDataHandler.FileHandlers;
using VoltageDataHandler.Models;

namespace VoltageDataHandler
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var handler = new TxtFileHandler();

            Console.WriteLine("Введите полный путь файла:");
            string filepath = Console.ReadLine();
            var data = handler.ReadFiles(filepath);

            var headlessData = DataHandler.DeleteHeader(data);
            
            var subtables = DataHandler.ParseData(headlessData);

            var finalData = DataHandler.CalculateAverages(subtables);

            CsvFileHandler csvFileHandler = new CsvFileHandler();

            string trimmedPath = filepath.Substring(0, filepath.Length - 3);
            string newPath = trimmedPath + "csv";


            csvFileHandler.WriteFiles(newPath, finalData);


            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed.TotalMilliseconds);
            Console.ReadLine();
        }
    }
}
