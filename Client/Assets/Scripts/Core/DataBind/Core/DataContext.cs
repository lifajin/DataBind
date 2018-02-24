using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RecordProperty
{
    public class ContextProperty : PropertyBase<Context>{
    }


    public class DataContext : MonoBehaviour
    {
        [HideInInspector]
        public ContextProperty _context = new ContextProperty();

        public void SetContext(Context context)
        {
            _context.Value = context;
        }

        public Context GetContext()
        {
            return _context.Value;
        }
    }

}