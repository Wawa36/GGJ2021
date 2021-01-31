using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct LiGData
{
    public Vector3 pos;
    public Vector3 dir;
}
public class LineGenerator : Generator<LiGData>
{

    [SerializeField]
    private float speed;

    [SerializeField]
    private GameObject prefab;

    private Transform baseEnvironment;




    [SerializeField]
    private FallingRockCircleGenerator.FallingType mode = FallingRockCircleGenerator.FallingType.Manual;

    [SerializeField]
    private float outerDist = 12;
    [SerializeField]
    private float initHeight = 0.1f;

    [SerializeField]
    private float endZ = -6f;

    [SerializeField]
    private float minEndX = -4f;
    private float maxEndX = 4f;

    [SerializeField]
    private float minScale = 1f;

    [SerializeField]
    private float maxScale = 1f;

    [SerializeField]
    private Transform point1;

    [SerializeField]
    private Transform point2;

    public void Start()
    {
        baseEnvironment = GameObject.Find("Base Environment").transform;
    }

    protected override void endGenerate(LiGData data)
    {
    }

    protected override void generate(LiGData data, int frame)
    {

        switch (mode)
        {
            case FallingRockCircleGenerator.FallingType.Random:
                generateLine(data.pos, transform.forward);
                break;
            case FallingRockCircleGenerator.FallingType.FollowPlayer:
                generateLine(data.pos,
                    (GameObject.FindGameObjectWithTag("Player").transform.position - data.pos).normalized);
                break;
            case FallingRockCircleGenerator.FallingType.Manual:
                generateLine(data.pos, this.transform.forward);
                break;

        }
    }

    protected override LiGData initGenerate(int index)
    {
        LiGData d;
        float t = Random.Range(0, 1f);
        d.pos = (1 - t) * point1.position + t * point2.position;
        d.pos = Quaternion.AngleAxis(Random.Range(-90.0f, 90.0f), Vector3.up) * d.pos;
        Vector3 dest = new Vector3(Random.Range(minEndX, maxEndX), initHeight, endZ);
        d.dir = dest - d.pos;
        return d;
    }

    private void generateLine(Vector3 pos, Vector3 dir)
    {
        dir.y = 0;
        dir = dir.normalized;
        GameObject obj = Instantiate(prefab);
        obj.transform.position = pos;
        obj.transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
        obj.transform.parent = baseEnvironment;
        obj.transform.localScale = Random.Range(minScale, maxScale) * obj.transform.localScale;
        obj.GetComponent<MoveForward>().speed = speed;
    }
}
