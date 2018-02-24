using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RecordProperty
{
    public class IBind : MonoBehaviour
    {
        public DataContext DataContext = null;
        public string Path;
        public bool IsAutoDataContext = true;
        public bool IsBindDone = false;
        public bool IsDebug = false;

        protected virtual void Awake()
        {
            if (false == IsAutoDataContext && null == DataContext)
            {
                Debug.Log("非自动绑定 需要手动设置Data Context");
                throw new System.NotSupportedException();
            }
            if (true == IsAutoDataContext)
            {
                SetAutoNearDataContext();
            }
        }

        protected void SetAutoNearDataContext()
        {
            DataContext = null;
            var p = gameObject;
            while (p != null)
            {
                var context = p.GetComponent<DataContext>();
                if (context != null)
                {
                    SetContext(context);
                    break;
                }
                p = (p.transform.parent == null) ? null : p.transform.parent.gameObject;
            }
            return;
        }

        public void SetContext(DataContext context)
        {
            if (DataContext != context)
            {
                Unbind();
                DataContext = context;
                if (null != DataContext) {
                    if (null != DataContext.GetContext())
                    {
                        Bind();
                        OnChange();
                    }
                    else {
                        Debug.LogWarning("the context is null, may be will bind laterly");
                        DataContext._context.OnPropertyChange += UpdateBind;
                    }
                }
                    
            }
            if (null == DataContext)
            {
                Debug.LogError("the data context is null, this is error for auto bind");
            }
        }

        public Context GetContext()
        {
            return DataContext.GetContext();
        }

        protected virtual void Unbind()
        {
            IsBindDone = false;
        }

        protected virtual void OnChange()
        {

        }

        protected virtual void Bind()
        {
            IsBindDone = true;
        }

        protected void UpdateBind() {
            Bind();
            OnChange();
            DataContext._context.OnPropertyChange -= UpdateBind;
        }
    }
}