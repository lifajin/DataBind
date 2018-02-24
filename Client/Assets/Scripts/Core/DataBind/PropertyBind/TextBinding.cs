using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
namespace RecordProperty
{
    [RequireComponent(typeof(Text))]
    public class TextBinding : RecordProperty.PropertyBind
    {
        public string Format = "{0}";
        protected Text text;

        void Awake()
        {
            text = GetComponent<Text>();
            base.Awake();
        }


        protected override void OnChange()
        {
            base.OnChange();
            var newValue = GetStringValue();
            ApplyNewValue(newValue);
        }

        protected virtual void ApplyNewValue(string newValue)
        {
            if (null != text)
            {
                text.text = newValue;
            }
        }
    }

}
