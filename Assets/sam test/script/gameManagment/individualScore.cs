using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class individualScore : MonoBehaviour
{
    // Start is called before the first frame update

    private int kill = 0;
    private int death = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addKill()
    {
        kill++;
    }
    public void addDeath()
    {
        death++;
    }

    public int getKill()
    {
        return kill;
    }
    public int getDeath()
    {
        return death;
    }
}
