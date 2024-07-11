using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMovement : MonoBehaviour, IDragHandler
{
    public Vector3 startPos;
    [SerializeField] float speed;
    [SerializeField] Vector2 camPos;
    public Vector2 xBorder;
    public Vector2 zBorder;

    [SerializeField] private GameObject cameraGO;
    [SerializeField] private float rotationAngle = -45f; // Кут обертання

    [Header("More settings")]
    [SerializeField] float increaseRangeZoneX;
    [SerializeField] float increaseRangeZoneY;
    public static CameraMovement instance;


    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        camPos = new Vector2(startPos.x, startPos.z);
        speed = Screen.width / 10;
        SetBorders();
    }
    public void SetBorders()
    {
        List<IslandData> islands = IslandBuilding.Instance.GetIslands();
        xBorder = Vector2.zero;
        zBorder = Vector2.zero;
        for (int i = 0; i < islands.Count; i++)
        {
            if (xBorder.x < islands[i].gameObject.transform.position.x)
            {
                xBorder.x = islands[i].transform.position.x;
            }
            if (xBorder.y > islands[i].transform.position.x)
            {
                xBorder.y = islands[i].transform.position.x;
            }

            if (zBorder.x < islands[i].transform.position.z)
            {
                zBorder.x = islands[i].transform.position.z;
            }
            if (zBorder.y > islands[i].transform.position.z)
            {
                zBorder.y = islands[i].transform.position.z;
            }
        }
        xBorder += new Vector2(increaseRangeZoneY, -increaseRangeZoneX);
        zBorder += new Vector2(increaseRangeZoneX, -increaseRangeZoneY);
    }
    private void OnDrawGizmos()
    {
        Vector3 point1 = new Vector3(xBorder.x, cameraGO.transform.position.y, zBorder.y);
        Vector3 point2 = new Vector3(xBorder.x, cameraGO.transform.position.y, zBorder.x);
        Vector3 point3 = new Vector3(xBorder.y, cameraGO.transform.position.y, zBorder.x);
        Vector3 point4 = new Vector3(xBorder.y, cameraGO.transform.position.y, zBorder.y);
        Debug.DrawLine(point1, point2, Color.blue);
        Debug.DrawLine(point2, point3, Color.blue);
        Debug.DrawLine(point3, point4, Color.blue);
        Debug.DrawLine(point4, point1, Color.blue);
    }
    public void OnDrag(PointerEventData eventData)
    {
        // Обрахування зміщення
        camPos.x += eventData.delta.y / speed;
        camPos.y -= eventData.delta.x / speed;

        // Обрахування нової позиції з врахуванням обертання
        float angleRad = rotationAngle * Mathf.Deg2Rad;
        float cos = Mathf.Cos(angleRad);
        float sin = Mathf.Sin(angleRad);

        float newX = camPos.x * cos - camPos.y * sin;
        float newZ = camPos.x * sin + camPos.y * cos;

        // Обмеження camPos за локальними осями
        if (newX > xBorder.x)
        {
            newX = xBorder.x;
        }
        if (newX < xBorder.y)
        {
            newX = xBorder.y;
        }

        if (newZ > zBorder.x)
        {
            newZ = zBorder.x;
        }
        if (newZ < zBorder.y)
        {
            newZ = zBorder.y;
        }

        // Застосування нової позиції
        Vector3 localPosition = new Vector3(newX, cameraGO.transform.position.y, newZ);
        cameraGO.transform.localPosition = localPosition;

        // Зворотне обчислення camPos з врахуванням нової позиції
        camPos.x = localPosition.x * cos + localPosition.z * sin;
        camPos.y = -localPosition.x * sin + localPosition.z * cos;
    }


}
