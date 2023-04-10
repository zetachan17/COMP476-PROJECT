using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionWIthWall : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 _oldPosition;
    private void Update()
    {
       // sphereCheckGround();
    }
    public void sphereCheckGround(Vector3 oldPosition)
    {
        _oldPosition = oldPosition;
        RaycastHit hit;
        LayerMask layer = 1 << 6;
        if (Physics.SphereCast(transform.position, 0.5f, -transform.up, out hit,  0.25f, layer))
        {
            Debug.Log(hit.transform.tag);
            GetComponent<healthManagment>().setHptoZero(null);
        }
       
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position - transform.up * 0.25f, 0.5f);
    }
 

}
