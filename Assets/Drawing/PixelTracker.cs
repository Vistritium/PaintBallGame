using System;
using System.Collections.Generic;
using AssemblyCSharp;
using Assets.Utils;
using UnityEngine;

namespace Assets.Drawing
{
    public class PixelTracker
    {
        private readonly uint pixelAmount;
        private readonly Dictionary<Player, MutableTracker> players;


        public PixelTracker(List<Player> players, uint pixelAmount)
        {
            this.players = new Dictionary<Player, MutableTracker>();
            players.ForEach(x => this.players.Add(x, new MutableTracker(pixelAmount)));
            this.pixelAmount = pixelAmount;
        }

        public void PixelChanged(Color from, Color to, Player player)
        {
            players[player].Increment();
            foreach (var curPlayer in players)
            {
                if (curPlayer.Key.Color.Eq(from))
                {
                    curPlayer.Value.Decrement();
                }
            }
        }

        public Dictionary<Player, float> GetPlayerPercentPixels()
        {
            var sortedDictionary = new Dictionary<Player, float>();

            foreach (var playerPixels in players)
            {
                sortedDictionary.Add(playerPixels.Key, playerPixels.Value.GetPercent());
            }

            return sortedDictionary;
        }

        private class MutableTracker
        {
            private readonly uint maxValue;
            private uint amount;

            public MutableTracker(uint maxValue)
            {
                this.maxValue = maxValue;
                amount = 0;
            }

            public void Increment()
            {
                amount = amount + 1;
            }

            public void Decrement()
            {
                if (amount == 0)
                {
                    throw new InvalidOperationException("Cant be less pixels than 0");
                }
                amount = amount - 1;
            }

            public uint Get()
            {
                return amount;
            }

            public float GetPercent()
            {
                float result = amount/(float) maxValue;
                return result;
            }
        }
    }
}