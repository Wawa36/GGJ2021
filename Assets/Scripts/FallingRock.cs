using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingRock : MonoBehaviour
{
    [SerializeField]
    private float minY = 0.1f;

    private bool destroyed = false;

    [SerializeField]
    private float timeUntilDown = 5;
    // Start is called before the first frame update
    void Start()
    {
        float newY = minY + 1.0f / 2.0f * timeUntilDown * timeUntilDown * Physics.gravity.magnitude;

        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        GetComponentInChildren<ScaledShadow>().maxY = newY;
    }

    void die() {
        if (!destroyed)
        {
            destroyed = true;
            Destroy(gameObject);
        }
    }
    void Update() {
        if (transform.position.y < minY) {
            die();
        }
    }
    // Update is called once per frame
    void OnCollisionEnter(Collision other)
    {
        if(!other.gameObject.CompareTag("Obstacle"))
            die();
    }
}
