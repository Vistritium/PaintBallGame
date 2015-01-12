using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using Random = System.Random;

namespace AssemblyCSharp
{
    [RequireComponent(typeof (MeshRenderer))]
    public class Player : MonoBehaviour
    {
        public static float PlayerSpeed = 5.0f;

        private Color _color;

        private MovingState _state;
        private Vector2 materialSize;

        private float planeHeight;
        private float planeWidth;
        private float speed;

        //for info only, do not change
        public float lastSpeed;

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        private List<float> speedModifiersPositive = new List<float>();
        private List<float> speedModifiersNegative = new List<float>();

        private List<float> sizeModifiers = new List<float>();
        private bool sizeModifiersDirty = false;
        


        public string Name { get; set; }

        public uint Id { get; set; }

        public void AddSizeModifier(float multiplier)
        {
            sizeModifiers.Add(multiplier);
            sizeModifiersDirty = true;
        }

        public void RemoveSizeModifier(float multiplier)
        {
            sizeModifiers.Remove(multiplier);
            sizeModifiersDirty = true;
        }

        public void AddSpeedMultiplier(float multiplier)
        {
            if (multiplier > 1)
            {
                speedModifiersPositive.Add(PlayerSpeed*multiplier - PlayerSpeed);
                speedModifiersPositive.Sort();
            }
            else if (multiplier < 1)
            {
                speedModifiersNegative.Add(PlayerSpeed - PlayerSpeed*multiplier);
                speedModifiersNegative.Sort();
            }
        }

        public void RemoveSpeedMultiplier(float multiplier)
        {
            if (multiplier > 1)
            {
                speedModifiersPositive.Remove(PlayerSpeed * multiplier - PlayerSpeed);
            }
            else if (multiplier < 1)
            {
                speedModifiersNegative.Remove(PlayerSpeed - PlayerSpeed * multiplier);
            }
        }

        public MovingState State
        {
            get { return _state; }
            set { _state = value; }
        }

        public Color Color
        {
            get { return _color; }
            set
            {
                GetComponent<MeshRenderer>().materials[0].color = value;
                _color = value;
            }
        }

        private void Start()
        {
            speed = PlayerSpeed;
        }

        public bool CollidesWithCircle(Vector2 position, float r)
        {
            float myR = transform.localScale.x;
            return Vector2.Distance(position, new Vector2(transform.position.x, transform.position.y)) < r + myR;
        }

        private void UpdateSize()
        {
            if (sizeModifiersDirty)
            {
                sizeModifiersDirty = false;

                sizeModifiers.Sort();
                float sizeModifier = 1;
                for (int i = 0; i < sizeModifiers.Count; i++)
                {
                    sizeModifier *= 1 + ((sizeModifiers[i]-1) / ((float)Math.Pow(2, i)));
                }
                this.transform.localScale = new Vector3(sizeModifier, sizeModifier, sizeModifier);

            }
        }


        private void Update()
        {

            int x = 0;
            int y = 0;

            StateToFactors(out y, out x);

            UpdateSize();

            if (x != 0 || y != 0)
            {
/*                if (speedModifiersPositive.Count >= 2)
                {
                    Debug.Log("hi");
                }*/
                float multipliedSpeed = Speed;
                for (int i = 0; i < speedModifiersPositive.Count; i++)
                {
                    float add = speedModifiersPositive[i]/((float) Math.Pow(2, i));
                    multipliedSpeed += add;
                }
                for (int i = 0; i < speedModifiersNegative.Count; i++)
                {
                    float add = speedModifiersNegative[i]/((float) Math.Pow(2, i));
                    multipliedSpeed -= add;
                }
                lastSpeed = multipliedSpeed;
                transform.Translate(multipliedSpeed*Time.deltaTime*x, multipliedSpeed*Time.deltaTime*y, 0);
            }
            // var vector3 = 
        }

        private void StateToFactors(out int y, out int x)
        {
            switch (_state)
            {
                case MovingState.DOWN:
                    y = -1;
                    x = 0;
                    return;
                case MovingState.LEFT:
                    x = -1;
                    y = 0;
                    return;
                case MovingState.LEFTDOWN:
                    x = -1;
                    y = -1;
                    return;
                case MovingState.LEFTUP:
                    x = -1;
                    y = 1;
                    return;
                case MovingState.NONE:
                    x = 0;
                    y = 0;
                    return;
                case MovingState.RIGHT:
                    x = 1;
                    y = 0;
                    return;
                case MovingState.RIGHTDOWN:
                    x = 1;
                    y = -1;
                    return;
                case MovingState.RIGHTUP:
                    x = 1;
                    y = 1;
                    return;
                case MovingState.UP:
                    x = 0;
                    y = 1;
                    return;
                default:
                    throw new ArgumentException();
            }
        }

        public override string ToString()
        {
            if ("".Equals(name))
            {
                return Id.ToString();
            }
            return name;
        }
    }
}