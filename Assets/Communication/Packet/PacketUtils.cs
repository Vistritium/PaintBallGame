using System;
using System.Diagnostics;
using SimpleJSON;

namespace Assets.Communication.Packet
{
    public class PacketUtils
    {
        public static JSONClass WrapAroundJson(string funkey, JSONNode value)
        {
            var json = new JSONClass();
            json["funKey"] = funkey;
            json["value"] = value;
            String wtf = json.ToString();
            if (wtf == "d")
            {
                throw new ArgumentException("");
            }
            return json;
        }
    }
}