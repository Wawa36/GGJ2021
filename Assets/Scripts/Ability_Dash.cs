using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Dash : MonoBehaviour
{

    [SerializeField] float dashForce;
    [SerializeField] float recoverTime;
    bool playerCanDash;
    bool pressDashButton = false;
    float time;
    Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        playerCanDash = true;
        time = 0;
    }

    void Update()
    {
        if (Input.GetButtonDown("Dash"))
        {
            pressDashButton = true;
        }

        Recover();
    }

    private void FixedUpdate()
    {
        Dash();
    }

    void Dash()
    {
        if (pressDashButton && playerCanDash)
        {
            Vector3 dashDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            rigidbody.AddForce(dashDirection * dashForce, ForceMode.Impulse);
            playerCanDash = false;
            time = 0;
            pressDashButton = false;
        }
    }

    void Recover()
    {
        time += Time.deltaTime;
        if (time >= recoverTime)
        {
            playerCanDash = true;
        }
    }
}
