using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AssemblyCSharp;
using SimpleJSON;
using UnityEngine;

namespace Assets.Powerups
{
    public class SizePowerup : Powerup
    {
        private int id;
        private float sizeBonus;
        private float duration;

        public void Initialize(int id, JSONNode data)
        {
            this.id = id;
            sizeBonus = data["sizeBonus"].AsFloat;
            duration = data["duration"].AsFloat;

            if (sizeBonus == 0 || duration == 0)
            {
                throw new ArgumentException("No json data in SizePowerup Initialize");
            }
        }

        public override float GetDuration()
        {
            return duration;
        }

        protected override void StartEffect()
        {
            GetComponent<Player>().AddSizeModifier(sizeBonus);
        }

        protected override void FinishEffect()
        {
            GetComponent<Player>().RemoveSizeModifier(sizeBonus);
        }

        protected override Color GetColor()
        {
            return Color.red;
        }

        protected override int GetId()
        {
            return id;
        }
    }
}
