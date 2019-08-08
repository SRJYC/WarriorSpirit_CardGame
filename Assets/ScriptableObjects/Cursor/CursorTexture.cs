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
            {
                Vector2 cursorHotspot = new Vector2(textures[index].width / 2, textures[index].height / 2);
                Cursor.SetCursor(textures[index], cursorHotspot, CursorMode.Auto);
            }
        }
    }
}
