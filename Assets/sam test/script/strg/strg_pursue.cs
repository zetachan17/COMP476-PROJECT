using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class strg_pursue : MonoBehaviour
{
    private int pursueDistance = 10;
    private StatTracked _stat;
    // Start is called before the first frame update
    void Start()
    {
        _stat = GetComponent<StatTracked>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 getSteering(float weight, strg_steerinAgent agent)
    {

        float distance = Vector3.Distance(agent.targetMoveToward.transform.position, this.transform.position);
        float ahead = distance / pursueDistance;
        Vector3 futurPosition = Vector3.zero;
        //Make sure that the target have a velocity
        if (agent.targetMoveToward.GetComponent<strg_steerinAgent>())
        {
            

            futurPosition = agent.targetMoveToward.GetComponent<strg_steerinAgent>().Velocity;
            futurPosition *= ahead;
            futurPosition += agent.targetMoveToward.transform.position;
        }
        else
        {
            Vector3 targetVelZero = Vector3.zero;
            futurPosition = agent.targetMoveToward.transform.position + targetVelZero * ahead;

        }

        Vector3 steering = agent.useSeek(futurPosition, 1) - agent.Velocity;
        return steering * weight;
    }
}
