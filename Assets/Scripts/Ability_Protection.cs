using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Protection: MonoBehaviour
{
    [SerializeField]
    private float protectionTime;

    [SerializeField]
    private float recoverTime;

    private float time;

    private bool recovered = true;
    public bool protect = false;

    [SerializeField]
    private float shockHeight = 0.1f;


    [SerializeField]
    private GameObject protection;
    // Update is called once per frame

    private void Update()
    {
        if (Input.GetButtonDown("Protect") && recovered)
        {
            BeginProtection();
        }
        if (protect || !recovered)
        {
            time += Time.deltaTime;
        }
        if (time > protectionTime)
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
        protection.SetActive(true);
        time = 0;
    }

    public void EndProtection()
    {
        protect = false;
        protection.SetActive(false);
    }

}
