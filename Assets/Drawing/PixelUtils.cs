using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Drawing
{
    public class PixelUtils
    {
        public static Dictionary<Color, uint> SortPixels(Texture2D texture)
        {
            Color[] pixels = texture.GetPixels();
            Dictionary<Color, uint> result = pixels.ToList().Aggregate(new Dictionary<Color, uint>(),
                (dictionary, color) =>
                {
                    if (dictionary.ContainsKey(color))
                    {
                        dictionary[color] = dictionary[color] + 1;
                    }
                    else
                    {
                        dictionary.Add(color, 1);
                    }
                    return dictionary;
                });
            return result;
        }
    }
}