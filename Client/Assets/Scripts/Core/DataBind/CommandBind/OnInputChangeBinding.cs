using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace RecordProperty
{
[RequireComponent (typeof (InputField))]
public class OnInputChangeBinding : RecordProperty.CommandBind
{
	private InputField inputBind;

	// Use this for initialization
	void Start () {
		inputBind = GetComponent<InputField> ();
		//inputBind.onValueChanged 
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

}
