using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionRayCast : MonoBehaviour
{
    public float raydistance;
    public int weightOfDodge;
    private int stuckdetection = 0;
    public GameObject newPositionVisualiser;
    public Vector3 lastHitdDirection;
    public List<GameObject> extraLine;
    

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public Vector3[] vissionDetection()
    {
        Vector3[] ellementToDodge = new Vector3[10];

        int layerMask = 1 << 6;

        // This would cast rays only against colliders in layer 6.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        //layerMask = ~layerMask;
        List<Vector3> basicRayAngle = new List<Vector3>();
        basicRayAngle.Add(transform.TransformDirection(Vector3.forward));
        basicRayAngle.Add(transform.TransformDirection(Vector3.back));
        basicRayAngle.Add(transform.TransformDirection(Vector3.up));
        basicRayAngle.Add(transform.TransformDirection(Vector3.down));
        /*
        var right45 = (Vector3.forward + Vector3.right).normalized;
        var left45 = (Vector3.forward - Vector3.right).normalized;
        var left = (Vector3.left).normalized;
        var right = (Vector3.right).normalized;
        var back = (Vector3.forward * -1).normalized;
        */
        RaycastHit hit;
        int count = 0;
        bool firstRay = true;
        float variableRaydistance = raydistance;

        //shoot a ray forward , backward , up and down to prevend hitting a wall
        foreach(Vector3 direction in basicRayAngle)
        {

            if (Physics.Raycast(transform.position, direction, out hit, variableRaydistance, layerMask))
            {
                Debug.DrawRay(transform.position, direction * variableRaydistance, Color.yellow);
                //Debug.Log("Did Hit");
                ellementToDodge[count] = hit.point + (hit.normal) * weightOfDodge;
               
                
                //If a ray hit a wall, the direction of the ray is saved to check the same dirrection again  during the next itterationn
                newPositionVisualiser.transform.position = hit.point + (hit.normal) * weightOfDodge;
                lastHitdDirection = direction;
                count++;
            }
            else
            {
                Debug.DrawRay(transform.position, direction * variableRaydistance, Color.white);
                //Debug.Log("Did not Hit");
            }
            if(firstRay == true)
            {
                variableRaydistance = variableRaydistance / 6;
                firstRay = false;
            }

        }

        //Raycast for specific part of the ship that are wider
        foreach (GameObject extraPoint in extraLine)
        {

            if (Physics.Raycast(extraPoint.transform.position, extraPoint.transform.TransformDirection(Vector3.forward), out hit, raydistance / 2, layerMask))
            {
                Debug.DrawRay(extraPoint.transform.position, extraPoint.transform.TransformDirection(Vector3.forward) * raydistance / 2, Color.yellow);
                //Debug.Log("Did Hit");
                ellementToDodge[count] = hit.point + (hit.normal) * weightOfDodge;
                newPositionVisualiser.transform.position = hit.point + (hit.normal) * weightOfDodge;
                lastHitdDirection = transform.TransformDirection(Vector3.forward);
                count++;
            }
            else
            {
                Debug.DrawRay(extraPoint.transform.position, transform.TransformDirection(Vector3.forward) * raydistance / 2, Color.white);
                //Debug.Log("Did not Hit");
            }
        }

        // Additional raycast that is use to check the position of the last wall detected. This raycast prevent the agent to rotate back and forth when they are close to a wall.
        if (lastHitdDirection != null)
        {

            if(Physics.Raycast(transform.position, lastHitdDirection, out hit, raydistance, layerMask))
            {
                Debug.DrawRay(transform.position, lastHitdDirection * raydistance, Color.red);
                ellementToDodge[count] = hit.point + (hit.normal) * weightOfDodge;
                newPositionVisualiser.transform.position = hit.point + (hit.normal) * weightOfDodge;
                count++;
            }
            else
            {
                Debug.DrawRay(transform.position, lastHitdDirection * raydistance, Color.blue);
                lastHitdDirection = transform.TransformDirection(Vector3.forward);
            }
        }

       

        return ellementToDodge;
    }
}
