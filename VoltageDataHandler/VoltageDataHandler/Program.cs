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
            var handler = new TxtFileHandler();

            while (true)
            {
                Console.WriteLine("Введите полный путь файла (или 'Exit' для выхода):");
                string filepath = Console.ReadLine();

                if (string.Equals(filepath, "Exit", StringComparison.OrdinalIgnoreCase))
                {
                    break; // Выход из цикла
                }

                // Удаление кавычек в начале и в конце
                filepath = filepath?.Trim('\"');

                // Проверка существования файла
                if (!File.Exists(filepath))
                {
                    Console.WriteLine("Файл не найден. Проверьте путь.");
                    continue; // Возврат в начало цикла
                }

                var stopwatch = new Stopwatch();
                stopwatch.Start();

                var data = handler.ReadFiles(filepath);
                Console.WriteLine($"Считано {data.Count} строк из файла.");

                var headlessData = DataHandler.DeleteHeader(data);
                var subtables = DataHandler.ParseData(headlessData);
                Console.WriteLine($"Обработано {subtables.Count} подтаблиц.");

                var finalData = DataHandler.CalculateAverages(subtables);

                CsvFileHandler csvFileHandler = new CsvFileHandler();

                // Изменение расширения файла
                string newPath = Path.ChangeExtension(filepath, ".csv");

                csvFileHandler.WriteFiles(newPath, finalData);
                Console.WriteLine($"Данные записаны в {newPath}.");

                stopwatch.Stop();
                Console.WriteLine($"Время выполнения: {stopwatch.Elapsed.TotalMilliseconds} ms");
            }
        }
    }
}
