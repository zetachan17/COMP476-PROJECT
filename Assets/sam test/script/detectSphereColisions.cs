using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectSphereColisions : MonoBehaviour
{
    // Start is called before the first frame update
    public float radius;
    [SerializeField] private Vector3 velocity;
    [SerializeField] private List<GameObject> _shipList;
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        velocity = GetComponent<strg_steerinAgent>().Velocity;
        detectColision();
    }



    public void detectColision()
    {
        GameObject _shipListParent = GameObject.Find("ShipList").gameObject;
        _shipList = new List<GameObject>();
        
        for (int i  =0; i < _shipListParent.transform.childCount; i++)
        {
            if(_shipListParent.transform.GetChild(i).name != this.name)
            {

                _shipList.Add(_shipListParent.transform.GetChild(i).gameObject);
            }
        }

        foreach(GameObject ship in _shipList)
        {
            float otherRadius = ship.GetComponent<detectSphereColisions>().radius;
            Vector3 otherVelocity = ship.GetComponent<detectSphereColisions>().velocity;
            float distance = Vector3.Distance(ship.transform.position, this.transform.position);
            if (distance <=  otherRadius + radius)
            {
                Vector3 colisonNomral = Vector3.Normalize(ship.transform.position - this.transform.position);

                float angle = Vector3.Angle(velocity, colisonNomral);
                Debug.Log(this.name + " at angle " + angle);
                
                if(angle <= 45)
                {
                    Debug.Log(this.name + "was Hit in the front" + angle);
                    GetComponent<interShipCollision>().hitFromTheFront();
                }
                else
                {
                   Debug.Log(this.name + "was Hit from the side or back" + angle);
                    GetComponent<interShipCollision>().hitFromTheSide(ship);
                }
            }
        }
        /*

        if(this.name == "sphereB")
        {
            return;
        }
        GameObject other = GameObject.Find("sphereB").gameObject;
        float otherR = other.GetComponent<detectSphereColisions>().radius;
        Vector3 otheerV = other.GetComponent<detectSphereColisions>().velocity;
        float distance = Vector3.Distance(other.transform.position, this.transform.position);
        if (distance <= otherR + radius)
        {
            Vector3 colisonNomral = Vector3.Normalize(other.transform.position - this.transform.position);
            Vector3 relVelocity = velocity - otheerV;

            float dotProduct = Vector3.Dot(colisonNomral, relVelocity);
            Debug.Log(dotProduct);
        };
        */
       
    }
}
