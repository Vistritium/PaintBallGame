using AssemblyCSharp;
using UnityEngine;

namespace Assets.Powerups
{
    [RequireComponent(typeof (Player))]
    public abstract class Powerup : MonoBehaviour
    {
        private float timeLeft;

        // Use this for initialization
        protected virtual void Start()
        {
            timeLeft = GetDuration();
            StartEffect();
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft <= 0)
            {
                FinishEffect();
                Destroy(this);
            }
        }

        public abstract float GetDuration();

        protected abstract void StartEffect();

        protected abstract void FinishEffect();

        protected abstract int GetId();
    }
}