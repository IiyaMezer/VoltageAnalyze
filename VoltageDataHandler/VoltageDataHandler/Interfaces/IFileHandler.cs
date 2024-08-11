using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoltageDataHandler.Models;

namespace VoltageDataHandler.Interfaces
{
    internal interface IFileHandler
    {
        List<string> ReadFiles(string filePath);
        void WriteFiles(string filePath, List<AvgSubtable> data);
    }
}
