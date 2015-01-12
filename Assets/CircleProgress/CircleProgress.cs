using System;
using System.Collections.Generic;
using Assets.MapBonus;
using UnityEngine;

namespace Assets.CircleProgress
{
    public class CircleProgress : MonoBehaviour
    {

        

        // Use this for initialization
        void Start ()
        {


        }


	
        // Update is called once per frame
        void Update () {
	
        }

        public void SetProgress(float progress)
        {
            float factor = Mathf.Lerp(-Mathf.PI, Mathf.PI, progress);
            gameObject.renderer.material.SetFloat("_Angle", factor);
        }

        public void SetColor(Color color)
        {
            gameObject.renderer.material.SetColor("_Color", color);
        }

        public void SetMaxRange(float modifier)
        {
            gameObject.renderer.material.SetFloat("_RangePercentMax", modifier * 100f);
        }
    }
}
