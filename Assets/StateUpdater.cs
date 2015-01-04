using System;
using System.Collections.Generic;
using System.Linq;
using AssemblyCSharp;
using Assets.Communication;
using Assets.Communication.Packet;
using UnityEngine;

namespace Assets
{
    [RequireComponent(typeof (PlayerManager))]
    public class StateUpdater : MonoBehaviour
    {
        private PlayerManager _manager;
        private float delayTime;

        private float lastDelay = 0;
        private Dictionary<uint, GameObject> players;
        private float startTime;
        private List<StateUpdate> stateUpdates;

        private void Start()
        {
            stateUpdates = new List<StateUpdate>();
            _manager = GetComponent<PlayerManager>();
            Output.Logg(string.Format("stateUpdates: {0}", stateUpdates));
            players = GetComponent<PlayerManager>().players;
        }

        public void StartTime()
        {
            startTime = Time.time;
        }

        public float TimeFromStart()
        {
            return Time.time - startTime;
        }

        public void AddState(StateUpdate stateUpdate)
        {
#if UNITY_EDITOR
            if (stateUpdate == null)
            {
                throw new ArgumentNullException("stateUpdate");
            }

            if (stateUpdates == null)
            {
                throw new ArgumentNullException("stateUpdates");
            }
#endif

            if (stateUpdate.time + delayTime <= TimeFromStart())
            {
                UpdateStateNow(stateUpdate);
                delayTime = TimeFromStart() - stateUpdate.time;
                Debug.Log("New delay: " + delayTime);
            }
            else
            {
                stateUpdates.Add(stateUpdate);
            }
        }

        private void UpdateStateNow(StateUpdate stateUpdate)
        {
            _manager.UpdatePlayer(stateUpdate.id, stateUpdate.position);
            _manager.UpdatePlayer(stateUpdate.id, MovingStateUtil.FromString(stateUpdate.state));
        }

        private void Update()
        {
            if (stateUpdates.Any())
            {
                StateUpdate stateUpdate = stateUpdates[0];
                if (stateUpdate.time <= TimeFromStart() + delayTime)
                {
                    UpdateStateNow(stateUpdate);
                    stateUpdates.RemoveAt(0);
/*                    if (delayTime != lastDelay)
                    {
                        Output.Logg(string.Format("new delay {0}", delayTime));
                        lastDelay = delayTime;
                    }*/
                }
            }
        }
    }
}