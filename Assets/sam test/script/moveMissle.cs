using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveMissle : MonoBehaviour
{
    public GameObject targetMoveToward;
    public float maxSpeed;
    public Vector3 acceleration = Vector3.zero;
    public Vector3 Velocity { get; set; }

    [SerializeField] private strg_seek seekScript;
    [SerializeField] private strg_pursue pursueScript;
    // Start is called before the first frame update
    void Start()
    {
        seekScript = GetComponent<strg_seek>();
        pursueScript = GetComponent<strg_pursue>();
    }

    // Update is called once per frame
    void Update()
    {
        acceleration = Vector3.zero;
        acceleration += seekScript.missleSeek( this);

        Velocity += acceleration * Time.deltaTime;
        //Velocity += distanceDifference*Time.deltaTime;
        Velocity = Vector3.ClampMagnitude(Velocity, maxSpeed);

        this.transform.position += Velocity * Time.deltaTime;
        this.transform.rotation = faceForward(this);
        

    }
    public Quaternion faceForward(moveMissle agent)
    {
        if (agent.Velocity == Vector3.zero)
        {
            return agent.transform.rotation;
        }
        // have to specify wich direction is upward to prevent the object from fliping then reachin 90 and -90 on x axis
        return Quaternion.LookRotation(agent.Velocity, agent.transform.up);
    }

}
