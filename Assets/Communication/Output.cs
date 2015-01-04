using System;
using System.Collections.Generic;
using AssemblyCSharp;
using Assets.Communication.Packet;
using SimpleJSON;
using UnityEngine;

namespace Assets.Communication
{
    [RequireComponent(typeof(PlayerManager))]
    public class Output : MonoBehaviour
    {
        public static void Logg(string log)
        {
            output.Log(log);
        }

        public void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.F))
            {
               this.SendMessage("SendMsg", "IT STILL EXISTS");
            }
        }

        public static Output output;

        public StateUpdater StateUpdater;

        private Input _input;

        private PlayerManager _manager;

        private void Start()
        {
            output = this;
            _manager = GetComponent<PlayerManager>();
            _input = GetComponent<Input>();
            PlayerController.Output = this;
            Application.ExternalCall("initConnection");
            StateUpdater = GetComponent<StateUpdater>();
        }

        public void SendPixelTrackerState()
        {
            PlayerManager playerManager = GetComponent<PlayerManager>();
            PixelTrackerState pixelTrackerState = new PixelTrackerState(playerManager.PixelTracker.GetPlayerPercentPixels());
            SendJSonToServer(pixelTrackerState.ToJson());
        }

        public void AteBonus(int bonusId)
        {
            var jsonData = new JSONData(bonusId);
            JSONClass wrapAroundJson = PacketUtils.WrapAroundJson("ateBonus", jsonData);

            SendJSonToServer(wrapAroundJson.ToString());
            
        }

        public void SendState(uint playerId, MovingState state, Vector2 position)
        {
            var stateUpdate = new StateUpdate(playerId, state.ToString(), StateUpdater.TimeFromStart(), position);
            //Output.Logg("State update: " + stateUpdate.ToJson());
            SendJSonToServer(stateUpdate.ToJson());
        }

        public void FinishGame()
        {
            
        }

        public void SendJSonToServer(string json)
        {
            #if !UNITY_EDITOR
            Application.ExternalCall("sendToServer", json);
            #else 
            Debug.Log(json);
            #endif
        }

        public void Log(string log)
        {
            #if !UNITY_EDITOR
            Application.ExternalCall("console.log", log);
            #endif
        }


    }
}
