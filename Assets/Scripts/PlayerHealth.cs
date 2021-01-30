using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public bool moreHealthAbility;
    [SerializeField] int powerMaxHealth;
    [SerializeField] int startMaxHealth;
    [SerializeField] int  currentMaxHealth;
    [SerializeField] public int currentHealth;

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

    public void HealthPowerUp() {
        int inc = powerMaxHealth - currentMaxHealth;
        currentHealth += inc;
        currentMaxHealth = powerMaxHealth;
        moreHealthAbility = true;
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
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (!playerIsInvincible) {
                Ability_Protection aProtection = GetComponent<Ability_Protection>();
                if (aProtection.protect)
                {
                    aProtection.EndProtection();
                }
                else {
                    currentHealth--;
                }
                time = 0;
                playerIsInvincible = true;
            }
            Destroy(collision.gameObject);
        }
    }
}
