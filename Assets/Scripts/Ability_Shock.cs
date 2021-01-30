using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Shock : MonoBehaviour
{
    [SerializeField]
    private float preparationTime;

    [SerializeField]
    private float recoverTime;

    [SerializeField]
    private float chargeTime;

    private float time;

    private bool recovered = true;
    public bool shockCharge = false;

    [SerializeField]
    private GameObject shockPrefab;

    [SerializeField]
    private float shockHeight = 0.1f;
    // Update is called once per frame
    
    private void Update()
    {
        if (Input.GetButtonDown("Shock") && recovered)
        {
            ShockCharge();
        }
        else if (!Input.GetButton("Shock") && shockCharge)
        {
            ShockChargeCancel();
        }
        if (shockCharge || !recovered) {
            time += Time.deltaTime;
        }
        if (shockCharge && time > chargeTime) {
            Shock();
        }

        if (!recovered && time > recoverTime) {
            recovered = true; 
        }
    }

    void ShockCharge() {
        shockCharge = true;
        time = 0;
    }

    void ShockChargeCancel() {
        shockCharge = false;
    }

    void Shock() {
        shockCharge = false;
        time = 0;

        GameObject shock = Instantiate(shockPrefab);
        shock.transform.position = new Vector3(transform.position.x, shockHeight, transform.position.z);
    }
}
