using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class strg_steerinAgent : MonoBehaviour
{

    public Vector3 Velocity { get; set; }
    public float mass;
    public float maxSpeed;
    public float speed;
    public float rotationSpeed; //used to control the speed of the lookForward function
    public bool player;

    public GameObject targetMoveAway;

    public GameObject targetMoveToward;
    public GameObject generalTarget;
    public GameObject closestNode;

    private strg_seek  seekScript;
    private strg_wander wandeScript;
    private strg_pursue pursueScript;
    private strg_flee fleeScript;
    private strg_evade evadeScript;
    private strg_arrived arrivedScript;

    public aiAnimation _aiAnimationScript;
    private collisionRayCast collisionDetection;

    public Vector3 acceleration = Vector3.zero;
    private float rotationValue = 0.0f;

    public bool initialInitialisation = false;
    


    public GameObject debugTarget;
    public bool evadeToogle;
    public bool awayFromPath = false;

    public float debugFleeWeight;

    public Animator _animator;
    public Vector3 oldPosition;


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
        arrivedScript = GetComponent<strg_arrived>();

        if (player == false)
        {
           
            _aiAnimationScript = GetComponent<aiAnimation>();
        }



        initialiseAgent();

       

    }

    // Update is called once per frame
    void Update()
    {
        if(initialInitialisation == false)
        {
            initialiseAgent();
        }
        checkDistanceFromtarget();
        
        if(player == true)
        {
            steeringCalculation();
        }

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

        if(player == true)
        {
            acceleration += pursueScript.getSteering(2, this);
        }
        else if (awayFromPath)
        {

            // IF the ai is trying to reache a target, we use pursue.

            if (evadeToogle == true)
            {
                acceleration += evadeScript.getSteering(debugFleeWeight, this);
                
            }
           
            acceleration += pursueScript.getSteering(2, this);
            
        }
        else
        {
            // If the ai is just moving around by folowing the path of node, we use arrived script to prevent infinite looping around a node

             GetComponent<pathNavigation>().nodeCheck();

            if (evadeToogle == true)
            {
                acceleration += evadeScript.getSteering(debugFleeWeight, this);
            }

            acceleration += arrivedScript.getSteering(4, this);
            

        }


        if(player == false)
        {
            Vector3[] wallToDoge = collisionDetection.vissionDetection();
            Vector3 tempAcc = Vector3.zero;

            foreach (Vector3 obstacle in wallToDoge)
            {
                if (obstacle != Vector3.zero)
                {
                    tempAcc += seekScript.seekSpecificPointNoDrift(5, this, obstacle);

                    //nbSteering++;
                }

            }
            acceleration += tempAcc;

        }

        acceleration /= mass;
        Vector3 oldVelocity = Velocity;
        
        Velocity += acceleration * Time.deltaTime;
        //Velocity += distanceDifference*Time.deltaTime;
        Velocity = Vector3.ClampMagnitude(Velocity, maxSpeed);


        if (player == false)
        {
            
            //Debug.Log((Velocity- oldVelocity) + "Acceleration");
            Vector3 modifiedAngle = Velocity - oldVelocity;

            _aiAnimationScript.setAnimation(modifiedAngle);
        }

        // agentTagetViz.transform.position += Velocity* Time.deltaTime;
        float debugSpeed = (Velocity.x + Velocity.y + Velocity.z) / 3;
        oldPosition = this.transform.position;
        
        this.transform.position += Velocity * Time.deltaTime;
        this.transform.rotation = faceForward(this);
        this.transform.Rotate(0.0f,0.0f, rotationValue * rotationSpeed * Time.deltaTime, Space.Self);
        GetComponent<collisionWIthWall>().sphereCheckGround(oldPosition);
        speed = (Vector3.Distance(oldPosition, this.transform.position) / Time.deltaTime);
        speed = Mathf.Round(speed * 10.0f) * 0.1f;
       
        
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

    public void initialiseAgent()
    {
        
        node tempNodeLink = this.gameObject.GetComponent<findClosestNode>().getClosestNode(transform.position);
        closestNode = tempNodeLink.gameObject;
        if(closestNode != null)
        {
            initialInitialisation = true;
        }
        
        GetComponent<pathNavigation>().nodeCheck();
        maxSpeed = GetComponent<StatTracked>().GetStat(StatTracked.Stat.MaxSpeed);
    }

    // is called by StatTracked when a change of stat occure;
    public void OnStatChange(StatTracked.Stat stat, float oldValue, float newValue)
    {
        if (stat == StatTracked.Stat.MaxSpeed)
        {
            maxSpeed = newValue;
        }
    }

    /// <summary>
    /// General function that allow to change the working of the steering agent. Check value can be set depending on the case.
    /// </summary>
    /// <param name="action"> Int value that indicate wich specific set of action we want to apply</param>
    /// <param name="objectList"> List of GameObject that can be used to pass multiple object like target  and enemy to evade. The order depend on the function</param>
    public enum SteeringOptions {  Persue, Evade, Other }    
    public void setSteering(SteeringOptions action, List<GameObject> objectList)
    {
        switch(action){
            
            case SteeringOptions.Persue:
                //seeking a specific object
                evadeToogle = false;
                targetMoveToward = objectList[0].gameObject;
                awayFromPath = true;
                steeringCalculation();
                break;
            case SteeringOptions.Evade:
                evadeToogle = true;
                targetMoveAway = objectList[0].gameObject;
                awayFromPath = true;
                steeringCalculation();
                break;
            default:
                evadeToogle = false;
                if (awayFromPath)
                {
                    awayFromPath = false;
                    closestNode = GetComponent<findClosestNode>().getClosestNode(transform.position).gameObject;
                    GetComponent<pathNavigation>().findPathToTarget(closestNode, generalTarget);
                }
                steeringCalculation();
                break;
        }
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


    public void setRotationAxis( float side)
    {
        rotationValue = side;
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
