using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class statVariableDictionary : MonoBehaviour
{
    // Start is called before the first frame update

    public float maxTurnAngle;
    public float baseTurnAngle;

    public float maxHP;
    public float baseMaxSpeed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       // GetComponent<StatTracked>().SetStat(StatTracked.Stat.MaxTurnAngle, 5);
    }

    public void testing(StatTracked.Stat stat,float oldValue, float newValue)
    {
        Debug.Log("stat change" + oldValue);
    }
}
