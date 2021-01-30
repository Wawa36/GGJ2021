using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Generator<T> : MonoBehaviour
{
    public float enableDelay;
    public float repeatingDelay;
    public int generatorCount = 1;


    public List<T> spawnData;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        if (spawnData is null) {
            spawnData = new List<T>();
        }
        spawnData.Clear();
        for(int i = 0; i < generatorCount; i++) {
            spawnData.Add(initGenerate());
        }
        Invoke("generateInvoke", enableDelay);
    }

    private void generateInvoke() {
        foreach (T sdata in spawnData) {
            generate(sdata);
        }
        Invoke("generateInvoke", repeatingDelay);
    }

    protected abstract void generate(T spawnData);
    protected abstract T initGenerate();
    protected abstract void endGenerate(T spawnData);

    private void OnDisable()
    {
        CancelInvoke();

        foreach (T sdata in spawnData) {
            endGenerate(sdata);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
