using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseScreenCheck
{
    /// <summary>
    /// Check Cursor Position
    /// </summary>
    /// <returns>
    /// 0 represent out of screen
    /// 1 represent bottom left,
    /// 2 represent bottom right,
    /// 3 represent top left,
    /// 4 represent top right,
    /// If mouse is on the line, then it will be considered to be bottom or left
    /// </returns>
    public static int Check()
    {
        if (CheckCursorInside())
            return 0;
        else
            return CheckCursorQuadrant();
    }

    public static int Check(Vector2 origin)
    {
        if (CheckCursorInside())
            return 0;
        else
            return CheckCursorQuadrant(origin);
    }

    /// <summary>
    /// Check if cursor inside screen only
    /// </summary>
    /// <returns></returns>
    public static bool CheckCursorInside()
    {

#if UNITY_EDITOR
        if (Input.mousePosition.x <= 0 || Input.mousePosition.y <= 0 || Input.mousePosition.x >= UnityEditor.Handles.GetMainGameViewSize().x - 1 || Input.mousePosition.y >= UnityEditor.Handles.GetMainGameViewSize().y - 1)
        {
            return false;
        }
#else
        if (Input.mousePosition.x <= 0 || Input.mousePosition.y <= 0 || Input.mousePosition.x >= Screen.width - 1 || Input.mousePosition.y >= Screen.height - 1) {
            return false;
        }
#endif
        return true;
    }

    /// <summary>
    /// Check Cursor position
    /// </summary>
    /// <param name="origin">The origin of coordinate</param>
    /// <returns>
    /// 1 represent bottom left,
    /// 2 represent bottom right,
    /// 3 represent top left,
    /// 4 represent top right,
    /// If mouse is on the line, then it will be considered to be bottom or left
    /// </returns>
    public static int CheckCursorQuadrant(Vector2 origin)
    {
        if (Input.mousePosition.x > origin.x)
        {
            return Input.mousePosition.y > origin.y ? 4 : 2;
        }
        else
        {
            return Input.mousePosition.y > origin.y ? 3 : 1;
        }
    }

    /// <summary>
    /// Overload method which take center of screen as origin
    /// </summary>
    /// <returns></returns>
    public static int CheckCursorQuadrant()
    {
#if UNITY_EDITOR
        float halfW = UnityEditor.Handles.GetMainGameViewSize().x / 2.0f;
        float halfH = UnityEditor.Handles.GetMainGameViewSize().y / 2.0f;
#else
            float halfW = Screen.width / 2.0f;
            float halfH = Screen.height / 2.0f;
#endif
        return CheckCursorQuadrant(new Vector2(halfH, halfW));
    }
}
