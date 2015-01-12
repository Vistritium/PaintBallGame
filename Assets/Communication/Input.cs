using System;
using System.Collections.Generic;
using AssemblyCSharp;
using Assets.Communication.Packet;
using Assets.MapBonus;
using SimpleJSON;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Communication
{
    [RequireComponent(typeof (PlayerManager))]
    public class Input : MonoBehaviour
    {
        private readonly Dictionary<string, StringArg> functions = new Dictionary<string, StringArg>();
        private PlayerManager _manager;
        private Output _output;
        public GameDefinition gameDefinition;
        private StateUpdater stateUpdater;

        private void Start()
        {
            functions.Add("start", json => _manager.StartGame());
            functions.Add("updatePositions", UpdatePositions);
            functions.Add("stateUpdate", UpdateState);
            functions.Add("playerData", AddPlayer);
            functions.Add("removePlayer", RemovePlayer);
            functions.Add("playerLeft", RemovePlayer);
            functions.Add("finish", json => _manager.FinishGame());
            functions.Add("getPixelTracketState", json => _output.SendPixelTrackerState());
            functions.Add("updatePosition", UpdatePosition);
            functions.Add("gameDefinition", GameDefinition);
            functions.Add("powerUp", PowerUp);
            functions.Add("mapBonus", AddMapBonus);
            functions.Add("destroyObject", DestroyObject);


            _manager = GetComponent<PlayerManager>();
            _output = GetComponent<Output>();
             stateUpdater = GetComponent<StateUpdater>();
        }

        public void AddMapBonus(JSONNode node)
        {
            var mapBonusData = new MapBonusData(node);
            var mapBonuses = (GameObject)GameObject.Find("MapBonuses");
            foreach (Transform child in mapBonuses.transform)
            {
                if (child.name == mapBonusData.name)
                {
                    GameObject newObj = (GameObject) Instantiate(child.gameObject);
                    newObj.GetComponent<MapBonusElement>().Initialize(mapBonusData.id, mapBonusData.lifespan);
                    newObj.name = mapBonusData.name + mapBonusData.id;
                    newObj.transform.position = new Vector3(mapBonusData.x, mapBonusData.y, newObj.transform.position.z);
                    newObj.SetActive(true);

                }
            }


        }

        public void DestroyObject(JSONNode node)
        {
            var destroyData = new DestroyData(node);
            GameObject toDestroy = GameObject.Find(destroyData.name + destroyData.id);
            if (toDestroy != null)
            {
                Destroy(toDestroy);
            }
            else
            {
                Output.Logg("Tried to destroy " + destroyData.name + destroyData.id + "but it does not exist");
            }
        }

        public void PowerUp(JSONNode node)
        {
            var powerupData = new PowerupData(node);
            _manager.AddPowerup(powerupData);

        }

        public void GameDefinition(JSONNode node)
        {
            gameDefinition = new GameDefinition(node);
            Player.PlayerSpeed = gameDefinition.Speed;
        }

        public void SendJSon(string json)
        {
            //Application.ExternalCall("console.log", "GOT: " + json);
            //Output.Logg("Got message: " + json);
            JSONNode parsed = null;
            try
            {
                parsed = JSON.Parse(json);
                functions[parsed["funKey"]](parsed["value"]);
            }
            catch (Exception e)
            {
                if (parsed != null)
                {
                    Output.Logg(parsed.ToString());
                }
                else
                {
                    Output.Logg("raw: " + json);
                }
                Output.Logg(e.ToString());

                throw;
            }
        }

        public void SendMsg(string msg)
        {
            Application.ExternalCall("console.log", msg);
        }

        public void UpdateState(JSONNode jsonStateUpdate)
        {
            var stateUpdate = new StateUpdate(jsonStateUpdate);

              stateUpdater.AddState(stateUpdate);

           // _manager.UpdatePlayer(stateUpdate.id, stateUpdate.position);
           // _manager.UpdatePlayer(stateUpdate.id, MovingStateUtil.FromString(stateUpdate.state));
        }


        public void UpdatePosition(JSONNode position)
        {
            var vector2 = new Vector2(position["x"].AsFloat, position["y"].AsFloat);
            Output.Logg(string.Format("updating by {0}", vector2));
            _manager.UpdatePlayer((uint) position["id"].AsInt, vector2);
        }

        // array of objects id, x, y
        public void UpdatePositions(JSONNode positions)
        {
            for (int i = 0; i < positions.Count; i++)
            {
                // Output.Logg("Id " + positions[i]["id"].AsInt + " got coords: " + positions[i]["x"].AsFloat + " " + positions[i]["y"].AsFloat);
                _manager.UpdatePlayer((uint) positions[i]["id"].AsInt,
                    new Vector2(positions[i]["x"].AsFloat, positions[i]["y"].AsFloat));
            }
        }

        public void RemovePlayer(JSONNode jsonNode)
        {
            _manager.RemovePlayerAndDestroy((uint) jsonNode.AsInt);
        }

        public void AddPlayer(JSONNode jsonNode)
        {
            _manager.AddPlayerToGame(new PlayerData(jsonNode));
        }

        public void StartGame(string start)
        {
            Debug.Log("Starting game");
            _manager.StartGame();
        }

        private delegate void StringArg(JSONNode json);
    }
}