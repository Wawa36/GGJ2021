using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaledShadow : MonoBehaviour
{
    public float maxY;
    [SerializeField]
    private float baseY = 2f;
    [SerializeField]
    private float minScale = 0.05f;
    [SerializeField]
    private float maxScale = 1f;
    [SerializeField]
    private float baseScale = 2f;

    [SerializeField]
    private Transform obj;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float diff = Mathf.Max(obj.position.y - baseY,0);
        float t = 1 - diff / (maxY - baseY);
        t = minScale * (1f - t) + t * maxScale;

        transform.localScale = new Vector3(t, t, t) * baseScale;
    }
}
