using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

public class OnScreenJoystick: OnScreenControl
{ 
    public RectTransform joystick;
    private Vector2 originalPosition;
    private Vector2 joystickTouchPos;
    public RectTransform joystickBG;
    public float joystickRadius;
    public Vector2 joystickVec;

    [InputControl(layout = "Vector2")]
    [SerializeField]
    private string m_ControlPath;

    protected override string controlPathInternal
    {
        get => m_ControlPath;
        set => m_ControlPath = value;
    }

    private void Start()
    {
        originalPosition = joystickBG.transform.position;
        joystickRadius = joystickBG.GetComponent<RectTransform>().sizeDelta.y / 4;
        joystickTouchPos = originalPosition;
    }

    public void OnPointerDown()
    {
        joystick.transform.position = Input.mousePosition;
        joystickBG.transform.position = Input.mousePosition;
        joystickTouchPos = Input.mousePosition;
    }

    public void OnPointerUp()
    {
        joystickVec = Vector2.zero;
        joystick.transform.position = originalPosition;
        joystickBG.transform.position = originalPosition;
        SendValueToControl(Vector2.zero);
    }

    public void EndDrag()
    {
        joystickVec = Vector2.zero;
        joystick.transform.position = originalPosition;
        joystickBG.transform.position = originalPosition;
        joystickTouchPos = originalPosition;
        SendValueToControl(Vector2.zero);

    }

    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;
        joystickVec = (dragPos - joystickTouchPos).normalized;
        float joystickDict = Vector2.Distance(dragPos, joystickTouchPos);

        if (joystickDict < joystickRadius)
        {
            joystick.transform.position = joystickTouchPos + joystickVec * joystickDict;

        }
        else
        {
            joystick.transform.position = joystickTouchPos + joystickVec * joystickRadius;
        }

        SendValueToControl(joystickVec);

    }
}
