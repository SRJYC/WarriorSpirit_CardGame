using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowManager : Singleton<WindowManager>
{
    public DisplaySwitch[] m_Windows;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FocusBoard()
    {
        foreach(DisplaySwitch display in m_Windows)
        {
            display.SetActive(false);
        }
    }
}
