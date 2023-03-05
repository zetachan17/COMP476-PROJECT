using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class strg_evade : MonoBehaviour
{
    private int evadeDistance = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 getSteering(float weight, strg_steerinAgent agent)
    {

        float distance = Vector3.Distance( this.transform.position, agent.targetMoveAway.transform.position);
        float ahead = distance / evadeDistance;
        Vector3 futurPosition = Vector3.zero;
        //Make sure that the target have a velocity
        if (agent.targetMoveAway.GetComponent<strg_steerinAgent>())
        {


            futurPosition = agent.targetMoveAway.GetComponent<strg_steerinAgent>().Velocity;
            futurPosition *= ahead;
            futurPosition += agent.targetMoveAway.transform.position;
        }
        else
        {
            Vector3 targetVelZero = Vector3.zero;
            futurPosition = agent.targetMoveAway.transform.position + targetVelZero * ahead;

        }

        Vector3 steering = agent.useFlee(futurPosition, 1) - agent.Velocity;
        print(steering);
        return steering* weight;
    }
}
