﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RRGData
{
    public Vector3 pos;
    public Vector3 dir;
}
public class RollingRockGenerator : Generator<RRGData>
{

    [SerializeField]
    private float speed;

    [SerializeField]
    private GameObject prefab;

    private Transform baseEnvironment;

    [SerializeField]
    private bool autoPlacement = true;

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
    private FallingRockCircleGenerator.FallingType mode;

    public void Start()
    {
        baseEnvironment = GameObject.Find("Base Environment").transform;
    }

    protected override void endGenerate(RRGData data)
    {
    }

    protected override void generate(RRGData data, int frame)
    {
        if (autoPlacement)
        {
            generateLine(data.pos, data.dir);
        }
        else
        {
            generateLine(this.transform.position, this.transform.forward);
        }
    }

    protected override RRGData initGenerate(int index)
    {
        RRGData d;
        d.pos = new Vector3(0, initHeight, outerDist);
        d.pos = Quaternion.AngleAxis(Random.Range(-90.0f, 90.0f), Vector3.up) * d.pos;
        Vector3 dest = new Vector3(Random.Range(minEndX, maxEndX), initHeight, endZ);
        d.dir = dest - d.pos;
        return d;
    }

    private void generateLine(Vector3 pos, Vector3 dir)
    {
        GameObject obj = Instantiate(prefab);
        obj.transform.position = pos;
        obj.transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
        obj.transform.parent = baseEnvironment;
        obj.GetComponent<MoveForward>().speed = speed;
    }
}