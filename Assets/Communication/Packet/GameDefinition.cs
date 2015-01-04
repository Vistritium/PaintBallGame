using SimpleJSON;

namespace Assets.Communication.Packet
{
    public class GameDefinition
    {
        public readonly float Height;
        public readonly float Speed;
        public readonly float Width;

        public GameDefinition(JSONNode node)
        {
            Speed = node["speed"].AsFloat;
            Width = node["width"].AsFloat;
            Height = node["height"].AsFloat;
        }
    }
}