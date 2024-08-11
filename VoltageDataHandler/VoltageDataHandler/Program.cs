using System.Diagnostics;
using VoltageDataHandler.DataHandlers;
using VoltageDataHandler.FileHandlers;

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

            var dataHandler = new DataHandler(); 

            data = dataHandler.DeleteHeader(data);

            stopwatch.Stop();
            Console.WriteLine(stopwatch.Elapsed.TotalMilliseconds);
            Console.ReadLine();
        }
    }
}
