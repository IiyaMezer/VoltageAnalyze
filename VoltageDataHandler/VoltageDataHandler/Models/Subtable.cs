using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoltageDataHandler.Models;

public class Subtable(string Name)
{
    public string Name { get; set; } = Name;
    public List<Measurement> Measurements { get; set; } = [];
}
