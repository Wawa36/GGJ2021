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

    bool died = false;

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

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0)) {
            currentHealth--;
        }
#endif
    }

    void ManageDeath()
    {
        if (currentHealth <= 0)
        {
            if (!died) {
                died = true;
                StageLink.instance.die();
            }
        }
        else {
            died = false;
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

            Invincible inv;
            if (!collision.gameObject.TryGetComponent<Invincible>(out inv)) {
                Destroy(collision.gameObject);
            }
        }
    }
}
