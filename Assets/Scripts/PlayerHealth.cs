﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public bool moreHealthAbility;
    [SerializeField] int powerMaxHealth;
    [SerializeField] int startMaxHealth;
    [SerializeField] int  currentMaxHealth;
    [SerializeField] private int _currentHealth;

    public int currentHealth {
        get => _currentHealth;
        set {
            _currentHealth = value;
            if (_currentHealth > currentMaxHealth)
                _currentHealth = currentMaxHealth;
        }
    }
    [SerializeField] public float invincibilityTime;
    float time;
    public bool playerIsInvincible;

    bool died = false;

    private void Awake()
    {
        foreach (StageLink.StageObject st in StageLink.instance.gameData.skills)
        {
            if (st.name == "moreHealth")
                moreHealthAbility = st.taken;
        }
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
        if (!moreHealthAbility)
        {
           currentMaxHealth = powerMaxHealth;
            currentHealth = currentMaxHealth;
            moreHealthAbility = true;
        }
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
            gameObject.layer = LayerMask.NameToLayer("Player");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            if (!playerIsInvincible) {
                Ability_Protection aProtection = GetComponent<Ability_Protection>();
                if (aProtection.canProtect())
                {
                    aProtection.Hit();
                }
                else {
                    currentHealth--;
                    GetComponent<Animator>().SetTrigger("hit");
                }
                time = 0;
                playerIsInvincible = true;

                gameObject.layer = LayerMask.NameToLayer("PlayerInvincible");
            }

            Invincible inv;
            if (!collision.gameObject.TryGetComponent<Invincible>(out inv)) {
                Destroy(collision.gameObject);
            }
        }
    }
}
