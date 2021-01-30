using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTimer : MonoBehaviour
{
    [SerializeField] float stageTime;
    [SerializeField] float time;
    [SerializeField] GameObject directionFeedback;

    [SerializeField] List<StageChanger> stageChangers = new List<StageChanger>();

    void Start()
    {
        time = 0;
    }

    void Update()
    {
        time += Time.deltaTime;
        TimerIsOver();
    }

    void TimerIsOver()
    {
        if(time >= stageTime)
        {
            foreach(StageChanger stageChanger in stageChangers)
            {
                stageChanger.ChangeToTrigger();
            }
            directionFeedback.SetActive(true);
        }
    }
}
