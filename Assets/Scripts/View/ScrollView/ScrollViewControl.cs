using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollViewControl : MonoBehaviour
{
    public RectTransform target;

    public ScrollRect scrollRect;
    // Start is called before the first frame update
    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    [ContextMenu("SetPosition")]
    public void Set()
    {
        float scrollValue = 1 + target.anchoredPosition.y / scrollRect.content.rect.height;
        scrollRect.verticalNormalizedPosition = scrollValue;
        Debug.Log(scrollValue);
    }
}
