using System;
using UnityEngine;

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

        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public string Name { get; set; }

        public uint Id { get; set; }

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


        private void Update()
        {
            int x = 0;
            int y = 0;

            StateToFactors(out y, out x);

            if (x != 0 || y != 0)
            {
                transform.Translate(speed*Time.deltaTime*x, speed*Time.deltaTime*y, 0);
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