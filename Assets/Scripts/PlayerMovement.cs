using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float baseSpeed;

    [SerializeField] float chargeSpeed;

    [SerializeField] float protectionSpeed;

    float speed;
    Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        speed = baseSpeed;
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        Ability_Shock aShock = GetComponent<Ability_Shock>();
        if (aShock && aShock.enabled && aShock.shockCharge)
        {
            speed = chargeSpeed;
        }
        else {
            speed = baseSpeed;
        }

        Ability_Protection aProtection = GetComponent<Ability_Protection>();
        if (aProtection && aProtection.enabled && aProtection.protect) {
            speed = Mathf.Min(speed, protectionSpeed);
        }

        rigidbody.MovePosition(rigidbody.position 
            + (transform.forward * Input.GetAxis("Vertical") * speed * Time.fixedDeltaTime) 
            + (transform.right * Input.GetAxis("Horizontal") * speed * Time.fixedDeltaTime));
    }
}
