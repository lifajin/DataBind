using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
namespace RecordProperty
{
    public enum ToggleValueType
    {
        Bind_Boolean,
        Bind_Enum,
    }

    [RequireComponent(typeof(Toggle))]
    public class ToggleBinding : RecordProperty.PropertyBind
    {
        protected Toggle toggle;
        public ToggleValueType bindValueType;
        public int bindValue = 0;

        protected override void Awake()
        {
            base.Awake();
            if (ToggleValueType.Bind_Boolean == bindValueType)
            {
                bindValue = 1;
            }
            if (ToggleValueType.Bind_Enum == bindValueType)
            {
                if (0 > bindValue)
                {
                    Debug.LogError("the bind value must be enum int large than zero");
                }
            }
            toggle = GetComponent<Toggle>();
            if (null != toggle)
            {
                toggle.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
            }
        }

        public void ValueChangeCheck()
        {
            //Debug.Log("toggle "+name+" Value Changed");
            if (ToggleValueType.Bind_Boolean == bindValueType)
            {
                SetValue(toggle.isOn.ToString());
            }
            if (ToggleValueType.Bind_Enum == bindValueType)
            {
                if (toggle.isOn)
                {
                    SetValue(bindValue.ToString());
                }
            }
        }

        protected override void OnChange()
        {
            base.OnChange();
            if (ToggleValueType.Bind_Boolean == bindValueType)
            {
                bool s = GetBoolValue();
                ApplyNewValue(s);
            }
            if (ToggleValueType.Bind_Enum == bindValueType)
            {
                var newValue = GetIntValue();
                ApplyNewValue(newValue);
            }
        }

        protected virtual void ApplyNewValue(bool newValue)
        {
            if (null != toggle)
            {
                toggle.isOn = newValue;
            }
        }

        protected virtual void ApplyNewValue(int newValue)
        {
            if (null != toggle)
            {
                toggle.isOn = bindValue == newValue;
            }
        }
    }

}
