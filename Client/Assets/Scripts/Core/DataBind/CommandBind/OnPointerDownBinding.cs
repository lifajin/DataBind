﻿using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
namespace RecordProperty
{
public class OnPointerDownBinding : RecordProperty.CommandBind
{
	protected override void Start(){
		base.Start ();
	}

	protected override EventTriggerType GetBindTriggerType(){
		return EventTriggerType.PointerDown;
	}
}

}
