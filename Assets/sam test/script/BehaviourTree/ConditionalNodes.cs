using System;
using System.Collections.Generic;

namespace BT
{
    public class Selector : Node
    {
        public Selector() : base() { }
        public Selector(List<Node> children) : base(children) { }

        public override NodeState Evaluate()
        {
            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.SUCCESS:
                        return NodeState.SUCCESS;
                    case NodeState.RUNNING:
                        return NodeState.RUNNING;
                    case NodeState.FAILURE:
                        continue;
                    default:
                        continue;
                }
            }

            state = NodeState.FAILURE;
            return state;
        }

    }

    public class Sequence : Node
    {
        public Sequence() : base() { }
        public Sequence(List<Node> children) : base(children) { }

        public override NodeState Evaluate()
        {
            bool anyChildIsRunning = false;

            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        return NodeState.FAILURE;
                    case NodeState.RUNNING:
                        anyChildIsRunning = true;
                        return NodeState.RUNNING;
                    case NodeState.SUCCESS:
                        continue;
                    default:
                        continue;                
                }
            }

            state = anyChildIsRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return state;
        }

    }
}
