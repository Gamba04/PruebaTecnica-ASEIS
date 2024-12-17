using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class ColorExporter
{
    public static void ExportColors(string path, List<Color> colors)
    {
        using (StreamWriter stream = new StreamWriter(path))
        {
            List<string> data = GetExportData(colors);

            foreach (string row in data)
            {
                stream.WriteLine(row);
            }

            stream.Close();
        }
    }

    private static List<string> GetExportData(List<Color> colors)
    {
        List<string> data = new List<string>();

        data.Add("R;G;B");

        foreach (Color color in colors)
        {
            string r = FormatColorComponent(color.r);
            string g = FormatColorComponent(color.g);
            string b = FormatColorComponent(color.b);

            data.Add($"{r};{g};{b}");
        }

        return data;
    }

    private static string FormatColorComponent(float value)
    {
        value = Mathf.Round(value * 100);

        return $"{value}%";
    }
}