using System;
using System.Linq;
using UnityEngine;

namespace AssemblyCSharp
{
    public enum MovingState
    {
        NONE,
        LEFT,
        UP,
        RIGHT,
        DOWN,
        LEFTUP,
        RIGHTUP,
        LEFTDOWN,
        RIGHTDOWN
    }

    public static class MovingStateUtil
    {
        public static MovingState With(this MovingState stateA, MovingState stateB)
        {
            var states = new MovingState[2];
            states[0] = stateA;
            states[1] = stateB;

            if (states.Contains(MovingState.LEFT) && states.Contains(MovingState.UP))
            {
                return MovingState.LEFTUP;
            }
            if (states.Contains(MovingState.RIGHT) && states.Contains(MovingState.UP))
            {
                return MovingState.RIGHTUP;
            }
            if (states.Contains(MovingState.LEFT) && states.Contains(MovingState.DOWN))
            {
                return MovingState.LEFTDOWN;
            }
            if (states.Contains(MovingState.RIGHT) && states.Contains(MovingState.DOWN))
            {
                return MovingState.RIGHTDOWN;
            }
            Debug.Log("Did not found conversion, returning NONE " + stateA + " " + stateB);
            return MovingState.NONE;
        }

        public static MovingState FromString(string value)
        {
            return (MovingState) Enum.Parse(typeof (MovingState), value);
        }
    }
}