using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionWIthWall : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 _oldPosition;
    public LayerMask layer;
    private void Start()
    {
       layer = 1 << 6;
    }
    private void Update()
    {
        //sphereCheckGround(Vector3.zero);
    }
    public void sphereCheckGround(Vector3 oldPosition)
    {
        _oldPosition = oldPosition;
        RaycastHit hit;
        LayerMask layer = 1 << 6;
        if (Physics.SphereCast(transform.position, 1.5f, -transform.up, out hit,  0.25f, layer))
        {
            Debug.Log(hit.transform.tag);
            
            GetComponent<healthManagment>().setHptoZero(GameObject.Find("wallScore"));
        }
       
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position - transform.up * 0.25f, 0.5f);
    }

    private void OnCollisionStay(Collision collision)
    {
        
        if (collision.gameObject.layer == layer)
        {
            if(GetComponent<strg_steerinAgent>().dead == false)
            {

                GetComponent<healthManagment>().setHptoZero(GameObject.Find("wallScore"));
            }
        }
        
        
    }
    /*
    private void OnCollisionEnter(Collision collision)
    {
        LayerMask layer = 1 << 6;
        if (collision.gameObject.layer == layer)
        {
            if (GetComponent<strg_steerinAgent>().dead == false)
            {

                GetComponent<healthManagment>().setHptoZero(GameObject.Find("wallScore"));
            }
        }
    }
    */
}
