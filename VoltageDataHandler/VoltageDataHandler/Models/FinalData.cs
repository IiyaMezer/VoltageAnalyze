using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoltageDataHandler.Models;

public class FinalData
{
    public List<AvgSubtable> Subtables { get; set; } = new List<AvgSubtable>();
}
