using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

namespace RecordProperty
{
    [RequireComponent(typeof(Image))]
    public class ImageFillBinding : RecordProperty.PropertyBind
    {
        protected Image image;

        void Awake()
        {
            image = GetComponent<Image>();
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
            if (null != image)
            {
                image.fillAmount = newValue;
            }
        }
    }

}
