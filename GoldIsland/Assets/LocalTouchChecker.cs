using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LocalTouchChecker : MonoBehaviour
{
    [SerializeField] GraphicRaycaster uiRaycaster;
    [SerializeField] EventSystem eventSystem;

    [SerializeField] int currentTouches;

    [SerializeField] UnityEvent ifZeroTouch;
    private void Start()
    {
        eventSystem = FindObjectOfType<EventSystem>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            PointerEventData pointerData = new PointerEventData(eventSystem)
            {
                position = Input.mousePosition
            };

            List<RaycastResult> result = new List<RaycastResult>();
            uiRaycaster.Raycast(pointerData, result);

            Debug.Log("Number of UI elements under mouse click: " + result.Count);
            currentTouches = result.Count;
            invokeEvent();
        }
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            PointerEventData pointerData = new PointerEventData(eventSystem)
            {
                position = touch.position
            };

            List<RaycastResult> result = new List<RaycastResult>();
            uiRaycaster.Raycast(pointerData, result);

            Debug.Log("Number of UI elements under touch: " + result.Count);
            currentTouches = result.Count;
            invokeEvent();
        }

    }
    public void invokeEvent()
    {
        if (currentTouches == 0 && ifZeroTouch != null && CameraMovement.instance.drag == false)
        {
            ifZeroTouch.Invoke();
        }
    }
}
