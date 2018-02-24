using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RecordProperty;
namespace RecordProperty
{
    public class PropertyBind : IBind
    {
        protected IProperty _bindProperty;

        protected override void Unbind()
        {
            if (null != _bindProperty)
            {
                _bindProperty.OnPropertyChange -= OnChange;
            }
            base.Unbind();
        }

        protected override void OnChange()
        {

        }

        protected override void Bind()
        {
            _bindProperty = GetContext().FindProperty(Path);
            if (null == _bindProperty)
            {
                Debug.Log("property bind path " + Path + " error");
                throw new System.NotSupportedException();
            }
            _bindProperty.OnPropertyChange += OnChange;
            base.Bind();
        }

        public float GetFloatValue()
        {
            return (_bindProperty as FloatProperty).Value;
        }

        public int GetIntValue()
        {
            return (_bindProperty as IntProperty).Value;
        }

        public double GetDoubleValue()
        {
            return (_bindProperty as DoubleProperty).Value;
        }

        public string GetStringValue()
        {
            return (_bindProperty as StringProperty).Value;
        }

        public bool GetBoolValue()
        {
            return (_bindProperty as BoolProperty).Value;
        }

        public void SetValue(float value)
        {
            (_bindProperty as FloatProperty).Value = value;
        }

        public void SetValue(int value)
        {
            (_bindProperty as IntProperty).Value = value;
        }

        public void SetValue(double value)
        {
            (_bindProperty as DoubleProperty).Value = value;
        }

        public void SetValue(string value)
        {
            (_bindProperty as StringProperty).Value = value;
        }

        public void SetValue(bool value)
        {
            (_bindProperty as BoolProperty).Value = value;
        }
    }
}