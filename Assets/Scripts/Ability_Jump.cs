using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Jump : MonoBehaviour
{
    [SerializeField] Transform bottomOfPlayer;
    [SerializeField] float jumpGroundSensibility;
    [SerializeField] float jumpForce;
    Rigidbody rigidbody;

    private bool isJump = false;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        bottomOfPlayer = this.gameObject.transform.Find("BottomOfPlayer");
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            isJump = true;
        }
    }

    private void FixedUpdate()
    {
        Jump();
    }

    private void Jump()
    {
        if (isJump && IsOnTheGround())
        {
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJump = false;
        }
    }

    bool IsOnTheGround()
    {
        if(bottomOfPlayer.position.y < jumpGroundSensibility)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
