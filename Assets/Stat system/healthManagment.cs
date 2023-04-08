using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class healthManagment : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    private StatTracked stat;
    void Start()
    {
        maxHealth = GetComponent<StatTracked>().GetStat(StatTracked.Stat.MaxHp);
        currentHealth = GetComponent<StatTracked>().GetStat(StatTracked.Stat.Hp);
        stat = GetComponent<StatTracked>();
    }


    public void changeHP(float amount)
    {
        if(currentHealth + amount <= maxHealth)
        {
            stat.SetStat(StatTracked.Stat.Hp, amount);
        }
        else
        {
            float modification = maxHealth - currentHealth;
            stat.SetStat(StatTracked.Stat.Hp, modification);
        }

        checkIfDead();
        updateUI();
    }


    public void checkIfDead()
    {
        if(currentHealth <= 0)
        {
            Debug.Log("This player is dead");
        }
    }

    public void updateUI()
    {
        //TO-Do update the UI
    }

    public void OnStatChange(StatTracked.Stat stat, float oldValue, float newValue)
    {
        if (stat == StatTracked.Stat.MaxHp)
        {
            maxHealth = newValue;
            updateUI();
        }
        else if(stat == StatTracked.Stat.Hp)
        {
            currentHealth = newValue;
        }
    }
}
