using System.Collections.Generic;
using AssemblyCSharp;
using Assets.Communication.Packet;
using SimpleJSON;
using UnityEngine;
using UInput = UnityEngine.Input;

namespace Assets.Communication
{
    internal class TestCommunicater : MonoBehaviour
    {
        private Input input;
        private PlayerManager playerManager;

        private void Start()
        {
            GameObject systems = GameObject.Find("Systems");
            input = systems.GetComponent<Input>();
            playerManager = systems.GetComponent<PlayerManager>();
        }

        private string WrapJsonAroundFunction(string function, string json)
        {
            var jsonNode = new JSONClass();
            jsonNode["funKey"] = function;
            jsonNode["value"] = json;
            return jsonNode.ToString();
        }

        private void SendMessage(string funKey, string value)
        {
            string wrapJsonAroundFunction = WrapJsonAroundFunction(funKey, value);
            Debug.Log("to send: " + wrapJsonAroundFunction);
            input.SendJSon(wrapJsonAroundFunction);
        }

        private void Update()
        {
            if (UInput.GetKeyDown(KeyCode.N))
            {
                //string json = "";
                GetComponent<PlayerManager>().AddPlayerToGame(new PlayerData(0, "", Color.magenta, 0, 0, true));
                //     this.SendMessage("AddPlayer", "{\"id\":0,\"name\":\"Maciej\",\"colorRed\":1.0,\"colorGreen\":0.0,\"colorBlue\":1.0,\"isMain\":true,\"x\":0.0,\"y\":0.0}");
                //PlayerData deserialiseData = JsonConvert.DeserializeObject<PlayerData>(json);
            }


            if (UInput.GetKeyDown(KeyCode.M))
            {
                input.StartGame("");
            }

            if (UInput.GetKeyDown(KeyCode.L))
            {
                Debug.developerConsoleVisible = true;
            }

            if (UInput.GetKeyDown(KeyCode.J))
            {
                SendMessage("finish", "");
            }

            if (UInput.GetKeyDown(KeyCode.P))
            {
                Dictionary<Player, float> playerPercentPixels = playerManager.PixelTracker.GetPlayerPercentPixels();
                foreach (var playerPercentPixel in playerPercentPixels)
                {
                    Debug.Log(string.Format("color {0} has {1}% pixels", playerPercentPixel.Key,
                        playerPercentPixel.Value));
                }
            }

            if (UInput.GetKeyDown(KeyCode.T))
            {
                JSONNode parsed =
                    JSON.Parse(
                        "[{ \"firstName\":\"John\" , \"lastName\":\"Doe\" }, { \"firstName\":\"Anna\" , \"lastName\":\"Smith\" }, { \"firstName\":\"Peter\" , \"lastName\":\"Jones\" }]");
                Debug.Log(parsed.Count);
            }
        }
    }
}