using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace RecordProperty
{
public class OnEventTriggerBinding : RecordProperty.CommandBind
{
    public EventTriggerType m_EventType;

	protected override void Start(){
		base.Start ();
	}

	protected override EventTriggerType GetBindTriggerType(){
		return m_EventType;
	}
}

}
