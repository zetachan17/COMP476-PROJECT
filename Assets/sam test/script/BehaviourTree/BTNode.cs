using System;
using System.Collections.Generic;
using UnityEngine;

namespace BT
{
    [Serializable]
    public enum NodeState
    {
        NONE,
        RUNNING,
        SUCCESS,
        FAILURE
    }

    [Serializable]
    public class Node
    {
        [SerializeField] protected NodeState state;

        [SerializeField] public Node parent;
        [SerializeField] protected List<Node> children = new();

        private Dictionary<string, object> _dataContext = new Dictionary<string, object>();

        public Node()
        {
            parent = null;
        }
        public Node(List<Node> children)
        {
            foreach (var child in children)
                _Attach(child);
        }

        private void _Attach(Node node)
        {
            node.parent = this;
            children.Add(node);
        }

        public virtual NodeState Evaluate() => NodeState.FAILURE;

        public void SetData(string key, object value)
        {
            _dataContext[key] = value;
        }

        public T GetData<T>(string key) where T : class
        {
            object value = null;
            if (_dataContext.TryGetValue(key, out value))
                return value as T;

            var node = parent;
            while (node != null)
            {
                value = node.GetData<T>(key);
                if (value != null)
                    return value as T;
                node = node.parent;
            }
            return null;
        }

        public bool ClearData(string key)
        {
            if (_dataContext.ContainsKey(key))
            {
                _dataContext.Remove(key);
                return true;
            }

            var node = parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                    return true;
                node = node.parent;
            }
            return false;
        }
    }
}
