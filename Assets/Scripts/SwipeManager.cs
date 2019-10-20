using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//Modified from https://forum.unity.com/threads/mouse-drag-direction-detection.437759/
public class SwipeManager : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{ 
    public static SwipeManager instance = null;

    Vector2 _lastPosition = Vector2.zero;
    public System.Action<Vector2> OnSwipeStart;
    public System.Action<Vector2> OnSwipeEnd;
    public System.Action<Vector2> OnSwipe;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _lastPosition = eventData.position;
        if(OnSwipeStart != null)
            OnSwipeStart(eventData.position);
    }
 
    public void OnEndDrag(PointerEventData eventData)
    {
        if(OnSwipeEnd != null)
            OnSwipeEnd(eventData.position);
    }
 
    public void OnDrag(PointerEventData eventData)
    {
        Vector2 direction = eventData.position - _lastPosition;
        _lastPosition = eventData.position;
 
        if(OnSwipe != null)
            OnSwipe(direction);
    }

    void Awake()
    {
        if(instance == null || instance.Equals(null))
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
}
