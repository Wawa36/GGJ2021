using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct FLGData {
    public Vector3 outerPoint;
    public Vector3 innerPoint;
}
public class FireballLineGenerator : Generator<FLGData>
{

    [SerializeField]
    private float fireballSpeed;

    [SerializeField]
    private GameObject fireballPrefab;

    private Transform baseEnvironment;

    [SerializeField]
    private bool autoPlacement = true;

    [SerializeField]
    private float outerDist = 25;
    [SerializeField]
    private float innerDist = 3;
    [SerializeField]
    private float initHeight = 0.5f;


    public void Start()
    {
        baseEnvironment = GameObject.Find("Base Environment").transform;
    }

    protected override void endGenerate(FLGData data)
    {
    }

    protected override void generate(FLGData data, int frame)
    {
        if (autoPlacement)
        {
            generateLine(data.outerPoint, (data.innerPoint - data.outerPoint).normalized);
        }
        else {
            generateLine(this.transform.position, this.transform.forward);
        }
    }

    protected override FLGData initGenerate(int index)
    {
        FLGData d;
        d.outerPoint = new Vector3(0, initHeight, outerDist);
        d.outerPoint = Quaternion.AngleAxis(Random.Range(0.0f, 360.0f), Vector3.up) * d.outerPoint;
        d.innerPoint = new Vector3(0, initHeight, Random.Range(0, innerDist));
        d.innerPoint = Quaternion.AngleAxis(Random.Range(0.0f, 360.0f), Vector3.up) * d.innerPoint;
        return d;
    }

    private void generateLine(Vector3 pos, Vector3 dir) {
        GameObject fireball = Instantiate(fireballPrefab);
        fireball.transform.position = pos;
        fireball.transform.rotation = Quaternion.LookRotation(dir, Vector3.up);
        fireball.transform.parent = baseEnvironment;
        fireballPrefab.GetComponent<MoveForward>().speed = fireballSpeed;
    }
}
