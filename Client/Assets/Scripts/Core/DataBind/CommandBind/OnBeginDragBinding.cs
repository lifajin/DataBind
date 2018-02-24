﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace RecordProperty
{
public class OnBeginDragBinding : RecordProperty.CommandBind {
	protected override void Start(){
		base.Start ();
	}

	protected override EventTriggerType GetBindTriggerType(){
		return EventTriggerType.BeginDrag;
	}
}

}
