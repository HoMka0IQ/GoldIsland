using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreBuild : MonoBehaviour
{
    Camera cam;
    [Header("Main")]
    [SerializeField] Cell_SO cell_SO;
    [SerializeField] LayerMask layerMask;
    public GameObject goCellUnder;
    public BuildUI buildUI;
    [Header("UI")]
    [SerializeField] GameObject actionBtnsCenter;
    [SerializeField] Animation actionBtnsAnim;
    void Start()
    {
        cam = Camera.main;
        if (actionBtnsAnim == null)
        {
            actionBtnsAnim = actionBtnsCenter.GetComponent<Animation>();
        }
    }
    void Update()
    {
        if (actionBtnsCenter.activeSelf == true)
        {
            actionBtnsCenter.transform.position = cam.WorldToScreenPoint(transform.position);
        }
        
    }
    public void AcceptBtn()
    {
        Cell cell = goCellUnder.GetComponent<Cell>();
        cell.islandData.CellChanger(cell_SO, cell.posInArray);
        cell.islandData.RefreshBuildsDataExcept(cell.posInArray);
        gameObject.SetActive(false);
        buildUI.ResetUI();
    }
    public void CancelBtn()
    {
        gameObject.SetActive(false);
        buildUI.ResetUI();
    }
    public void ShowActionBtns()
    {
        actionBtnsCenter.SetActive(true);
        actionBtnsAnim.Play();
    }
    public void HideActionBtns()
    {
        actionBtnsCenter.SetActive(false);
    }
}
