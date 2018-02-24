using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RecordProperty
{
    public class CollectionBind : IBind
    {

        public GameObject Template;

        protected ICollection _collection;

        protected override void Unbind()
        {
            if (_collection != null)
            {
                _collection.OnItemInsert -= OnItemInsert;
                _collection.OnItemRemove -= OnItemRemove;
                _collection.OnItemsClear -= OnItemsClear;
                _collection.OnSelectionChange -= OnCollectionSelectionChange;
                _collection = null;
                OnItemsClear();
            }
            base.Unbind();
        }

        protected override void Bind()
        {
            _collection = GetContext().FindCollection(Path);
            if (_collection == null)
            {
                Debug.Log("collection bind path " + Path + " error");
                throw new System.NotSupportedException();
            }
                

            _collection.OnItemInsert += OnItemInsert;
            _collection.OnItemRemove += OnItemRemove;
            _collection.OnItemsClear += OnItemsClear;
            _collection.OnSelectionChange += OnCollectionSelectionChange;

            for (var i = 0; i < _collection.ItemsCount; ++i)
            {
                OnItemInsert(i, _collection.GetRawItem(i));
            }
            OnCollectionSelectionChange(-1, -1);
            base.Bind();
        }

        protected virtual void OnItemInsert(int position, System.Object insertedItem)
        {
            if (null == Template)
            {
                Debug.LogError("OnItemInsert Template is null");
                return;
            }

            GameObject itemObject = Instantiate(insertedItem as Context, position);
            if (itemObject == null)
                return;
            itemObject.name = string.Format("{0}", position);
            for (var i = 0; i < transform.childCount; ++i)
            {
                var child = transform.GetChild(i).gameObject;
                int childNumber;
                if (int.TryParse(child.name, out childNumber) && childNumber >= position)
                {
                    child.name = string.Format("{0}", childNumber + 1);
                }
            }
            itemObject.transform.SetParent(gameObject.transform);
            itemObject.transform.localScale = gameObject.transform.localScale;
            itemObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
            itemObject.transform.position = gameObject.transform.position;
        }

        protected virtual void OnItemRemove(int position)
        {
            for (var i = 0; i < transform.childCount; ++i)
            {
                var child = transform.GetChild(i).gameObject;
                int childNumber;
                if (int.TryParse(child.name, out childNumber))
                {
                    if (childNumber == position)
                    {
                        GameObject.DestroyImmediate(child);
                        break;
                    }
                }
            }
            for (var i = 0; i < transform.childCount; ++i)
            {
                var child = transform.GetChild(i).gameObject;
                int childNumber;
                if (int.TryParse(child.name, out childNumber))
                {
                    if (childNumber > position)
                    {
                        child.name = string.Format("{0}", childNumber - 1);
                    }
                }
            }
        }

        protected virtual void OnItemsClear()
        {
            while (transform.childCount > 0)
            {
                GameObject.DestroyImmediate(transform.GetChild(0).gameObject);
            }
        }

        protected virtual void OnCollectionSelectionChange(int form, int to)
        {
            int index = to;
        }

        public GameObject Instantiate(Context itemContext, int index)
        {
            if (Template == null)
            {
                return null;
            }
            Template.SetActive(false);

            GameObject instance = (GameObject)Instantiate(Template);
            var itemData = instance.GetComponent<DataContext>();
            if (null == itemData)
            {
                itemData = instance.AddComponent<DataContext>();
            }

            instance.SetActive(true);
            instance.transform.SetParent(transform);
            itemData.SetContext(itemContext);
            instance.transform.SetParent(null);

            //		RectTransform rect = instance.GetComponent<RectTransform> ();
            //		Debug.Log ("the scale is "+rect.localScale);
            //		Debug.Log ("the position is "+rect.position + " local position is "+rect.localPosition);

            var bindings = instance.GetComponentsInChildren<IBind>();
            foreach (var binding in bindings)
            {
                binding.SetContext(itemData);
            }
            //itemData.SetIndex(index);
            return instance;
        }
    }
}