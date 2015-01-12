using AssemblyCSharp;
using Assets.CircleProgress;
using UnityEngine;

namespace Assets.Powerups
{
    [RequireComponent(typeof(ProgressCircleStack))]
    [RequireComponent(typeof (Player))]
    public abstract class Powerup : MonoBehaviour
    {
        private float timeLeft;

        // Use this for initialization
        protected virtual void Start()
        {
            float duration = GetDuration();
            timeLeft = duration;
            StartEffect();
            this.GetComponent<ProgressCircleStack>().AddCircleProgress(GetColor(), duration);
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

        protected abstract Color GetColor();

        protected abstract int GetId();
    }
}