using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinimapScript : MonoBehaviour
{

    [SerializeField]
    private float scale = 20f;

    [SerializeField]
    private float severalObjectsOffset = 2.0f;

    [SerializeField]
    private GameObject playerIcon;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateMinimap() {

        foreach(Transform ch in transform) {
            Destroy(ch.gameObject);
        }
        List<StageLink.StageObject> objs = StageLink.instance.allActiveStageObjects();

        RectTransform thisTr = GetComponent<RectTransform>();
        Dictionary<StageLink.StagePosition, int> countAtPos = new Dictionary<StageLink.StagePosition, int>();
        Dictionary<StageLink.StagePosition, int> indexAtPos = new Dictionary<StageLink.StagePosition, int>();

        countAtPos[StageLink.instance.findStagePosition()] = 1;
        foreach (StageLink.StageObject obj in objs) {
            if (countAtPos.ContainsKey(obj.pos)) {
                countAtPos[obj.pos]++;
            } else {
                countAtPos[obj.pos] = 1;
            }
        }

        foreach (StageLink.StagePosition pos in countAtPos.Keys) {
            indexAtPos[pos] = countAtPos[pos] - 1;
        }
        
        foreach (StageLink.StageObject obj in objs) {
            int index = indexAtPos[obj.pos];
            int count = countAtPos[obj.pos];
            indexAtPos[obj.pos]--;

            Vector3 pos = StageLink.instance.positionToVector(obj.pos) * scale;
            GameObject img = Instantiate(obj.icon);
            RectTransform tr = img.GetComponent<RectTransform>();
            tr.parent = thisTr;
            tr.anchoredPosition = pos + Quaternion.AngleAxis(360f * index / count, Vector3.forward) * Vector3.right
                * ((count > 1) ? severalObjectsOffset : 0f);
        }

        GameObject icon = Instantiate(playerIcon);
        Vector3 playerPos = StageLink.instance.positionToVector(StageLink.instance.findStagePosition()) * scale;
        RectTransform t = icon.GetComponent<RectTransform>();
        t.parent = thisTr;
        t.anchoredPosition = playerPos;
    }
}
