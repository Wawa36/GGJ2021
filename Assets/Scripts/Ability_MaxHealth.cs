using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_MaxHealth : MonoBehaviour
{
    PlayerHealth ph;
    // Start is called before the first frame update
    void Start()
    {
        ph = GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!ph.moreHealthAbility)
            ph.HealthPowerUp();   
    }
}
