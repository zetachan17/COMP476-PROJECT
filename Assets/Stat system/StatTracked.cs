using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StatTracked : MonoBehaviour
{
    [SerializeField]
    private List<StatInfo> m_Stats;

    [SerializeField]
    protected UnityEvent<Stat, float, float> OnStatChanged;

    public void SetStat(Stat type, float value) {
        try
        {
            for (int i = 0; i < m_Stats.Count; i++) {
                if (m_Stats[i].type == type)
                {
                    var oldVal = m_Stats[i].value;
                    m_Stats[i].value = value;

                    OnStatChanged?.Invoke(type, oldVal, value);
                }
            }
        }
        catch (Exception e) {
            Debug.LogError($"An error occured while setting stat. Original stat type is different from the one you are trying to set.");
            throw e;
        }
    }

    public float GetStat(Stat type) {
        foreach (var stat in m_Stats)
        {
            if (stat.type == type)
                return stat.value;
        }

        throw new ArgumentException($"Invalid {nameof(type)}. Object does not have it.");
    }

    public enum Stat {
        MaxSpeed,
        MaxTurnAngle,
        MaxHp,
        Hp,
        Visibility, // Stealth
        Sheild
    }

    [Serializable]
    protected class StatInfo  {
        public Stat type;
        public float value;
    }
}
