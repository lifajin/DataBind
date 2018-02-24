using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

namespace RecordProperty
{

public enum EnableBindValueType{
	Bind_Boolean,
	Bind_IntEqual,
    Bind_IntGreater,
    Bind_IntLess
}

public class EnableGameObjectBinding : RecordProperty.PropertyBind
{
	public EnableBindValueType valueType = EnableBindValueType.Bind_Boolean;
	public bool boolValue = true;
	public int intValue = 0;

	protected override void OnChange(){
		base.OnChange();
        if (EnableBindValueType.Bind_Boolean == valueType)
        {
            var newValue = GetBoolValue();
            ApplyNewValue(newValue);
        }
        else if (EnableBindValueType.Bind_IntEqual == valueType)
        {
            var newValue = GetIntValue();
            ApplyNewValue(newValue);
        }
        else if (EnableBindValueType.Bind_IntGreater == valueType)
        {
            gameObject.SetActive(GetIntValue() >= intValue);
        }
        else if (EnableBindValueType.Bind_IntLess == valueType)
        {
            gameObject.SetActive(GetIntValue() <= intValue);
        }
    }

	protected virtual void ApplyNewValue(bool newValue){
		gameObject.SetActive(newValue == boolValue);
	}

	protected virtual void ApplyNewValue(int newValue){
		gameObject.SetActive(newValue == intValue);
	}
}

}
