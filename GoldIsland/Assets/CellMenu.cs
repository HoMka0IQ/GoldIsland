using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CellMenu : MonoBehaviour
{
    [SerializeField] GameObject cellMenuUI;
    ConnectionScreenToWorld connectionScreenToWorld;

    [SerializeField] Animation menuAnim;
    private bool isTouching = false;
    private Vector2 initialTouchPosition;
    [SerializeField] LayerMask checkLayer;

    bool openTimer;

    public static CellMenu instance;
    private void Awake()
    {
        instance = this;
        connectionScreenToWorld = cellMenuUI.GetComponent<ConnectionScreenToWorld>();
    }
    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && TouchChecker.instance.GetTrueTouch() && openTimer == false && TouchChecker.instance.IsPointerOverUIObject(Input.GetTouch(0)) == false)
        {
            CloseCellMenu();
        }
    }

    public void CloseCellMenu()
    {
        cellMenuUI.SetActive(false);
    }
    public void OpenCellMenu(Cell cell, GameObject go)
    {
        openTimer = true;
        cellMenuUI.SetActive(true);
        connectionScreenToWorld.setData(go);
        menuAnim.Play();
        cell.PlayAnim();
        Invoke("offOpenTimer", 0.01f);
    }
    void offOpenTimer()
    {
        openTimer = false;
    }

    public void DeleteCell()
    {
        BuildZonesManager.instance.HideAllZones();
        CloseCellMenu();
    }
}
