using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchChecker : MonoBehaviour
{
    public List<GraphicRaycaster> uiRaycaster;
    public EventSystem eventSystem;
    
    public static TouchChecker instance;
    private void Awake()
    {
        instance = this;
    }
    public int GetTouchCount()
    {
        List<RaycastResult> results = new List<RaycastResult>();
        if (Input.GetMouseButtonDown(0))
        {
            PointerEventData pointerData = new PointerEventData(eventSystem)
            {
                position = Input.mousePosition
            };
            
            foreach (GraphicRaycaster raycaster in uiRaycaster)
            {
                List<RaycastResult> result = new List<RaycastResult>();
                raycaster.Raycast(pointerData, result);
                results.AddRange(result);
            }

            Debug.Log("Number of UI elements under mouse click: " + results.Count);

            return results.Count;
        }
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            PointerEventData pointerData = new PointerEventData(eventSystem)
            {
                position = touch.position
            };

            foreach (GraphicRaycaster raycaster in uiRaycaster)
            {
                List<RaycastResult> result = new List<RaycastResult>();
                raycaster.Raycast(pointerData, result);
                results.AddRange(result);
            }

            Debug.Log("Number of UI elements under touch: " + results.Count);

            return results.Count;
        }
        return results.Count;
    }

}
