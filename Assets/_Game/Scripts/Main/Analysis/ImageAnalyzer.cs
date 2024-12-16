using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class ImageAnalyzer
{
    public static List<Color> GetPredominantColors(Texture2D image, uint amount)
    {
        Dictionary<Color, uint> data = GetColorData(image);

        List<KeyValuePair<Color, uint>> sortedData = data.OrderBy(color => color.Value, Comparer<uint>.Create((a, b) => (int)(b - a))).ToList();
        List<Color> predominantColors = sortedData.ConvertAll(color => color.Key);

        return predominantColors.GetRange(0, (int)amount);
    }

    private static Dictionary<Color, uint> GetColorData(Texture2D image)
    {
        Dictionary<Color, uint> data = new Dictionary<Color, uint>();

        int count = 0;

        for (int x = 0; x < image.width; x++)
        {
            for (int y = 0; y < image.height; y++)
            {
                Color color = image.GetPixel(x, y);

                if (data.ContainsKey(color))
                {
                    data[color]++;

                    count++;
                }
                else
                {
                    data.Add(color, 1);
                }
            }
        }

        return data;
    }
}