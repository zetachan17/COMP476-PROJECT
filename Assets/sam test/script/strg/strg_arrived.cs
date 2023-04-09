using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class strg_arrived : MonoBehaviour
{

    public float slowRadius;
    public float arrivedRadius;
    //  public float maxAccel; //max speed;
    public float timeToTarget;
    private float maxTurnAngle;

    // Start is called before the first frame update
    private void Start()
    {
        maxTurnAngle = GetComponent<StatTracked>().GetStat(StatTracked.Stat.MaxTurnAngle);
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 getSterringSeeking(float weight, strg_steerinAgent agent, Vector3 point)
    {
        Vector3 targetVelocity = Vector3.zero;

        Vector3 desiredVelocity = point - agent.transform.position;
        float distance = desiredVelocity.magnitude;
        desiredVelocity = desiredVelocity.normalized * agent.maxSpeed;

        if(distance <= arrivedRadius)
        {
            desiredVelocity *= 0;
        }else if(distance < slowRadius)
        {
            float angle = Vector3.Angle(agent.Velocity, desiredVelocity);

            // Limit the turn angle to maxTurnAngle
            float turnAngle = Mathf.Min(angle, 0);

            // Calculate the new desired velocity with the limited turn angle
            Vector3 newDesiredVelocity = Vector3.RotateTowards(desiredVelocity, agent.Velocity, turnAngle * Mathf.Deg2Rad, 0);
            desiredVelocity = newDesiredVelocity.normalized * agent.maxSpeed;

            //desiredVelocity *= (distance / slowRadius);
           
        }
        else
        {
            float angle = Vector3.Angle(agent.Velocity, desiredVelocity);
            float turnAngle = Mathf.Min(angle, maxTurnAngle);

            // Calculate the new desired velocity with the limited turn angle
            Vector3 newDesiredVelocity = Vector3.RotateTowards(desiredVelocity, agent.Velocity, turnAngle * Mathf.Deg2Rad, 0);
            desiredVelocity = newDesiredVelocity.normalized * agent.maxSpeed;
        }




        return desiredVelocity;
    }

    public Vector3 getSteering(float weight, strg_steerinAgent agent)
    {
        Vector3 newDesiredVelocity = getSterringSeeking(weight, agent, agent.targetMoveToward.transform.position);
        Vector3 steering = newDesiredVelocity - agent.Velocity;

        return steering * weight;
    }

    public void OnStatChange(StatTracked.Stat stat, float oldValue, float newValue)
    {
        if (stat == StatTracked.Stat.MaxTurnAngle)
        {
            maxTurnAngle = newValue;
        }
    }
}
