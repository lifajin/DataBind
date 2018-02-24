using System;
using System.Collections.Generic;

namespace RecordProperty
{
    public abstract class ICollection {
        public int ItemsCount;

        public delegate void ItemInsertDelegate(int position, Object insertedItem);
        public delegate void ItemRemoveDelegate(int position);
        public delegate void ItemsClearDelegate();
        public delegate void SelectionChangeDelegate(int form, int to);

        public event ItemInsertDelegate OnItemInsert;
        public event ItemRemoveDelegate OnItemRemove;
        public event ItemsClearDelegate OnItemsClear;
        public event SelectionChangeDelegate OnSelectionChange;

        protected void InvokeOnItemInsert(int position, object insertedItem)
        {
                if(null != OnItemInsert) OnItemInsert(position, insertedItem);
        }

        protected void InvokeOnItemRemove(int position)
        {
            if (null != OnItemRemove) OnItemRemove(position);
        }

        protected void InvokeOnItemsClear()
        {
            if (null != OnItemsClear) OnItemsClear();
        }

        protected void InvokeOnSelectionChange(int from, int to)
        {
            if (null != OnSelectionChange) OnSelectionChange(from, to);
        }

        public abstract Object GetRawItem(int index);
    }

    public class CollectionProperty<T> : ICollection
                where T : Context
    {
        protected List<T> _items = new List<T>();

        public int SelectedIndex = -1;

        public new int ItemsCount {
            get {
                return _items.Count;
            }
        }

        public T SelectedItem {
            get {
                return _items[SelectedIndex];
            }
        }

        public override Object GetRawItem(int index) {
            return _items[index];
        }

        public T GetItem(int index) {
           return _items[index];
        }

        public bool GetItem(int index, out T v) {
            v = default(T);
            if (index < 0 || index >= _items.Count)
                return false;
            v = _items[index];
            return true;
        }

        public void Add(T item) {
            if (0 == _items.Count) {
                Insert(0, item);
                return;
            }
            Insert(_items.Count - 1, item);
            return;
        }

        public void Clear() {
            Select(-1);
            _items.Clear();
            InvokeOnItemsClear();
        }

        public bool Contains(T item) {
            return _items.Contains(item);
        }

        public bool Remove(T item) {
            var index = _items.IndexOf(item);
            if (-1 == index) {
                return false;
            }
            return RemoveAt(index);
        }

        public bool Insert(int index, T item) {
            if (index < 0 || (index >= _items.Count && 0 != index))
                return false;
            InvokeOnItemInsert(index, item);
            _items.Insert(index, item);
            if (index <= SelectedIndex) {
                Select(SelectedIndex+1);
            }
            return true;
        }

        public bool RemoveAt(int index) {
            if (index < 0 || index >= _items.Count)
                return false;
            if (SelectedIndex == index) {
                Select(-1);
            }
            _items.RemoveAt(index);
            InvokeOnItemRemove(index);
            return true;
        }

        public void Select(int index) {
            var fromIndex = SelectedIndex;
            if (fromIndex != index)
            {
                if (-1 != SelectedIndex) {
                    SelectedItem.OnContextInCollectionUnSelected();
                }
                InvokeOnSelectionChange(fromIndex, index);
            }
            SelectedIndex = index;
            if (-1 != SelectedIndex)
            {
                SelectedItem.OnContextInCollectionSelected();
            }
        }

        public void Select(T item) {
            var toIndex = _items.IndexOf(item);
            Select(toIndex);
        }
    }
}
