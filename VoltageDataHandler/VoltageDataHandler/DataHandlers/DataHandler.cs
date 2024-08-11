using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoltageDataHandler.Models;

namespace VoltageDataHandler.DataHandlers;

public static class DataHandler
{

    public static Func<List<string>, List<string>> DeleteHeader = data => data.Skip(4).ToList();

    public static List<Subtable> ParseData(List<string> rawData)
    {
        List<Subtable> subtables = [];

        Subtable currentSubtable = null;

        foreach (var line in rawData) 
        {
            if (string.IsNullOrEmpty(line))
            {
                if (currentSubtable != null)
                {
                    subtables.Add(currentSubtable);
                    currentSubtable = null;
                }
            }
            else if (line.Contains(':')) 
            {
                currentSubtable = new Subtable(line.Trim());
            }
            else
            {
                var parts = line.Split('/');
                int sec = int.Parse(parts[0]);
                double[] voltage = parts.Skip(1).Select(double.Parse).ToArray();

                if (currentSubtable != null)
                {
                    currentSubtable.Measurements.Add(new Measurement(sec, voltage));
                }
            }
        }

        if (currentSubtable != null)
        {
            subtables.Add(currentSubtable);
        }

        return subtables;
    }


}
