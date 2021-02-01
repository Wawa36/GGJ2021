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

    [SerializeField]
    private float minChargeTime;

    private float time;

    private bool recovered = true;
    public bool shockCharge = false;

    [SerializeField]
    private GameObject shockPrefab;

    [SerializeField]
    private GameObject shockChargePrefab;

    private GameObject shockChargeObj;

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
            if (time < minChargeTime)
                ShockChargeCancel();
            else
                Shock();
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
        shockChargeObj = Instantiate(shockChargePrefab, GameObject.FindGameObjectWithTag("Player").transform);
        shockChargeObj.transform.position = new Vector3(shockChargeObj.transform.position.x, 
            shockChargePrefab.transform.position.y, shockChargeObj.transform.position.z);
    }

    void ShockChargeCancel() {
        shockCharge = false;
        Destroy(shockChargeObj);
    }

    void Shock() {
        shockCharge = false;
        Destroy(shockChargeObj);
        

        GameObject shock = Instantiate(shockPrefab);
        float t = Mathf.Min(chargeTime, time) / chargeTime;
        shock.transform.localScale = new Vector3(shock.transform.localScale.x * t, 1, shock.transform.localScale.z * t);
        shock.transform.position = new Vector3(transform.position.x, shockHeight, transform.position.z);
        time = 0;
    }
}
