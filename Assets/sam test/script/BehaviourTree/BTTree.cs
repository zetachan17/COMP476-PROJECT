using System;
using UnityEngine;

namespace BT {

    [Serializable]
    public  class Tree : MonoBehaviour
    {
        [SerializeField] protected Node root;

        public Tree()
        {
            this.root = SetupTree();
        }

        protected virtual void Update()
        {
            if (root is not null)
                root.Evaluate();
        }

        protected virtual Node SetupTree() { return null; }
    }

}