using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Protection : MonoBehaviour
{
    [SerializeField]
    private float protectionTime;

    [SerializeField]
    private float recoverTime;

    private float time;

    private bool recovered = true;
    public bool protect = false;
    private bool hit = false;

    [SerializeField]
    private float shockHeight = 0.1f;


    [SerializeField]
    private GameObject protectionPrefab;

    [SerializeField]
    private GameObject protection;
    // Update is called once per frame

    private void Update()
    {
        if (Input.GetButtonDown("Protect") && recovered
            && !GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().playerIsInvincible)
        {
            BeginProtection();
        }
        if (protect || !recovered)
        {
            time += Time.deltaTime;
        }
        if (protect && time > protectionTime)
        {
            EndProtection();
        }

        if (!recovered && time > recoverTime)
        {
            recovered = true;
        }
    }

    public void BeginProtection()
    {
        protect = true;
        recovered = false;
        protection = Instantiate(protectionPrefab, transform);
        protection.GetComponent<Animator>().speed = 1f / protectionTime;
        time = 0;
    }

    public void EndProtection()
    {
        time = 0;
        protect = false;
        protection.GetComponent<Animator>().speed = 1f / recoverTime;
        hit = false;
    }

    public void Hit() {
        protection.GetComponent<Animator>().SetTrigger("hit");
        float invTime = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>().invincibilityTime;
        protection.GetComponent<Animator>().speed = 1f / invTime;

        Invoke("EndProtection", invTime);
        hit = true;
    }

    public void endInvincibility() {
    }

    public bool canProtect() {
        return protect && !hit;
    }

}
