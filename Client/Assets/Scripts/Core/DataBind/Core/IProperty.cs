using System;


namespace RecordProperty
{
    public delegate void PropertyChanged();

    public interface IProperty
    {
        event PropertyChanged OnPropertyChange;
    }

    public abstract class PropertyBase<T> : IProperty
    {
        public event PropertyChanged OnPropertyChange;

        protected T _value;


        protected virtual bool IsChanged(T value) {
            return !_value.Equals(value);
        }

        public T Value {
            get {
                return _value;
            }
            set {
                bool isChanged = false;
                if (null == _value && null != value) {
                    isChanged = true;
                }
                else {
                    isChanged = IsChanged(value);
                }
                _value = value;
                if (true == isChanged && null != OnPropertyChange) {
                    OnPropertyChange();
                }
            }
        }

        public T GetValue() {
            return Value;
        }

        public void SetValue(T v) {
            Value = v;
        }
    }

    public class FloatProperty : PropertyBase<float>
    {

        protected override bool IsChanged(float value)
        {
            return Math.Abs(Value - value) > 0.0001f;
        }
    }

    public class IntProperty : PropertyBase<int>
    {
        protected override bool IsChanged(int value)
        {
            return Value != value;
        }
    }

    public class DoubleProperty : PropertyBase<double>
    {
        protected override bool IsChanged(double value)
        {
            return Math.Abs(Value - value) > 0.000001;
        }
    }

    public class StringProperty : PropertyBase<string>
    {
        protected override bool IsChanged(string value)
        {
            return Value != value;
        }
    }

    public class BoolProperty : PropertyBase<bool>
    {
        protected override bool IsChanged(bool value)
        {
            return Value != value;
        }
    }
}
