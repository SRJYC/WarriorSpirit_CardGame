using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CursorType : int
{
    Target = 0,
    Normal,
}

[CreateAssetMenu(menuName = "Other/Cursor")]
public class CursorTexture : ScriptableObject
{
    [SerializeField] private List<Texture2D> textures = null;

    public void ChangeCursor(CursorType type)
    {
        if(type == CursorType.Normal)
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        else
        {
            int index = (int)type;
            if(index < textures.Count)
                Cursor.SetCursor(textures[index], Vector2.zero, CursorMode.Auto);
        }
    }
}
