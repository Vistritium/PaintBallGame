using System;
using SimpleJSON;
using UnityEngine;

namespace Assets.Communication.Packet
{
    public class PacketUtils
    {
        public static JSONClass WrapAroundJson(string funkey, JSONNode value)
        {
            var json = new JSONClass();
            json["funKey"] = funkey;
            json["value"] = value;
            return json;
        }
    }
}