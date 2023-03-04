using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionRayCast : MonoBehaviour
{
    public float raydistance;
    public int weightOfDodge;
    private int stuckdetection = 0;
    public GameObject newPositionVisualiser;

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

        var right45 = (Vector3.forward + Vector3.right).normalized;
        var left45 = (Vector3.forward - Vector3.right).normalized;
        var left = (Vector3.left).normalized;
        var right = (Vector3.right).normalized;
        var back = (Vector3.forward * -1).normalized;

        RaycastHit hit;
        int count = 0;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, raydistance, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * raydistance, Color.yellow);
            //Debug.Log("Did Hit");
            ellementToDodge[count] = hit.point + (hit.normal) * weightOfDodge;
            newPositionVisualiser.transform.position = hit.point + (hit.normal) * weightOfDodge;
            count++;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * raydistance, Color.white);
            //Debug.Log("Did not Hit");
        }
        return ellementToDodge;
    }
}
