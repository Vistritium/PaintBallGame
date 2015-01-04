using System;
using System.Text;
using AssemblyCSharp;
using Assets.Utils;
using UnityEngine;

namespace Assets.Drawing
{
    //credits: Aron Granberg http://www.arongranberg.com/unity/unitypaint/
    public class Drawer
    {
        private readonly Action<Color, Color, Player> pixelChanged;

        public Drawer(Action<Color, Color, Player> pixelChanged)
        {
            this.pixelChanged = pixelChanged;
        }

        private static float GaussFalloff(float distance, float inRadius)
        {
            return Mathf.Clamp01(Mathf.Pow(360.0f, (float) (-Mathf.Pow(distance/inRadius, 2.5f) - 0.01)));
        }

        private static Vector2[] Sample(Vector2 p)
        {
            return new[]
            {
                p + new Vector2(0.25f, 0.5f),
                p + new Vector2(0.75f, 0.5f),
                p + new Vector2(0.5f, 0.25f),
                p + new Vector2(0.5f, 0.75f)
            };
        }

        public void Paint(Vector2 posPercentage, float rad, Color col, Texture2D tex, float ratio, Player player)
        {
            var pos = new Vector2(posPercentage.x*tex.width, posPercentage.y*tex.height);
            var start = new Vector2(Mathf.Clamp(pos.x - rad*ratio, 0, tex.width),
                Mathf.Clamp(pos.y - rad, 0, tex.height));
            var end = new Vector2(Mathf.Clamp(pos.x + rad*ratio, 0, tex.width), Mathf.Clamp(pos.y + rad, 0, tex.height));
            float width = rad*2;

            var widthX = (int) Mathf.Round(end.x - start.x);
            var widthY = (int) Mathf.Round(end.y - start.y);

            StringBuilder linesAggregate = null;

            Color[] pixels = tex.GetPixels((int) start.x, (int) start.y, widthX, widthY, 0);
            var center = new Vector2(widthX/2.0f, widthY/2.0f);
            float debugDist = 0.0f;
            for (int y = 0; y < widthY; y++)
            {
                for (int x = 0; x < widthX; x++)
                {
                    float percentageX = x == 0 ? 0 : x/(float) widthX;
                    float percentageY = y == 0 ? 0 : y/(float) widthY;

                    float dist = (new Vector2(0.5f, 0.5f) - new Vector2(percentageX, percentageY)).magnitude;
                    debugDist = dist;

                    if (dist > 0.5f)
                    {
                        continue;
                    }


                    int cIndex = y*widthX + x;
                    Color pixel = pixels[cIndex];
                    if (!pixel.Eq(col))
                    {
                        pixelChanged.Invoke(pixel, col, player);
                        pixels[cIndex] = col;
                    }
                }
            }

            tex.SetPixels((int) start.x, (int) start.y, widthX, widthY, pixels, 0);
        }


/*        public static Texture2D Paint(Vector2 posPercentage, float rad, Color col, float hardness, Texture2D tex,
            float ratio)
        {
            var pos = new Vector2(posPercentage.x*tex.width, posPercentage.y*tex.height);
            Debug.Log(string.Format("tex x: {0} y: {1}", tex.width, tex.height));
            var start = new Vector2(Mathf.Clamp(pos.x - rad*0.5f, 0, tex.width), Mathf.Clamp(pos.y - rad, 0, tex.height));
            float width = rad*2;
            var end = new Vector2(Mathf.Clamp(pos.x + rad*0.5f, 0, tex.width), Mathf.Clamp(pos.y + rad, 0, tex.height));
            var widthX = (int) Mathf.Round(end.x - start.x);
            var widthY = (int) Mathf.Round(end.y - start.y);
            //     float sqrRad = rad*rad;
            float sqrRad2 = (rad + 1)*(rad + 1);
            Color[] pixels = tex.GetPixels((int) start.x, (int) start.y, widthX, widthY, 0);

            for (int y = 0; y < widthY; y++)
            {
                for (int x = 0; x < widthX; x++)
                {
                    Vector2 p = new Vector2(x, y) + start;
                    Vector2 center = p + new Vector2((float) 0.25, (float) 0.5);
                    float dist = (center - pos).sqrMagnitude;
                    if (dist > sqrRad2)
                    {
                        continue;
                    }
                    Vector2[] samples = Sample(p);
                    Color c = Color.black;
                    for (int i = 0; i < samples.Length; i++)
                    {
                        dist = GaussFalloff(Vector2.Distance(samples[i], pos), rad)*hardness;
                        if (dist > 0)
                        {
                            c += Color.Lerp(pixels[y*widthX + x], col, dist);
                        }
                        else
                        {
                            c += pixels[y*widthX + x];
                        }
                    }
                    c /= samples.Length;

                    pixels[y*widthX + x] = c;
                }
            }

            tex.SetPixels((int) start.x, (int) start.y, widthX, widthY, pixels, 0);
            return tex;
        }*/
    }
}