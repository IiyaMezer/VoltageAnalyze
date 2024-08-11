using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoltageDataHandler.Interfaces;
using VoltageDataHandler.Models;

namespace VoltageDataHandler.FileHandlers;

public class CsvFileHandler : IFileHandler
{
    public List<string> ReadFiles(string filePath)
    {
        throw new NotImplementedException();
    }

    public void WriteFiles(string filePath, List<AvgSubtable> data)
    {
        StringBuilder sb = new StringBuilder();

        if (data.Count == 0)
        {
            throw new ArgumentException("Список подтаблиц не может быть пустым", nameof(data));
        }

        StringBuilder headers = new StringBuilder();
        headers.Append("Min");
        headers.Append(';');
        headers.Append(string.Join(";", Enumerable.Range(1, data[0].Data.Length).Select(i => $"Data{i}")));
        sb.AppendLine(headers.ToString());

        foreach (AvgSubtable subtable in data)
        {
            var line = new StringBuilder();

            line.Append(subtable.Name);
            line.Append(";");
            line.AppendLine(string.Join(";" , subtable.Data.Select(d => d.ToString("F3"))));
            sb.Append(line.ToString());
        }

        File.WriteAllText(filePath, sb.ToString());
    }
}
