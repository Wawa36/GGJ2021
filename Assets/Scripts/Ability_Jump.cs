using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Jump : MonoBehaviour
{
    [SerializeField] Transform bottomOfPlayer;
    [SerializeField] float jumpGroundSensibility;
    [SerializeField] float jumpForce;
    Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        bottomOfPlayer = this.gameObject.transform.Find("BottomOfPlayer");
    }

    private void FixedUpdate()
    {
        Jump();
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && IsOnTheGround())
        {
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
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
