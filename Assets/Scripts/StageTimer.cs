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
            if (StageLink.instance.findStagePosition().y == 2)
            {
                if (StageLink.instance.gameData.diamond.taken)
                    UISingleton.instance.GetComponentInChildren<UITips>().writeTip("You dropped the ring: yo won !");
                else
                    UISingleton.instance.GetComponentInChildren<UITips>().writeTip("Comeback with the ring to save the world !");
            }
            else
            {
                foreach (StageChanger stageChanger in stageChangers)
                {
                    stageChanger.ChangeToTrigger();
                }
                directionFeedback.SetActive(true);
            }
            if(GameObject.Find("Generators"))
                GameObject.Find("Generators").SetActive(false);
        }
    }
}
