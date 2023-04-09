using System.Collections;
using UnityEngine.Events;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    public StatTracked.Stat modifies;

    [SerializeField]
    protected float newValue;   // We make this a function if we want the new value to depend on the old value

    [SerializeField]
    protected float duration = 2;   // Duration of the powerup in seconds.\

    [SerializeField]
    protected UnityEvent<StatTracked> OnPowerUpApplied;

    [SerializeField]
    protected UnityEvent<StatTracked> OnPowerUpExpired;

    public void Apply(StatTracked obj) {
        var oldStat = obj.GetStat(modifies);
        obj.SetStat(modifies, oldStat+newValue);    //Small boost at the time 
        OnPowerUpApplied?.Invoke(obj);
        //StartCoroutine(Wait()); // The stat power up are not temporary , they stay intill the actor get killed.
        //obj.SetStat(modifies, oldStat);
        //OnPowerUpExpired?.Invoke(obj);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision");
        StatTracked tracker = collision.gameObject.GetComponent<StatTracked>();
        if (tracker != null)
        {
            Debug.Log("Tracker");
            Apply(tracker);
        }
            
    }

    private IEnumerator Wait() { 
        yield return new WaitForSeconds(duration);
    }
}
