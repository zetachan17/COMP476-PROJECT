using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class strg_steerinAgent : MonoBehaviour
{

    public Vector3 Velocity { get; set; }
    public float mass;
    public float maxSpeed;
    public GameObject targetMoveToward;

    private strg_seek  seekScript;
    private collisionRayCast collisionDetection;
    public Vector3 acceleration = Vector3.zero;

    public GameObject debugTarget;
    // Start is called before the first frame update
    void Start()
    {
        seekScript = GetComponent<strg_seek>();
        collisionDetection = GetComponent<collisionRayCast>();
    }

    // Update is called once per frame
    void Update()
    {
      //  checkDistanceFromtarget();

        steeringCalculation();

        if (Input.GetKey(KeyCode.C))
        {
            
                this.targetMoveToward = debugTarget;

            
            //  seeker.gameObject.GetComponent<SteeringAgent>().getAssigneCoin(coinObject);
            // agenOne.GetComponent<SteeringV2>().target = newtarget;
            //AgentTwo.GetComponent<SteeringV2>().target = newtarget;
        }
    }

    public void steeringCalculation()
    {
        acceleration = Vector3.zero;
        acceleration += seekScript.getSteering(2, this);

        Vector3[] wallToDoge = collisionDetection.vissionDetection();
        Vector3 tempAcc = Vector3.zero;

        foreach (Vector3 obstacle in wallToDoge)
        {
            if (obstacle != Vector3.zero)
            {
                tempAcc += seekScript.seekSpecificPointNoDrift(20, this, obstacle);

                //nbSteering++;
            }

        }
        acceleration += tempAcc;
        acceleration /= mass;
        Velocity += acceleration * Time.deltaTime;


        //Velocity += distanceDifference*Time.deltaTime;
        Velocity = Vector3.ClampMagnitude(Velocity, maxSpeed);
        // agentTagetViz.transform.position += Velocity* Time.deltaTime;
        this.transform.position += Velocity * Time.deltaTime;
        this.transform.rotation = faceForward(this);
    }

    public Quaternion faceForward(strg_steerinAgent agent)
    {
        if (agent.Velocity == Vector3.zero)
        {
            return agent.transform.rotation;
        }

        return Quaternion.LookRotation(agent.Velocity);
    }

    private void checkDistanceFromtarget()
    {
        float distance = (transform.position - targetMoveToward.transform.position).magnitude;
        if (distance <= 3)
        {
            targetMoveToward = GetComponent<selectRandomTarget>().returnNewTarget();
        }
    }
}
