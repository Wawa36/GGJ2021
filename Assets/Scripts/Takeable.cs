using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Takeable : MonoBehaviour
{

    [SerializeField]
    private string stageObjectName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) {
            StageLink.instance.takeAndDeactivateObject(stageObjectName, true);
            Destroy(gameObject);
        }
    }
}
