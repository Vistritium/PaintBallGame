using SimpleJSON;
using UnityEngine;

namespace Assets.Communication.Packet
{
    public struct PlayerData
    {
        public const string key = "playerData";
        public static float DefaultRadius = 1.0f;
        public float radius;

        public PlayerData(uint id, string name, Color color, float x, float y, bool isMain = false) : this()
        {
            this.id = id;
            this.name = name;
            colorRed = color.r;
            colorBlue = color.b;
            colorGreen = color.g;
            this.x = x;
            this.y = y;
            this.isMain = isMain;
            radius = DefaultRadius;
        }

        public PlayerData(JSONNode node) : this()
        {
            id = (uint) node["id"].AsInt;
            name = node["name"];
            colorRed = node["colorRed"].AsFloat;
            colorGreen = node["colorGreen"].AsFloat;
            colorBlue = node["colorBlue"].AsFloat;
            x = node["x"].AsFloat;
            y = node["y"].AsFloat;
            isMain = node["isMain"].AsBool;
            radius = DefaultRadius;
        }

        public uint id { get; set; }
        public string name { get; set; }
        public float colorRed { get; set; }
        public float colorGreen { get; set; }
        public float colorBlue { get; set; }
        public bool isMain { get; set; }
        public float x { get; set; }
        public float y { get; set; }


        public override string ToString()
        {
            return
                string.Format(
                    "colorBlue: {0}, colorGreen: {1}, colorRed: {2}, id: {3}, isMain: {4}, x: {5}, y: {6}, name: {7}",
                    colorBlue, colorGreen, colorRed, id, isMain, x, y, name);
        }
    }
}