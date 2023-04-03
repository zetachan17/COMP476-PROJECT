using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class strg_steerinAgent : MonoBehaviour
{

    public Vector3 Velocity { get; set; }
    public float mass;
    public float maxSpeed;

    public GameObject targetMoveAway;
    public GameObject targetMoveToward;

    private strg_seek  seekScript;
    private strg_wander wandeScript;
    private strg_pursue pursueScript;
    private strg_flee fleeScript;
    private strg_evade evadeScript;
    private collisionRayCast collisionDetection;
    public Vector3 acceleration = Vector3.zero;

    public GameObject debugTarget;
    public bool debugFleeToogle;

    public float debugFleeWeight;
    // Start is called before the first frame update
    void Start()
    {
        // Need to find a better way to handle the steering script 
        seekScript = GetComponent<strg_seek>();
        wandeScript = GetComponent<strg_wander>();
        collisionDetection = GetComponent<collisionRayCast>();
        pursueScript = GetComponent<strg_pursue>();
        fleeScript = GetComponent<strg_flee>();
        evadeScript = GetComponent<strg_evade>();
    }

    // Update is called once per frame
    void Update()
    {
       //checkDistanceFromtarget();

        steeringCalculation();

        if (Input.GetKey(KeyCode.C))
        {

            // this.targetMoveToward = debugTarget;
            float valueOne = 10 + 4 * 5;
            float valueTwo = 10;
            valueTwo += 4;
            valueTwo *= 5;
            float valuethree = 10;
            valuethree += 4 * 5;

            //print("Value one : " + valueOne);
            //print("Value two : " + valueTwo);
            //print("Value three : " + valuethree);
            //  seeker.gameObject.GetComponent<SteeringAgent>().getAssigneCoin(coinObject);
            // agenOne.GetComponent<SteeringV2>().target = newtarget;
            //AgentTwo.GetComponent<SteeringV2>().target = newtarget;
        }
    }

    public void steeringCalculation()
    {
        acceleration = Vector3.zero;

        if (debugFleeToogle == true)
        {
            acceleration += evadeScript.getSteering(debugFleeWeight, this);
            acceleration += pursueScript.getSteering(1, this);
        }
        else
        {

         acceleration += pursueScript.getSteering(1, this);
        }

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
       // this.transform.Rotate(0.0f,0.0f,1.0f, Space.Self);
    }

    public Quaternion faceForward(strg_steerinAgent agent)
    {
        if (agent.Velocity == Vector3.zero)
        {
            return agent.transform.rotation;
        }
        // have to specify wich direction is upward to prevent the object from fliping then reachin 90 and -90 on x axis
        return Quaternion.LookRotation(agent.Velocity, agent.transform.up);
    }

    /// <summary>
    /// The fonction is called to get acess to a specific  type of seek behaviour from and outside file.
    /// </summary>
    /// <param name="position">Vector3 of the position we want the agent to seek toward.</param>
    /// <param name="type">Int value that represent the type of seek behaviour we want to use.</param>
    /// <returns></returns>
    public Vector3 useSeek(Vector3 position, int type)
    {
        Vector3 returnedSteering = Vector3.zero;
        switch (type)
        {
            case 1:
                returnedSteering = seekScript.kinematickSeek(this, position);
                break;
        }

        return returnedSteering;
    }


    /// <summary>
    /// The fonction is called to get acess to a specific  type of flee behaviour from and outside file.
    /// </summary>
    /// <param name="position">Vector3 of the position we want the agent to flww from.</param>
    /// <param name="type">Int value that represent the type of flee behaviour we want to use.</param>
    /// <returns></returns>
    public Vector3 useFlee(Vector3 position, int type)
    {
        Vector3 returnedSteering = Vector3.zero;
        switch (type)
        {
            case 1:
                returnedSteering = fleeScript.kinematickFlee(this, position);
                break;
        }

        return returnedSteering;
    }



    //------------------------ Debug section , the folowing function are not suppose to be here in the final verions of the program

    private void checkDistanceFromtarget()
    {
        float distance = (transform.position - targetMoveToward.transform.position).magnitude;
        if (distance <= 3)
        {
            targetMoveToward = GetComponent<selectRandomTarget>().returnNewTarget();
        }
    }

}
