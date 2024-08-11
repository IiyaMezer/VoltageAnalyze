using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoltageDataHandler.Models;

internal class Measurement
{


    public int Sec {  get; set; }
    public double[] Data { get; set; }

    public Measurement(int sec, double[] data)
    {
        Sec = sec;
        Data = data;
    }
}