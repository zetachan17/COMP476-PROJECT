using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interShipCollision : MonoBehaviour
{
    // Start is called before the first frame update

   

    public void hitFromTheSide(GameObject killer)
    {
        //implement Function Get hit from the side

        Debug.Log(this.name + " was hit from the side");
        
        this.gameObject.GetComponent<healthManagment>().setHptoZero(killer);
    }

    public void hitFromTheFront()
    {
        //implement function get hit from the front
        Debug.Log(this.name +" was hit in the front");
    }
}
