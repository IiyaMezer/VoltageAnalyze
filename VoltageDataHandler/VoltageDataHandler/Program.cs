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
                Console.WriteLine("Введите полный путь к папке (или 'Exit' для выхода):");
                string directoryPath = Console.ReadLine();

                if (string.Equals(directoryPath, "Exit", StringComparison.OrdinalIgnoreCase))
                {
                    break; // Выход из цикла
                }

                // Удаление кавычек в начале и в конце
                directoryPath = directoryPath?.Trim('\"');

                // Проверка существования папки
                if (!Directory.Exists(directoryPath))
                {
                    Console.WriteLine("Папка не найдена. Проверьте путь.");
                    continue; // Возврат в начало цикла
                }

                var stopwatch = new Stopwatch();
                stopwatch.Start();

                // Получение всех текстовых файлов в папке и подпапках
                string[] files = Directory.GetFiles(directoryPath, "*.txt", SearchOption.AllDirectories);

                foreach (var filepath in files)
                {
                    var data = handler.ReadFiles(filepath);
                    Console.WriteLine($"Считано {data.Count} строк из файла: {Path.GetFileName(filepath)}.");

                    var headlessData = DataHandler.DeleteHeader(data);
                    var subtables = DataHandler.ParseData(headlessData);
                    Console.WriteLine($"Обработано {subtables.Count} подтаблиц из файла: {Path.GetFileName(filepath)}.");

                    var finalData = DataHandler.CalculateAverages(subtables);

                    CsvFileHandler csvFileHandler = new CsvFileHandler();

                    // Изменение расширения файла
                    string newPath = Path.ChangeExtension(filepath, ".csv");

                    csvFileHandler.WriteFiles(newPath, finalData);
                    Console.WriteLine($"Данные записаны в {newPath}.");
                }

                stopwatch.Stop();
                Console.WriteLine($"Время выполнения: {stopwatch.Elapsed.TotalMilliseconds} ms");
            }
        }
    }
}
