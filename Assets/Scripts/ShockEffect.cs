using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockEffect : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle")) {
            Destroy(other.gameObject);
        }
    }
    public void EndEffect() {
        Destroy(gameObject);
    }
}
