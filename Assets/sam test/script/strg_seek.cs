using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class strg_seek : MonoBehaviour
{
    public float driftRatio;
    public float maxTurnAngle = 30f;
    public Vector3 getSteering( float weight, strg_steerinAgent agent)
   {
        Vector3 desiredVelocity = agent.targetMoveToward.transform.position - agent.transform.position;
        desiredVelocity = desiredVelocity.normalized * agent.maxSpeed;

        // Calculate the angle between the current velocity and the desired velocity
        float angle = Vector3.Angle(agent.Velocity, desiredVelocity);

        // Limit the turn angle to maxTurnAngle
        float turnAngle = Mathf.Min(angle, maxTurnAngle);

        // Calculate the new desired velocity with the limited turn angle
        Vector3 newDesiredVelocity = Vector3.RotateTowards(desiredVelocity, agent.Velocity, turnAngle * Mathf.Deg2Rad, 0);
        newDesiredVelocity = newDesiredVelocity.normalized * agent.maxSpeed;

        Vector3 steering = newDesiredVelocity - agent.Velocity;

        return steering * weight;
    }

    public Vector3 seekSpecificPointNoDrift(float weight, strg_steerinAgent agent, Vector3 targetPoss) {

        Vector3 desiredVelocity = (agent.targetMoveToward.transform.position - targetPoss);
        desiredVelocity = desiredVelocity.normalized * agent.maxSpeed * driftRatio;
        Vector3 steering = desiredVelocity - agent.Velocity;

        return steering * weight;
        //return Vector3.zero;
    }
}
