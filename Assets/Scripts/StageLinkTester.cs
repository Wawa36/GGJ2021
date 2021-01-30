using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageLinkTester : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            StageLink.instance.changeScene(StageLink.StageMove.LEFT);
        }
        else if (Input.GetMouseButtonDown(1)) {
            StageLink.instance.changeScene(StageLink.StageMove.RIGHT);
        } 
        else if (Input.GetMouseButtonDown(2)) {
            StageLink.instance.changeScene(StageLink.StageMove.UP);
        }
    }
}
