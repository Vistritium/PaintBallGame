using AssemblyCSharp;
using SimpleJSON;
using UnityEngine;

namespace Assets.Communication.Packet
{
    public class StateUpdate
    {
        public const string key = "stateUpdate";

        public StateUpdate(uint id, string state, float time, Vector2 position)
        {
            this.id = id;
            this.state = state;
            this.time = time;
            this.position = position;
        }

        public StateUpdate(JSONNode json)
        {
            id = (uint) json["id"].AsInt;
            state = json["state"];
            time = json["time"].AsFloat;
            position = new Vector2(json["x"].AsFloat, json["y"].AsFloat);
        }

        public uint id { get; private set; }
        public string state { get; private set; }
        public float time { get; private set; }
        public Vector2 position { get; private set; }

        public MovingState GetState()
        {
            return MovingStateUtil.FromString(state);
        }


        public string ToJson()
        {
/*           // var jsonNode = new JSONNode();
            var jsonNode = new JSONClass();
            jsonNode["funKey"] = key;
           // var valueNode = jsonNode["value"];
           // Debug.Log(valueNode);
            jsonNode["value"]["id"].AsInt = (int)id;
            jsonNode["value"]["state"] = state;
            Debug.Log(jsonNode.ToString());
            return jsonNode.ToString();*/

            var json = new JSONClass();
            json["id"].AsInt = (int) id;
            json["state"] = state;
            json["time"].AsFloat = time;
            json["x"].AsFloat = position.x;
            json["y"].AsFloat = position.y;
            return PacketUtils.WrapAroundJson(key, json).ToString();
        }
    }
}