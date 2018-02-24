using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
namespace RecordProperty
{
public class OnPointetUpBinding : RecordProperty.CommandBind
{
	protected override void Start(){
		base.Start ();
	}

	protected override EventTriggerType GetBindTriggerType(){
		return EventTriggerType.PointerExit;
	}
}
}

