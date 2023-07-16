using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.UI;

namespace BurningFrost
{
    public class AmmoUI : MonoBehaviour, MMEventListener<GhostParameter>
    {
        [SerializeField] private Slider slider;
        [SerializeField] private Graphic fill;

        public void OnMMEvent(GhostParameter parameter)
        {
            if (parameter.isNull)
            {
                fill.color = Color.clear;
                return;
            }

            slider.maxValue = parameter.MaxFloat;
            slider.value = parameter.CurrentFloat;
            fill.color = parameter.Color;
        }

        /// <summary>
        /// On enable we start listening for events
        /// </summary>
        private void OnEnable()
        {
            this.MMEventStartListening();
        }

        /// <summary>
        /// On disable we stop listening for events
        /// </summary>
        private void OnDisable()
        {
            this.MMEventStopListening();
        }
    }
}
