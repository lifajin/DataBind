using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RecordProperty
{
    public class ClickCommandBinding : RecordProperty.IBind, IPointerClickHandler
    {
        protected System.Delegate _command;

        public void OnPointerClick(PointerEventData eventData)
        {
            TrggerCommand(eventData);
        }

        protected override void Unbind()
        {
            _command = null;
            base.Unbind();
        }

        protected override void Bind()
        {
            _command = GetContext().FindCommand(Path);
            if (null == _command) {
                throw new System.NotSupportedException();
            }
            base.Bind();
        }

        public void TrggerCommand(BaseEventData data)
        {
            if (_command == null)
            {
                return;
            }
            _command.DynamicInvoke(data);
        }

        protected virtual EventTriggerType GetBindTriggerType()
        {
            throw new System.NotImplementedException();
        }
    }

}
