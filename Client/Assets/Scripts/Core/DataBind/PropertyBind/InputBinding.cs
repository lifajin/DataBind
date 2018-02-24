using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace RecordProperty
{
    [RequireComponent(typeof(InputField))]
    public class InputBinding : RecordProperty.PropertyBind
    {
        protected InputField inputField;

        void Awake()
        {
            inputField = GetComponent<InputField>();
            if (null != inputField)
            {
                inputField.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
            }
            base.Awake();

        }

        public void ValueChangeCheck()
        {
            //Debug.Log("input Value Changed");
            SetValue(inputField.text);
        }

        protected override void OnChange()
        {
            base.OnChange();
            var newValue = GetStringValue();
            ApplyNewValue(newValue);
        }

        protected virtual void ApplyNewValue(string newValue)
        {
            if (null != inputField)
            {
                inputField.text = newValue;
            }
        }
    }

}
