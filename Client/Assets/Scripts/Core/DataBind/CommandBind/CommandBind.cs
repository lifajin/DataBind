using RecordProperty;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
namespace RecordProperty
{
    [RequireComponent(typeof(EventTrigger))]
    public class CommandBind : IBind
    {
        protected System.Delegate _bindCommand;

        protected virtual void Start()
        {
            EventTrigger trigger = GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = GetBindTriggerType();
            entry.callback.AddListener((data) =>
            {
                TrggerCommand(data);
            });
            trigger.triggers.Add(entry);
        }

        protected override void Unbind()
        {
            _bindCommand = null;
            base.Unbind();
        }

        protected override void OnChange()
        {

        }

        protected override void Bind()
        {
            _bindCommand = GetContext().FindCommand(Path);
            if (null == _bindCommand)
            {
                Debug.Log("command bind path " + Path + " error");
                throw new System.NotSupportedException();
            }
            base.Bind();
        }

        public void TrggerCommand(BaseEventData data)
        {
            if (_bindCommand == null)
            {
                return;
            }
            _bindCommand.DynamicInvoke(data);
        }

        protected virtual EventTriggerType GetBindTriggerType()
        {
            throw new System.NotImplementedException();
        }
    }
}