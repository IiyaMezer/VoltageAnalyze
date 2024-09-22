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
                double[] voltage = parts.Skip(1)
                     .Select(s => double.Parse(s, System.Globalization.CultureInfo.InvariantCulture))
                     .ToArray();

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

    public static List<AvgSubtable> CalculateAverages(List<Subtable> subtables)
    {
        List<AvgSubtable> result = new();

        foreach (Subtable subtable in subtables)
        {
            if(subtable.Measurements.Count == 0)
            {
                continue;
            }

            var avgData = new List<double>();

            double[] sumData = new double[subtable.Measurements[0].Data.Length];

            foreach(Measurement measurement in subtable.Measurements)
            {
                for(int i = 0; i < sumData.Length; i++)
                {
                    sumData[i] += measurement.Data[i];
                }
            }

            for(int i = 0;i < sumData.Length; i++)
            {
                avgData.Add(Math.Round(sumData[i] / subtable.Measurements.Count, 3));
            }
            AvgSubtable avgSubtable = new(subtable.Name, avgData.ToArray());
            result.Add(avgSubtable);

        }

        return result;
    }


}
