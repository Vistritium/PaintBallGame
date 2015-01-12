using System;
using AssemblyCSharp;
using SimpleJSON;
using UnityEngine;

namespace Assets.Powerups
{
    public class SpeedPowerup : Powerup
    {
        private float duration;
        private int id;
        private float speedBonus;


        public void Initialize(int id, JSONNode data)
        {
            this.id = id;
            speedBonus = data["speedBonus"].AsFloat;
            duration = data["duration"].AsFloat;

            if (speedBonus == 0 || duration == 0)
            {
                throw new ArgumentException("No json data in SpeedPowerUp Initialize");
            }
        }

        // Use this for initialization
        protected override void Start()
        {
            base.Start();
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
        }

        public override float GetDuration()
        {
            return duration;
        }

        protected override void StartEffect()
        {
            Debug.Log("Start effect");
            GetComponent<Player>().AddSpeedMultiplier(speedBonus);
        }

        protected override void FinishEffect()
        {
            Debug.Log("Finish effect");
            GetComponent<Player>().RemoveSpeedMultiplier(speedBonus);
        }

        protected override int GetId()
        {
            return id;
        }
    }
}