using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class NormalizeScrollRect : MonoBehaviour
{
    private ScrollRect scrollRect;

    private bool reset = false;
    // Start is called before the first frame update
    void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        reset = true;
    }

    private void OnEnable()
    {
        reset = true;
    }

    private void Update()
    {
        if(reset)
        {
            reset = false;
            scrollRect.verticalNormalizedPosition = 1;
            scrollRect.horizontalNormalizedPosition = 0;
        }
    }


}
