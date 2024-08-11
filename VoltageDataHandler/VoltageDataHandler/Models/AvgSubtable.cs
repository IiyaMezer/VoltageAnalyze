using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoltageDataHandler.Models;

public class AvgSubtable(string name, double[] data)
{
    public string Name { get; set; } = name;
    public double[] Data { get; set; } = data;
}
