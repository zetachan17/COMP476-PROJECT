using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    protected StatTracked.Stat modifies;

    [SerializeField]
    protected float newValue;   // This can also be a function if we want the new value to depend on the old value


    public void Apply(StatTracked obj) {
        obj.SetStat(modifies, newValue);
    }

    private void OnCollisionEnter(Collision collision)
    {
        StatTracked tracker = collision.gameObject.GetComponent<StatTracked>();
        if (tracker != null)
            Apply(tracker);
    }
}
