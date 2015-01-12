using System;
using Assets.Powerups;
using SimpleJSON;
using UnityEngine;

namespace Assets.Communication.Packet
{
    public class PowerupData
    {
        public PowerupData(uint playerId, int powerUpId, JSONNode powerUpData)
        {
            PlayerId = playerId;
            PowerUpId = powerUpId;
            PowerUpData = powerUpData;
        }

        public PowerupData(JSONNode node)
        {
            PlayerId = (uint) node["playerId"].AsInt;
            PowerUpId = node["powerupId"].AsInt;
            PowerUpData = node["data"];
        }

        public uint PlayerId { get; set; }
        public int PowerUpId { get; set; }
        public JSONNode PowerUpData { get; set; }


        public Powerup AddPowerupToGameobject(GameObject gameObject)
        {
            switch (PowerUpId)
            {
                case 1:
                {
                    var speedPowerup = gameObject.AddComponent<SpeedPowerup>();
                    speedPowerup.Initialize(PowerUpId, PowerUpData);
                    return speedPowerup;
                }

                case 2:
                {
                    var sizePowerup = gameObject.AddComponent<SizePowerup>();
                    sizePowerup.Initialize(PowerUpId, PowerUpData);
                    return sizePowerup;
                }


                default:
                {
                    throw new ArgumentException("Incorrect powerupId: " + PowerUpId);
                }
            }
        }
    }
}