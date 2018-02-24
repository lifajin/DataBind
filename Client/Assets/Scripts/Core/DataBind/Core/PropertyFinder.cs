using System;
using System.Reflection;
using UnityEngine.EventSystems;

namespace RecordProperty
{
    public delegate void Command(BaseEventData data);

    public static class PropertyFinder
    {
        public static IProperty FindProperty(object node, string path) {
            return GetPropertyValue(node, path) as IProperty;
        }

        public static ICollection FindCollection(object node, string path)
        {
            return GetPropertyValue(node, path) as ICollection;
        }

        public static Delegate FindCommand(object node, string path)
        {
            return GetPropertyValue(node, path, true) as Delegate;
        }

        public static object GetPropertyValue(object srcobj, string propertyName, bool isFindMeath = false)
        {
            if (srcobj == null)
                return null;

            object obj = srcobj;

            // Split property name to parts (propertyName could be hierarchical, like obj.subobj.subobj.property
            string[] propertyNameParts = propertyName.Split('.');
            string propertyNamePart = "";
            for (int i=0; i<propertyNameParts.Length;++i) {
                propertyNamePart = propertyNameParts[i];
                if (obj == null) return null;

                // propertyNamePart could contain reference to specific 
                // element (by index) inside a collection
                if (!propertyNamePart.Contains("["))
                {
                    if (true == isFindMeath && i == propertyNameParts.Length-1) {
                        MethodInfo method = obj.GetType().GetMethod(propertyNamePart);
                        if (method == null) return null;
                        obj = Delegate.CreateDelegate(typeof(Command), obj, method);
                    }
                    else
                    {
                        FieldInfo pi = obj.GetType().GetField(propertyNamePart+"Property", BindingFlags.Public |BindingFlags.NonPublic | BindingFlags.Instance);
                        if (pi == null) return null;
                        obj = pi.GetValue(obj);
                    }

                }
                else
                { 
                    int indexStart = propertyNamePart.IndexOf("[") + 1;
                    string collectionPropertyName = propertyNamePart.Substring(0, indexStart - 1);
                    int collectionElementIndex = Int32.Parse(propertyNamePart.Substring(indexStart, propertyNamePart.Length - indexStart - 1));
                    //   get collection object
                    FieldInfo pi = obj.GetType().GetField(collectionPropertyName + "Property", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    if (pi == null) return null;
                    object unknownCollection = pi.GetValue(obj);
                    //   try to process the collection as array
                    if (unknownCollection.GetType().IsArray)
                    {
                        object[] collectionAsArray = unknownCollection as Array[];
                        obj = collectionAsArray[collectionElementIndex];
                    }
                    else
                    {
                        //   try to process the collection as ICollection
                        ICollection collectionAsList = unknownCollection as ICollection;
                        if (collectionAsList != null)
                        {
                            obj = collectionAsList.GetRawItem(collectionElementIndex);
                        }
                        else
                        {
                            // ??? Unsupported collection type
                        }
                    }
                }
            }

            return obj;
        }
    }
}
