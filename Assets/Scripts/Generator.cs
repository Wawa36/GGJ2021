using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Generator : MonoBehaviour
{
    public float enableDelay;
    public float repeatingDelay;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnEnable()
    {
        Invoke("generateInvoke", enableDelay);
    }

    private void generateInvoke() {
        generate();
        Invoke("generateInvoke", repeatingDelay);
    }

    protected abstract void generate();

    private void OnDisable()
    {
        CancelInvoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
