using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Dash : MonoBehaviour
{

    [SerializeField] float dashForce;
    [SerializeField] float recoverTime;

    [SerializeField] float deadZone = 0.05f;
    [SerializeField] float dashDrag = 10f;
    bool playerCanDash;
    bool pressDashButton = false;
    float time;
    Rigidbody rigidbody;

    Vector3 dashDirection;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        playerCanDash = true;
        time = 0;
        dashDirection = Vector3.forward;
    }

    void Update()
    {
        if (Input.GetButtonDown("Dash"))
        {
            if(playerCanDash)
                pressDashButton = true;
        }

        Vector3 newDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (newDir.magnitude > deadZone) {
            dashDirection = newDir.normalized;
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
            rigidbody.drag = 0.0f;
        }
        else {
            rigidbody.drag = dashDrag;
        }
    }
}
