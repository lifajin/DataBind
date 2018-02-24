using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;

namespace RecordProperty
{
    public class CollectionListContext : Context
    {
        public delegate void OnCollectionDelegat(CollectionListContext context);

        public readonly StringProperty MsgProperty = new StringProperty();
        public string Msg {
            get { return MsgProperty.GetValue(); }
            set { MsgProperty.SetValue(value); }
        }

        private readonly CollectionProperty<ItemInCollectionContext> OneCollectionProperty = new CollectionProperty<ItemInCollectionContext>();
        public CollectionProperty<ItemInCollectionContext> OneCollection {
            get { return OneCollectionProperty; }
        }

        #region Command
        public void OnAddItemClick(BaseEventData data)
        {
            ItemInCollectionContext item = new ItemInCollectionContext();
            item.Name = UnityEngine.Random.Range(1, 1000).ToString();
            item.ImageColor = ImageBinding.GetColorFormatString(Color.blue);
            item.IsClickedEnable = false;
            item.onClickItemEvt += this.OnSubItemClicked;
            this.OneCollection.Add(item);
            this.Msg = "add item done";
        }
        #endregion

        #region Command
        public void OnClearItemClick(BaseEventData data)
        {
            this.OneCollection.Clear();
            this.Msg = "clear items done";
        }
        #endregion

        #region Command
        public void OnRemoveItemClick(BaseEventData data)
        {
            if (-1 != this.OneCollection.SelectedIndex)
            {
                this.OneCollection.Remove(this.OneCollection.SelectedItem);
                this.Msg = "remove the item done";
            }
            else
            {
                this.Msg = "please select item first";
            }
        }
        #endregion

        #region Command
        public void OnUpdateItemClick(BaseEventData data)
        {
            if (-1 != this.OneCollection.SelectedIndex)
            {
                this.OneCollection.GetItem(this.OneCollection.SelectedIndex).Name = "rand" + UnityEngine.Random.Range(1, 100).ToString();
                this.Msg = "random update item done";
            }
            else
            {
                this.Msg = "please select item first";
            }
        }
        #endregion

        #region Command
        public void OnSubItemClicked(ItemInCollectionContext item)
        {
            this.OneCollection.Select(item);
            this.Msg = "select item done";
        }
        #endregion

        public void OnListScroll(BaseEventData data)
        {
            Debug.Log("OnScroll");
        }
    }

    public delegate void OnItemInCollectionDelegat(ItemInCollectionContext context);
    public class ItemInCollectionContext : Context
    {
        private readonly StringProperty NameProperty = new StringProperty();
        public string Name
        {
            get { return NameProperty.GetValue(); }
            set { NameProperty.SetValue(value); }
        }

        private readonly StringProperty ImageColorProperty = new StringProperty();
        public string ImageColor
        {
            get { return ImageColorProperty.GetValue(); }
            set { ImageColorProperty.SetValue(value); }
        }

        private readonly BoolProperty IsClickedEnableProperty = new BoolProperty();
        public bool IsClickedEnable
        {
            get { return IsClickedEnableProperty.GetValue(); }
            set { IsClickedEnableProperty.SetValue(value); }
        }

        #region Command Hover
        public event OnItemInCollectionDelegat onHoverItemEvt;
        public void OnHoverItem(BaseEventData data)
        {
            if (null != onHoverItemEvt) onHoverItemEvt(this);
            this.ImageColor = ImageBinding.GetColorFormatString(Color.yellow);
        }
        #endregion

        #region Command Hover
        public event OnItemInCollectionDelegat onExitItemEvt;
        public void OnExitItem(BaseEventData data)
        {
            if (null != onExitItemEvt) onExitItemEvt(this);
            this.ImageColor = ImageBinding.GetColorFormatString(Color.blue);
        }
        #endregion

        #region Command Click
        public event OnItemInCollectionDelegat onClickItemEvt;
        public void OnClickItem(BaseEventData data)
        {
            if (null != onClickItemEvt) onClickItemEvt(this);
        }
        #endregion

        public override void OnContextInCollectionSelected()
        {
            this.IsClickedEnable = true;
        }

        public override void OnContextInCollectionUnSelected()
        {
            this.IsClickedEnable = false;
        }
    }

}
