using SimpleJSON;

namespace Assets.Communication.Packet
{
    public class MapBonusData
    {
        public MapBonusData(JSONNode node)
        {
            id = node["id"].AsInt;
            x = node["x"].AsFloat;
            y = node["y"].AsFloat;
            name = node["name"].Value;
/*            JSONNode jsonNode = node["lifespan"];
            if (jsonNode != null)
            {
                lifespan = jsonNode.AsFloat;                
            }*/
        }

        public float lifespan { get; set; }

        public int id { get; set; }

        public string name { get; set; }

        public float x { get; set; }

        public float y { get; set; }
    }
}