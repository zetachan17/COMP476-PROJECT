using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyTest : MonoBehaviour
{
    private void Start()
    {
        GetComponent<StatTracked>().SetStat(StatTracked.Stat.Hp, 4);
    }

    public void ChangeToRedIfBelow5(StatTracked.Stat stat, float oldValue, float newValue) {
        if (stat == StatTracked.Stat.Hp && newValue <= 5)
            gameObject.GetComponent<Renderer>().material.color = Color.red;
    }
}
