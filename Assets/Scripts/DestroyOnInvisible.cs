using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnInvisible : MonoBehaviour
{
    [SerializeField]
    private double minX = -0.5, maxX = 1.5;
    [SerializeField]
    private double minY = -0.5, maxY = 1.5;

    void Update()
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        if (screenPosition.x < minX * Screen.width || screenPosition.x > maxX * Screen.width
            || screenPosition.y < minY * Screen.height || screenPosition.y > maxY * Screen.height)
            Destroy(gameObject);
    }
}
