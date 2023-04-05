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

}
