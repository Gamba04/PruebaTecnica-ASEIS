using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class ColorExporter
{
    public static void ExportColors(string path, List<Color> colors)
    {
        using (StreamWriter stream = new StreamWriter(path))
        {
            foreach (Color color in colors)
            {
                stream.WriteLine($"{color.r};{color.g};{color.b};{color.a}");
            }

            stream.Close();
        }
    }
}