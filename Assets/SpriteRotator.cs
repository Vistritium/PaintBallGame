using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets
{
    public class SpriteRotator : MonoBehaviour
    {
        public float factor = 20;
        public Rotation rotation = Rotation.Left;

        // Use this for initialization
        private void Start()
        {
            transform.eulerAngles = transform.eulerAngles + new Vector3(0, 0, Random.Range(0.0f, 90.0f));
        }

        // Update is called once per frame
        private void Update()
        {
            float rotationModifier;
            switch (rotation)
            {
                case Rotation.Right:
                    rotationModifier = -1.0f;
                    break;
                case Rotation.Left:
                    rotationModifier = 1.0f;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            transform.eulerAngles = transform.eulerAngles + new Vector3(0, 0, Time.deltaTime * factor * rotationModifier);
        }

        public enum Rotation

        {
            Right,
            Left
        }
    }
}