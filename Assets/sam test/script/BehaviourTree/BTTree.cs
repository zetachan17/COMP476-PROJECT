using System;
using UnityEngine;

namespace BT {

    [Serializable]
    public abstract class Tree : MonoBehaviour
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

        protected abstract Node SetupTree();
    }

}