using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayAtHeight : MonoBehaviour
{
    [SerializeField] float height;

    void Update()
    {
        this.transform.position = new Vector3(this.transform.parent.position.x,
                                                                 height,
                                             this.transform.parent.position.z);
    }
}
