using System.Diagnostics;
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
            var data = handler.ReadFiles("C:/Users/carna/source/repos/IiyaMezer/VoltageAnalyze/VoltageDataHandler/ZAP0138.TXT");

            var headlessData = DataHandler.DeleteHeader(data);
            
            var subtables = DataHandler.ParseData(headlessData);

            var finalData = DataHandler.CalculateAverages(subtables);


            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed.TotalMilliseconds);
            Console.ReadLine();
        }
    }
}
