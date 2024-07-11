using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildUI : MonoBehaviour, IDragHandler, IEndDragHandler
{

    [Header("Ray")]
    Camera mainCamera;
    [SerializeField] LayerMask layer;

    [Header("Build")]
    [SerializeField] BuildCell_So buildCell_SO;
    GameObject prebuildGO;
    CalcNeighborCells calcNeighborCells;
    PreBuild preBuild;
    Vector3 lastPos;
    [SerializeField] Transform prebuildParent;

    [Header("Main")]
    [SerializeField] Image image;
    bool dragging;
    void Start()
    {
        mainCamera = Camera.main;
        prebuildGO = Instantiate(buildCell_SO.prebuildPrefab, prebuildParent);
        calcNeighborCells = prebuildGO.GetComponent<CalcNeighborCells>();
        preBuild = prebuildGO.GetComponent<PreBuild>();
        preBuild.buildUI = this;
        prebuildGO.SetActive(false);
        if (image == null)
        {
            image = GetComponent<Image>();
        }
    }
    private void OnEnable()
    {
        transform.localPosition = Vector3.zero;
        Color color = image.color;
        color.a = 1;
        image.color = color;
    }

    void Update()
    {
        if (!dragging && prebuildGO.activeSelf == true)
        {
            transform.position = mainCamera.WorldToScreenPoint(prebuildGO.transform.position);
        }
        
    }


    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Drag");
        dragging = true;
        preBuild.HideActionBtns();
        transform.position = eventData.position;
        Ray ray = mainCamera.ScreenPointToRay(transform.position);
        RaycastHit hit;
        Color color = image.color;
        color.a = 0;
        image.color = color;
        if (Physics.Raycast(ray, out hit, 55f, layer))
        {
            Debug.DrawRay(ray.origin, ray.direction * 55f, Color.cyan);

            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("EmptyCell"))
            {
                if (lastPos != hit.collider.gameObject.transform.position || prebuildGO.activeSelf == false)
                {
                    prebuildGO.SetActive(true);
                    prebuildGO.transform.position = hit.collider.gameObject.transform.position;
                    calcNeighborCells.MoveAnimPlay();
                    calcNeighborCells.CalcNeighborCell();
                    lastPos = hit.collider.gameObject.transform.position;
                }
                return;
            }

        }
        prebuildGO.SetActive(false);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragging = false;
        preBuild.ShowActionBtns();
        if (prebuildGO.activeSelf == false)
        {
            ResetUI();
        }
    }
    public void ResetUI()
    {
        transform.localPosition = Vector3.zero;
        Color color = image.color;
        color.a = 1;
        image.color = color;
    }
}
