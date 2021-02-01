using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        if(StageLink.instance.ready())
            time += Time.deltaTime;
        UISingleton.instance.GetComponentInChildren < Slider > ().value = (stageTime - time) / stageTime;
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

            if(GameObject.Find("Generators"))
                GameObject.Find("Generators").SetActive(false);
        }
    }
}
