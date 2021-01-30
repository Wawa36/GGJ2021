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

    [SerializeField]
    private float fallJumpMultiplier = 2.5f;
    private float lowJumpMultiplier = 2.0f;

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
        if (!IsOnTheGround()){
            if (rigidbody.velocity.y < 0)
            {
                rigidbody.AddForce(Vector3.down * Physics.gravity.magnitude * (fallJumpMultiplier - 1.0f), ForceMode.Acceleration);
            } else if (!Input.GetButton("Jump"))
            {
                rigidbody.AddForce(Vector3.down * Physics.gravity.magnitude * (lowJumpMultiplier - 1.0f), ForceMode.Acceleration);
            }
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
