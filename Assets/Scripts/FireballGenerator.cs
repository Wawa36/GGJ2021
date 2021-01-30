using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballGenerator : Generator
{

    [SerializeField]
    private float fireballSpeed;

    [SerializeField]
    private GameObject fireballPrefab;

    private Transform baseEnvironment;

    public void Start()
    {
        baseEnvironment = GameObject.Find("Base Environment").transform;
    }
    protected override void generate()
    {
        GameObject fireball = Instantiate(fireballPrefab);
        fireball.transform.position = this.transform.position;
        fireball.transform.rotation = this.transform.rotation;
        fireball.transform.parent = baseEnvironment;
        fireballPrefab.GetComponent<MoveForward>().speed = fireballSpeed;
    }
}
