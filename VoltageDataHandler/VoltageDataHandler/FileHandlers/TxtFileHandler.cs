using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoltageDataHandler.Interfaces;
using VoltageDataHandler.Models;

namespace VoltageDataHandler.FileHandlers
{
    internal class TxtFileHandler : IFileHandler
    {
        public List<string> ReadFiles(string filePath)
        {
            var data = new List<string>();

            foreach (var line in File.ReadLines(filePath))
            {
                data.Add(line);
            }
            return data;
        }

        public void WriteFiles(string filePath, List<AvgSubtable> data)
        {
            throw new NotImplementedException();
        }
    }
}
