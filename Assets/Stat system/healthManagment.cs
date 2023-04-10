using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class healthManagment : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private float maxHealth;
    [SerializeField] private float currentHealth;
    private StatTracked stat;
    
    [SerializeField] private GameObject healthBar;
    
    private Slider _healthBarSlider;
    private Image _healthBarFill;
    private Gradient _healthBarGradient;
    void Start()
    {
        maxHealth = GetComponent<StatTracked>().GetStat(StatTracked.Stat.MaxHp);
        currentHealth = GetComponent<StatTracked>().GetStat(StatTracked.Stat.Hp);
        stat = GetComponent<StatTracked>();
        
        if(this.GetComponent<strg_steerinAgent>().player == true)
        {
            _healthBarSlider = healthBar.GetComponent<Slider>();
            _healthBarFill = healthBar.GetComponentInChildren<HealthBar>().fill;
            _healthBarGradient = healthBar.GetComponent<HealthBar>().gradient;
        }
    }


    public void changeHP(float amount, GameObject enemy)
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

        checkIfDead(enemy);
        updateUI();
    }
    public void setHptoZero(GameObject killer)
    {
        changeHP(currentHealth * -1, killer);
    }
    public void ChangeMaxHP(float amount)
    {
        //stat.SetStat(StatTracked.Stat.MaxHp, amount);
        maxHealth = stat.GetStat(StatTracked.Stat.MaxHp);
        updateUI();
    }

    private void Update()
    {
        //Debug.Log("Current Health: " + currentHealth);
       // Debug.Log( gameObject.name + "maxHealth: " + maxHealth + " currentHealth: " + currentHealth);
    }

    public void checkIfDead(GameObject enemy)
    {
        if(currentHealth <= 0)
        {
            Debug.Log("This player is dead");
            if(enemy != null)
            {

                enemy.GetComponent<individualScore>().getKill();
            }
            GetComponent<deathManagment>().getkilled();
        }
    }

    public void resetHealth()
    {
        changeHP(maxHealth, null);
    }

    public void updateUI()
    {
        //TO-Do update the UI

        if (GetComponent<strg_steerinAgent>().player == true)
        {
            _healthBarSlider.maxValue = maxHealth;
            _healthBarSlider.value = currentHealth;
            _healthBarFill.color = _healthBarGradient.Evaluate(_healthBarSlider.normalizedValue);
        }
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
