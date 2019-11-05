using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathHandler : MonoBehaviour
{
    public static DeathHandler instance = null;
    public float health;
    public float maxHealth;
    public float damageBase;
    public float damageScale;

    public float slowdownSpeed;

    // Start is called before the first frame update
    void Awake()
    {
        if(instance == null || instance.Equals(null))
        {
            instance = this;
            health = maxHealth;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        ScoreManager.instance.OnComboChange += ComboChange;
        PauseManager.OnEnter[PauseManager.State.Dead] += OnDeath;

        health = maxHealth;
    }

    void Die()
    {
        PauseManager.currentState = PauseManager.State.Dead;
    }

    void OnDestroy()
    {
        ScoreManager.instance.OnComboChange -= ComboChange;
        PauseManager.OnEnter[PauseManager.State.Dead] -= OnDeath;
    }

    void OnDeath()
    {
        StartCoroutine(DeathRoutine());
    }

    IEnumerator DeathRoutine()
    {
        while(Time.timeScale > 0)
        {
            yield return 0;
            if(PauseManager.currentState != PauseManager.State.Dead)
                yield break;
            Time.timeScale = Mathf.Max(0, Time.timeScale - Time.deltaTime * slowdownSpeed);
        }
    }

    public void OnMiss()
    {
        //Lose health
        health -= damageBase * damageScale;
        //Check death
        if(health <= 0)
        {
            //Die
            Die();
        }
    }

    public void ComboChange()
    {
        if(ScoreManager.instance.combo != 0)
        {
            //Gain health based on combo stage
            float tempMaxHealth = 100;
            switch(ScoreManager.instance.GetComboStage())
            {
                case 0:
                    tempMaxHealth = Mathf.Max(health, 0.25f*maxHealth);
                    break;
                case 1:
                    tempMaxHealth = Mathf.Max(health, 0.5f * maxHealth);
                    break;
                default:
                    tempMaxHealth = maxHealth;
                    break;
            }

            health = Mathf.Min(tempMaxHealth, health + ScoreManager.instance.combo/ScoreManager.instance.comboBase);
        }
    }
}
