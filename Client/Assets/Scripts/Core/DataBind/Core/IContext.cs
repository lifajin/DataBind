using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RecordProperty {
    public class Context {
        public IProperty FindProperty(string path)
        {
            return PropertyFinder.FindProperty(this, path);
        }

        public ICollection FindCollection(string path)
        {
            return PropertyFinder.FindCollection(this, path);
        }

        public Delegate FindCommand(string path)
        {
            return PropertyFinder.FindCommand(this, path);
        }

        public virtual void OnContextInCollectionSelected() { }
        public virtual void OnContextInCollectionUnSelected() { }
    }

}
