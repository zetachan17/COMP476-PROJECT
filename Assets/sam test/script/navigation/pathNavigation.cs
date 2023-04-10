using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathNavigation : MonoBehaviour
{
    public List<node> pathOfNode;
    public node lastNode;
    public node targetNode;
    private strg_steerinAgent _steeringAgent;

    private void Start()
    {
        _steeringAgent = GetComponent<strg_steerinAgent>();
        _steeringAgent.targetMoveToward = targetNode.gameObject;
    }
    public void setNewPath(List <node> list)
    {
        pathOfNode = list;
    }

    public void nodeCheck()
    {
        float distanceFromNextNode = Vector3.Distance(transform.position, targetNode.transform.position);
        if(distanceFromNextNode <= 10)
        {
            lastNode = targetNode;
            //get next node
            //targetNodeIsTheLastOne
            if (pathOfNode.Count <= 1)
            {
                // get an new target node
                node finalDestination = GameObject.Find("nodeList").GetComponent<nodeSelection>().getRandomNode();
                gameObject.GetComponent<strg_steerinAgent>().generalTarget = finalDestination.gameObject;
                pathOfNode = GameObject.Find("nodeList").GetComponent<pathFinding>().findPath( targetNode, finalDestination, new List<node>());
            }
            else
            {
                pathOfNode.RemoveAt(0);
            }
            
            targetNode = pathOfNode[0];
            _steeringAgent.targetMoveToward = targetNode.gameObject;

        }
        else
        {
            //No need to change the target node
        }
    }


    /// <summary>
    /// Function called when we want the ai to go back to navigating using the node list
    /// </summary>
    /// <param name="target"> GameObject of what node we want to reache </param>
    /// <param name="closestNodePosition"> GameObject reference of the closes node , the navigation path will start from here </param>
    public void findPathToTarget(GameObject target, GameObject closestNodePosition)
    {
        
        pathOfNode = GameObject.Find("nodeList").GetComponent<pathFinding>().findPath(closestNodePosition.GetComponent<node>(), target.GetComponent<node>(), new List<node>());

    }

}
