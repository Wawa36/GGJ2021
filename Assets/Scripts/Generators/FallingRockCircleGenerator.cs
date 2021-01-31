using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct FRCGData
{
    public Vector3 pos;
}
public class FallingRockCircleGenerator : Generator<FRCGData>
{
    public enum FallingType {
        Random,
        FollowPlayer,
        Manual
    }

    [SerializeField]
    private GameObject prefab;

    private Transform baseEnvironment;

    [SerializeField]
    private FallingType fallingType;

    [SerializeField]
    private float outerDist = 10;

    [SerializeField]
    private float maxScale = 2f;

    [SerializeField]
    private float minScale = 1f;

    [SerializeField]
    private float[] rockAngles;

    [SerializeField]
    private float[] rockScales;

    [SerializeField]
    private float[] rockOffsets;

    [SerializeField]
    private float[] rockCounts;


    public void Start()
    {
        baseEnvironment = GameObject.Find("Base Environment").transform;
    }

    protected override void endGenerate(FRCGData data)
    {
    }

    protected override void generate(FRCGData data, int frame)
    {
        generateObj(data.pos, (frame-1) % rockCounts.Length);
    }

    protected override FRCGData initGenerate(int index)
    {
        FRCGData d;
        switch (fallingType) {
            case FallingType.Random:
                d.pos = new Vector3(Random.Range(-outerDist, outerDist), 0, Random.Range(-outerDist, outerDist));
                break;
            case FallingType.FollowPlayer:
                d.pos = GameObject.FindGameObjectWithTag("Player").transform.position;
                break;
            default:
                d.pos = transform.position;
                break;
        }
        return d;
    }

    private void generateObj(Vector3 pos, int index)
    {
        for (int j = 0; j < rockCounts[index]; j++)
        {
            GameObject obj = Instantiate(prefab);

            Vector3 offset = Vector3.forward * rockOffsets[index];
            offset = Quaternion.AngleAxis(rockAngles[index] + 360f * j / rockCounts[index], Vector3.up) * offset;
            obj.transform.position = pos + offset;
            obj.transform.localScale = obj.transform.localScale * rockScales[index];
            obj.transform.parent = baseEnvironment;
        }
    }
}
