using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class CardDraggable : MonoBehaviour,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler
{
    private CanvasGroup canvasGroup = null;

    public bool isDragged { get; set; }

    protected Vector3 dragOffset = Vector3.zero;

    //the parent to move when end drag, 
    //should be the origin parent if nothing happen
    [HideInInspector]public Transform parentToMoveTo = null;

    private static readonly float alphaWhenDrag = 0.5f;

    public CursorTexture cursorTextureController;

    private bool delayDisable = false;
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void OnEnable()
    {
        isDragged = false;
        delayDisable = false;
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        isDragged = true;

        //make card transparent
        canvasGroup.alpha = alphaWhenDrag;

        //change cursor texture
        cursorTextureController.ChangeCursor(CursorType.Target);


        //Debug.Log("Parent before: " + transform.parent.gameObject);
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        //change position
        //transform.position = dragOffset + (Vector3)eventData.position;
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        isDragged = false;

        //change parent
        if (parentToMoveTo != null && parentToMoveTo != this.transform.parent)
            this.transform.SetParent(parentToMoveTo);

        //make card transparent
        canvasGroup.alpha = 1.0f;

        //change cursor texture
        cursorTextureController.ChangeCursor(CursorType.Normal);

        //Debug.Log("Parent after: " + transform.parent.gameObject);

        if (delayDisable)
            RemoveSelf();
    }

    public virtual void Disable()
    {
        if(isDragged)
            delayDisable = true;
        else
            RemoveSelf();
    }

    private void RemoveSelf()
    {
        GameObject.Destroy(this);
    }
}
