using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectionIcons : MonoBehaviour
{
    [SerializeField]
    StageLink.StageMove move;

    float scaleOffset = 30f;

    // Start is called before the first frame update
    void Start()
    {
        List<StageLink.StageObject> objs = StageLink.instance.findStageObjects(move);

        int i = 0;
        foreach (StageLink.StageObject obj in objs) {
            GameObject icon = Instantiate(obj.icon, transform);

            Vector3 offset = +(objs.Count > 1 ? 1 : 0) *
                (Quaternion.AngleAxis(i * 360f / objs.Count, Vector3.forward) * Vector3.up) * scaleOffset;
            icon.GetComponent<RectTransform>().anchoredPosition = new Vector2(offset.x, offset.y); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
