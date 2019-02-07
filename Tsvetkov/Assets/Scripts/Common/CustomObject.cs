using System;

namespace DefaultNamespace
{
    /// <summary>
    /// Data transfer object
    /// </summary>
    public sealed class CustomObject
    {
        private readonly object _value;

        public CustomObject(Object value)
        {
            _value = value;
        }

        public object Value
        {
            get { return _value; }
        }
    }
}