using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

namespace RecordProperty
{
    public enum ImageBindType
    {
        Bind_Sprite,
        Bind_Color,
    }

    [RequireComponent(typeof(Image))]
    public class ImageBinding : RecordProperty.PropertyBind
    {
        protected Image image;
        public ImageBindType bindType = ImageBindType.Bind_Sprite;
        protected override void Awake()
        {
            image = GetComponent<Image>();
            base.Awake();
        }


        protected override void OnChange()
        {
            base.OnChange();
            var newValue = GetStringValue();
            ApplyNewValue(newValue);
        }

        protected virtual void ApplyNewValue(string newValue)
        {
            if (null != image)
            {
                if (ImageBindType.Bind_Sprite == bindType)
                {
                    image.sprite = Resources.Load(newValue, typeof(Sprite)) as Sprite;
                    image.overrideSprite = Resources.Load(newValue, typeof(Sprite)) as Sprite;
                }
                if (ImageBindType.Bind_Color == bindType)
                {
                    string[] rgba = newValue.Split(':');
                    if (3 == rgba.Length)
                    {
                        image.color = new Color(float.Parse(rgba[0]), float.Parse(rgba[1]), float.Parse(rgba[2]));
                    }
                    if (4 == rgba.Length)
                    {
                        image.color = new Color(float.Parse(rgba[0]), float.Parse(rgba[1]), float.Parse(rgba[2]), float.Parse(rgba[3]));
                    }
                }
            }
        }

        public static string GetColorFormatString(Color c)
        {
            return c.r.ToString() + ':' + c.g.ToString() + ':' + c.b.ToString() + ':' + c.a.ToString();
        }
    }

}

