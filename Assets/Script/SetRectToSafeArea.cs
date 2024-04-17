using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRectToSafeArea : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        RectTransform tRect = GetComponent<RectTransform>();
        tRect.offsetMin = new Vector2(Screen.safeArea.xMin, Screen.safeArea.yMin);
        tRect.sizeDelta = new Vector2(Screen.safeArea.width, Screen.safeArea.height);
    }
}
