using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
namespace RecordProperty
{
    [RequireComponent(typeof(Slider))]
    public class SliderBinding : RecordProperty.PropertyBind
    {
        protected Slider slider;

        void Awake()
        {
            slider = GetComponent<Slider>();
            base.Awake();

        }


        protected override void OnChange()
        {
            base.OnChange();
            var newValue = GetFloatValue();
            ApplyNewValue(newValue);
        }

        protected virtual void ApplyNewValue(float newValue)
        {
            if (null != slider)
            {
                slider.value = newValue;
            }
        }
    }
}

