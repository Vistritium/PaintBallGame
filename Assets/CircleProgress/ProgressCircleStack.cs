
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.CircleProgress
{
    public class ProgressCircleStack : MonoBehaviour {

        class CircleProgressWrapper
        {
            public CircleProgress circleProgress;
            public float timeLeft;
            public float duration;
           


            public CircleProgressWrapper(CircleProgress circleProgress, float duration)
            {
                this.circleProgress = circleProgress;
                this.timeLeft = duration;
                this.duration = duration;
            }
        }


        List<CircleProgressWrapper> circleProgresses = new List<CircleProgressWrapper>();
        private float scaleFactor = 1.3f;

        public void AddCircleProgress(Color color, float time)
        {
            var mapBonuses = (GameObject)GameObject.Find("Inactive");
            foreach (Transform child in mapBonuses.transform)
            {
                if (child.name == "ProgressCircle")
                {
                    GameObject newObj = (GameObject)Instantiate(child.gameObject);
                    newObj.SetActive(true);
                    var circleProgress = newObj.GetComponent<CircleProgress>();
                    circleProgress.SetColor(color);
                    circleProgresses.Add(new CircleProgressWrapper(circleProgress, time));
                    newObj.transform.position = this.transform.position;
                    newObj.transform.parent = this.transform;
                }
            }
            SizeUpdate();
        }

        void SizeUpdate()
        {
            for (int i = 0; i < circleProgresses.Count; i++)
            {
                var current = circleProgresses[i];
                Vector3 localScale = Vector3.one * (float)Math.Pow(scaleFactor, i) * 0.13f;
                current.circleProgress.gameObject.transform.localScale = localScale;

                float maxRange = 1 - 1 * i * 0.03f;
                Debug.Log(string.Format("maxRange for i {0} is {1}", i, maxRange));
                current.circleProgress.SetMaxRange(maxRange);
            }
        }

        // Use this for initialization
        void Start () {
	
        }

/*        void OnGUI()
        {
            if (GUI.Button(new Rect(0, 0, 100, 100), "Add circle of doom"))
            {
                this.AddCircleProgress(Color.red, 10f);
            }
        }*/
	
        // Update is called once per frame
        void Update ()
        {
            CircleProgressWrapper toRemove = null;
            foreach (var circleProgressWrapper in circleProgresses)
            {
                circleProgressWrapper.timeLeft -= Time.deltaTime;
                if (circleProgressWrapper.timeLeft < 0)
                {
                    toRemove = circleProgressWrapper;
                }
                circleProgressWrapper.circleProgress.SetProgress(circleProgressWrapper.timeLeft / circleProgressWrapper.duration);
            }
            if (toRemove != null)
            {
                Destroy(toRemove.circleProgress.gameObject);
                circleProgresses.Remove(toRemove);
                SizeUpdate();
            }
        }
    }
}
