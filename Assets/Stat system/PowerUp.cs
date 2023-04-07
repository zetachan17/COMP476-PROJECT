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
    protected float duration;   // Duration of the powerup in seconds.\

    [SerializeField]
    protected UnityEvent<StatTracked> OnPowerUpApplied;

    [SerializeField]
    protected UnityEvent<StatTracked> OnPowerUpExpired;

    public void Apply(StatTracked obj) {
        var oldStat = obj.GetStat(modifies);
        obj.SetStat(modifies, newValue);
        OnPowerUpApplied?.Invoke(obj);
        StartCoroutine(Wait());
        obj.SetStat(modifies, oldStat);
        OnPowerUpExpired?.Invoke(obj);
    }

    private void OnCollisionEnter(Collision collision)
    {
        StatTracked tracker = collision.gameObject.GetComponent<StatTracked>();
        if (tracker != null)
            Apply(tracker);
    }

    private IEnumerator Wait() { 
        yield return new WaitForSeconds(duration);
    }
}
