using SimpleJSON;

namespace Assets.Communication.Packet
{
    public class DestroyData
    {
        public DestroyData(JSONNode node)
        {
            name = node["name"].Value;
            id = node["id"].AsInt;
        }

        public int id { get; set; }

        public string name { get; set; }
    }
}