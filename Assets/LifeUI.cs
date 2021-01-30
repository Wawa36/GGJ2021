using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeUI : MonoBehaviour
{
    [SerializeField]
    PlayerHealth health;

    [SerializeField]
    Image[] hearts;
    // Start is called before the first frame update
    void Start()
    {
        health = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < hearts.Length; i++) {
            hearts[i].enabled = i + 1 <= health.currentHealth;
        }   
    }
}
