using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShockEffect : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Invincible inv;
        if (other.gameObject.CompareTag("Obstacle") && !other.TryGetComponent<Invincible>(out inv)) {
            Destroy(other.gameObject);
        }
    }
    public void EndEffect() {
        Destroy(gameObject);
    }
}
