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
                Console.WriteLine("Введите полный путь к файлу или папке (или 'Exit' для выхода):");
                string inputPath = Console.ReadLine();

                if (string.Equals(inputPath, "Exit", StringComparison.OrdinalIgnoreCase))
                {
                    break; // Выход из цикла
                }

                // Удаление кавычек в начале и в конце
                inputPath = inputPath?.Trim('\"');

                // Проверка существования файла или папки
                if (File.Exists(inputPath))
                {
                    // Обработка одного файла
                    ProcessFile(handler, inputPath);
                }
                else if (Directory.Exists(inputPath))
                {
                    // Обработка всех файлов в папке и подпапках
                    var stopwatch = new Stopwatch();
                    stopwatch.Start();

                    string[] files = Directory.GetFiles(inputPath, "*.txt", SearchOption.AllDirectories);

                    foreach (var filepath in files)
                    {
                        ProcessFile(handler, filepath);
                    }

                    stopwatch.Stop();
                    Console.WriteLine($"Время выполнения: {stopwatch.Elapsed.TotalMilliseconds} ms");
                }
                else
                {
                    Console.WriteLine("Файл или папка не найдены. Проверьте путь.");
                }
            }
        }

        private static void ProcessFile(TxtFileHandler handler, string filepath)
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
    }
}

