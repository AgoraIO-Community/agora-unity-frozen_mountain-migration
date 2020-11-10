using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class LongPressButton : Button, IPointerClickHandler
{

    public float holdTime = 1.0f;
    public UnityEvent onLongPress = new UnityEvent();
    public UnityEvent onRightClick = new UnityEvent();
    public UnityEvent onLeftClick = new UnityEvent();

    public override void OnPointerDown(PointerEventData eventData)
    {
        Invoke("OnLongPress", holdTime);
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        CancelInvoke("OnLongPress");
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        CancelInvoke("OnLongPress");
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        base.OnPointerClick(eventData);
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            onRightClick.Invoke();
        }
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            onLeftClick.Invoke();
        }
    }

    void OnLongPress()
    {
        onLongPress.Invoke();
    }
}
