using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct FPGData
{
    public Vector3 pos;
    public Vector3 dir;
}
public class FireballPackGenerator : Generator<FPGData>
{

    [SerializeField]
    private float fireballSpeed;

    [SerializeField]
    private GameObject fireballPrefab;

    private Transform baseEnvironment;

    [SerializeField]
    private bool autoPlacement = true;

    [SerializeField]
    private float outerDist = 11;
    [SerializeField]
    private float initHeight = 0.5f;

    [SerializeField]
    private int fireballsCount = 5;

    [SerializeField]
    private float frontOffset = 1;

    [SerializeField]
    private float lateralOffset = 1;


    public void Start()
    {
        baseEnvironment = GameObject.Find("Base Environment").transform;
    }

    protected override void endGenerate(FPGData data)
    {
    }

    protected override void generate(FPGData data, int frame)
    {
        if (autoPlacement)
        {
            generatePack(data.pos, data.dir);
        }
        else
        {
            generatePack(this.transform.position, this.transform.forward);
        }
    }

    protected override FPGData initGenerate(int index)
    {
        FPGData d;
        d.pos = new Vector3(0, initHeight, outerDist);
        d.pos = Quaternion.AngleAxis(Random.Range(0.0f, 360.0f), Vector3.up) * d.pos;

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        Vector3 playerPos = Vector3.up;
        if(player)
            playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;

        d.dir = (playerPos - d.pos).normalized;
        d.dir.y = 0;
        d.dir = d.dir.normalized;
        return d;
    }
        
    private void generatePack(Vector3 pos, Vector3 dir)
    {
        Vector3 dirPerp = Vector3.Cross(dir, Vector3.up);
        for (int i = 0; i < fireballsCount; i++)
        {
            GameObject fireball = Instantiate(fireballPrefab);
            float lateral = i - (fireballsCount-1) / 2.0f;
            fireball.transform.position = pos + dirPerp * lateral * lateralOffset + (i%2) * frontOffset * dir;
            fireball.transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
            fireball.transform.parent = baseEnvironment;
            fireballPrefab.GetComponent<MoveForward>().speed = fireballSpeed;

        }
    }
}

