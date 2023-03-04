using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class strg_wander : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 lastWanderDirection;
    Vector3 lastDisplacement;
    float wanderTimer;
    public float wanderInterval;
    public float wanderDegreesDelta;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 KinematicWander( strg_steerinAgent agent)
    {
        wanderTimer += Time.deltaTime;

        if(lastWanderDirection == Vector3.zero)
        {
            lastWanderDirection = transform.forward.normalized * agent.maxSpeed;
        }

        if (lastDisplacement == Vector3.zero)
        {
            lastDisplacement = transform.forward;
        }

        Vector3 desiredVelocity = lastDisplacement;

        if (wanderTimer > wanderInterval)
        {
            float angle = (Random.value - Random.value) * wanderDegreesDelta;
            Vector3 direction = Quaternion.AngleAxis(angle, Vector3.up) * lastWanderDirection.normalized;
            Vector3 circleCenter = transform.position + lastDisplacement;
            Vector3 destination = 
        }
       
    }
}
