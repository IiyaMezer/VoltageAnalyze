using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoltageDataHandler.Interfaces;

namespace VoltageDataHandler.FileHandlers;

public class CsvFileHandler : IFileHandler
{
    public List<string> ReadFiles(string filePath)
    {
        throw new NotImplementedException();
    }

    public void WriteFiles(string filePath, List<string> files)
    {
        throw new NotImplementedException();
    }
}
