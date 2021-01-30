using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct FRRGData
{
    public Vector3 pos;
}
public class FallingRockRandomGenerator : Generator<FRRGData>
{


    [SerializeField]
    private GameObject prefab;

    private Transform baseEnvironment;

    [SerializeField]
    private bool autoPlacement = true;

    [SerializeField]
    private float outerDist = 10;

    [SerializeField]
    private float maxScale = 2f;

    [SerializeField]
    private float minScale = 1f;


    public void Start()
    {
        baseEnvironment = GameObject.Find("Base Environment").transform;
    }

    protected override void endGenerate(FRRGData data)
    {
    }

    protected override void generate(FRRGData data)
    {
        generateObj(data.pos);
    }

    protected override FRRGData initGenerate()
    {
        FRRGData d;
        d.pos = new Vector3(Random.Range(-outerDist, outerDist), 0, Random.Range(-outerDist,outerDist));
        return d;
    }

    private void generateObj(Vector3 pos)
    {
        GameObject obj = Instantiate(prefab);
        obj.transform.position = pos;
        obj.transform.localScale = obj.transform.localScale * Random.Range(minScale, maxScale);
        obj.transform.parent = baseEnvironment;
    }
}
