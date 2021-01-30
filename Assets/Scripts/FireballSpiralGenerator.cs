using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSGData
{
    public Vector3 pos;
    public Vector3 dir;
}
public class FireballSpiralGenerator : Generator<FSGData>
{

    [SerializeField]
    private float fireballSpeed;

    [SerializeField]
    private GameObject fireballPrefab;

    private Transform baseEnvironment;

    [SerializeField]
    private bool autoPlacement = true;

    [SerializeField]
    private float angularStep;

    [SerializeField]
    private float initHeight = 0.5f;

    [SerializeField]
    private float outerDist = 10;

    [SerializeField]
    private int fireballCount = 5;


    public void Start()
    {
        baseEnvironment = GameObject.Find("Base Environment").transform;
    }

    protected override void endGenerate(FSGData data)
    {
    }

    protected override void generate(FSGData data)
    {
        
        if (autoPlacement)
        {
            Debug.Log(data.dir);
            data.dir = Quaternion.AngleAxis(angularStep, Vector3.up) * data.dir;
            Debug.Log(data.dir);
            generateSpiral(data.pos, data.dir);
            
        }
        else
        {
            this.transform.rotation = Quaternion.AngleAxis(angularStep, Vector3.up) * this.transform.rotation;
            generateSpiral(this.transform.position, this.transform.forward);
        }

    }

    protected override FSGData initGenerate()
    {
        FSGData d = new FSGData();
        d.pos = new Vector3(0, initHeight, outerDist);
        d.pos = Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.up) * d.pos;
        d.dir = Quaternion.AngleAxis(Random.Range(0f, 360f), Vector3.up) * Vector3.forward;
        
        return d;
    }

    private void generateSpiral(Vector3 pos, Vector3 dir)
    {
        for (int i = 0; i < fireballCount; i++)
        {
            GameObject fireball = Instantiate(fireballPrefab);
            fireball.transform.position = pos;
            fireball.transform.rotation = Quaternion.AngleAxis(360f / fireballCount * i, Vector3.up) * Quaternion.LookRotation(dir, Vector3.up);
            fireball.transform.parent = baseEnvironment;
            fireballPrefab.GetComponent<MoveForward>().speed = fireballSpeed;
        }
    }
}