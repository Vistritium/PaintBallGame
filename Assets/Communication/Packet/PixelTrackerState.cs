using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using AssemblyCSharp;
using SimpleJSON;

namespace Assets.Communication.Packet
{
    public class PixelTrackerState
    {
        private readonly Dictionary<Player, float> playerPixelPercent;
        private const string FunKey = "pixelTracketState";

        public PixelTrackerState(Dictionary<Player, float> playerPixelPercent)
        {
            this.playerPixelPercent = playerPixelPercent;
        }

        public string ToJson()
        {
            var jsonNode = new JSONClass();
            foreach (var entry in playerPixelPercent)
            {
                jsonNode[entry.Key.Id.ToString()] = entry.Value.ToString(CultureInfo.InvariantCulture);
            }
            string result = PacketUtils.WrapAroundJson(FunKey, jsonNode).ToString();
            UnityEngine.Debug.Log(result);
            return result;
        }
    }
}
