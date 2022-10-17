using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNameFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField, Range(0,1)] private float smoothing = 0.03f;
    [SerializeField] private float offSetX;
    [SerializeField] private float offSetY;

    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        if (target)
        {
            Vector3 nextPos = Vector3.Lerp(rectTransform.position, target.position, smoothing);
            nextPos.x += offSetX;
            nextPos.y += offSetY;
            rectTransform.position = nextPos;
        }
    }
}
