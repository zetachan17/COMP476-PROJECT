using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class strg_seek : MonoBehaviour
{
   
    private float maxTurnAngle = 50f;

    private void Start()
    {
        maxTurnAngle = GetComponent<StatTracked>().GetStat(StatTracked.Stat.MaxTurnAngle);
    }

    public Vector3 kinematickSeek(strg_steerinAgent agent, Vector3 tagetPosition)
    {
        Vector3 desiredVelocity = tagetPosition - agent.transform.position;
        desiredVelocity = desiredVelocity.normalized * agent.maxSpeed;

        // Calculate the angle between the current velocity and the desired velocity
        float angle = Vector3.Angle(agent.Velocity, desiredVelocity);

        // Limit the turn angle to maxTurnAngle
        float turnAngle = Mathf.Min(angle, maxTurnAngle);

        // Calculate the new desired velocity with the limited turn angle
        Vector3 newDesiredVelocity = Vector3.RotateTowards(desiredVelocity, agent.Velocity, turnAngle * Mathf.Deg2Rad, 0);
        newDesiredVelocity = newDesiredVelocity.normalized * agent.maxSpeed;

        return newDesiredVelocity;
    }

    //primary seek script.
    //Agent will move toward they are currently moving toward,
    //the possible turn angle is restricted by the macTurnAngle, lower angle = sharperTurn ( 90-maxTurnAngle = angle that the Agent can rotate toward)

    public Vector3 getSteering( float weight, strg_steerinAgent agent)
    {
        Vector3 newDesiredVelocity = kinematickSeek(agent , agent.targetMoveToward.transform.position);
        Vector3 steering = newDesiredVelocity - agent.Velocity;

        return steering * weight;
    }
    
    //Specific seek script to move toward a specified point. Primarly use to move away from wall
    public Vector3 seekSpecificPointNoDrift(float weight, strg_steerinAgent agent, Vector3 targetPoss) {

        Vector3 desiredVelocity = ( targetPoss - agent.transform.position );
        desiredVelocity = desiredVelocity.normalized * agent.maxSpeed;

        /*
        // Calculate the angle between the current velocity and the desired velocity
        float angle = Vector3.Angle(agent.Velocity, desiredVelocity);

        // Limit the turn angle to maxTurnAngle
        float turnAngle = Mathf.Min(angle, maxTurnAngle);

        // Calculate the new desired velocity with the limited turn angle
        Vector3 newDesiredVelocity = Vector3.RotateTowards(desiredVelocity, agent.Velocity, turnAngle * Mathf.Deg2Rad, 0);
        newDesiredVelocity = newDesiredVelocity.normalized * agent.maxSpeed;
        */
        Vector3 steering = desiredVelocity - agent.Velocity;

        return steering * weight;

    }


    public Vector3 missleSeek(moveMissle missle)
    {
        Vector3 desiredVelocity = missle.targetMoveToward.transform.position - missle.transform.position;
        desiredVelocity = desiredVelocity.normalized * missle.maxSpeed;


        Vector3 steering = desiredVelocity - missle.Velocity;
        return steering;



    }
    public void OnStatChange(StatTracked.Stat stat, float oldValue, float newValue)
    {
        if(stat == StatTracked.Stat.MaxTurnAngle)
        {
            maxTurnAngle = newValue;
        }
    }
}
