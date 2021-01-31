using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    void Start()
    {
        this.transform.rotation = Quaternion.AngleAxis(this.transform.rotation.eulerAngles.y, -Camera.main.transform.forward) 
                                * Quaternion.AngleAxis(- Camera.main.transform.rotation.eulerAngles.x, Vector3.right);
    }
}
