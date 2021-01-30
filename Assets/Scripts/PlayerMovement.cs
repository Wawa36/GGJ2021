using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed;
    Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rigidbody.MovePosition(rigidbody.position 
            + (transform.forward * Input.GetAxis("Vertical") * speed * Time.fixedDeltaTime) 
            + (transform.right * Input.GetAxis("Horizontal") * speed * Time.fixedDeltaTime));
    }
}
