using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] bool moreHealthAbility;
    [SerializeField] float powerMaxHealth;
    [SerializeField] float startMaxHealth;
    [SerializeField] float currentMaxHealth;
    [SerializeField] float currentHealth;

    [SerializeField] float invincibilityTime;
    float time;
    bool playerIsInvincible;

    private void Awake()
    {
        if(moreHealthAbility)
        {
            currentMaxHealth = powerMaxHealth;
        }
        else
        {
            currentMaxHealth = startMaxHealth;
        }
        currentHealth = currentMaxHealth;
        time = 0;
        playerIsInvincible = false;
    }

    private void Update()
    {
        ManageDeath();
        ManageInvincibility();
    }

    void ManageDeath()
    {
        if(currentHealth <= 0)
        {
            //Do the death stuff
            Debug.Log("I am dead guys!");
        }
    }

    void ManageInvincibility()
    {
        time += Time.deltaTime;
        if (time >= invincibilityTime)
        {
            playerIsInvincible = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle")
            && !playerIsInvincible)
        {
            currentHealth--;
            time = 0;
            playerIsInvincible = true;
        }
    }
}
