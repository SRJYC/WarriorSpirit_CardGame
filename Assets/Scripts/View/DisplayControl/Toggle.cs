using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Toggle : MonoBehaviour
{
    public Image image;

    public Color color_on;
    public Color color_off;

    public void Display(bool on)
    {
        if (on)
            image.color = color_on;
        else
            image.color = color_off;
    }


}
