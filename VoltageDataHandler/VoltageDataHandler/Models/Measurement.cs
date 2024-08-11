using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoltageDataHandler.Models;

public class Measurement(int sec, double[] data)
{
    public int Sec { get; set; } = sec;
    public double[] Data { get; set; } = data;
}