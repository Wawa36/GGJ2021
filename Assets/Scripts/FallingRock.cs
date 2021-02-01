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

    private float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        float newY = minY + 1.0f / 2.0f * timeUntilDown * timeUntilDown * Physics.gravity.magnitude;


        GetComponentInChildren<ScaledShadow>().maxY = newY;
        time = 0;
        GetComponentInChildren<ScaledShadow>().y = newY;
    }

    void die() {
        if (!destroyed)
        {
            destroyed = true;
            GetComponent<Animator>().SetTrigger("fall");
        }
    }
    void Update() {

        time += Time.deltaTime;
        float newY = minY + 1.0f / 2.0f * (timeUntilDown - time) * (timeUntilDown - time) * Physics.gravity.magnitude;
        GetComponentInChildren<ScaledShadow>().y = newY;
        if (time > timeUntilDown) {
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
