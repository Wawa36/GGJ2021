using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Generator<T> : MonoBehaviour
{
    public float enableDelay;
    public float repeatingDelay;
    public float resetGenDelay;
    public int generatorCount = 1;
    public int resetGeneratorPeriod = 0;

    private int genFrame = 0;


    public List<T> spawnData;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void initGenerators() {
        genFrame = 0;
        if (spawnData is null)
        {
            spawnData = new List<T>();
        }
        spawnData.Clear();
        for (int i = 0; i < generatorCount; i++)
        {
            spawnData.Add(initGenerate());
        }
    }

    private void OnEnable()
    {
        initGenerators();   
        Invoke("generateInvoke", enableDelay);
    }

    private void generateInvoke() {
        genFrame ++;
        
        foreach (T sdata in spawnData) {
            generate(sdata);
        }
        if (genFrame >= resetGeneratorPeriod && resetGeneratorPeriod > 0)
        {
            initGenerators();
            Invoke("generateInvoke", resetGenDelay);
        }
        else {
            Invoke("generateInvoke", repeatingDelay);
        }
       
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
