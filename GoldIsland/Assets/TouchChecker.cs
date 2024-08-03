using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchChecker : MonoBehaviour
{
    private bool isTouching = false;
    private Vector2 initialTouchPosition;

    public static TouchChecker instance;
    private void Awake()
    {
        instance = this;
    }
    private void Update()
    {
        if (Input.touchCount > 0 && !EventSystem.current.IsPointerOverGameObject(0))
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    initialTouchPosition = touch.position;

                    isTouching = true;
                    break;

                case TouchPhase.Moved:
                    isTouching = false;
                    break;
            }
        }
    }
    public bool IsPointerOverUIObject(Touch touch)
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = new Vector2(touch.position.x, touch.position.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }
    public bool GetTrueTouch()
    {
        return isTouching;
    }

}
