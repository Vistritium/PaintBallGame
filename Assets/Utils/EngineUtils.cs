using System;
using UnityEngine;

namespace Assets.Utils
{
    public static class EngineUtils
    {
        public static bool Eq(this Color x, Color y)
        {
            float delta = 0.1f;
            if (Math.Abs(x.r - y.r) < delta && Math.Abs(x.g - y.g) < delta && Math.Abs(x.b - y.b) < delta)
            {
                return true;
            }
            return false;
        }
    }
}