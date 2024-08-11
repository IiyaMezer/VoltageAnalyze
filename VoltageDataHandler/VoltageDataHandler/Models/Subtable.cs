using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoltageDataHandler.Models;

public class Subtable
{
    public string Name { get; set; }
    public List<Measurement> Measurements { get; set; } = new List<Measurement>();


    public Subtable(string Name) 
    {
        this.Name = Name;
    }
}
