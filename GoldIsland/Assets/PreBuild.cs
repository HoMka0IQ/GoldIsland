using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreBuild : MonoBehaviour
{
    Camera cam;
    [Header("Main")]
    [SerializeField] Cell_SO cell_SO;
    [SerializeField] LayerMask layerMask;
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
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 5, Vector3.down * 10, out hit, 100, layerMask))
        {
            Debug.Log(LayerMask.LayerToName(hit.transform.gameObject.layer));
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("EmptyCell"))
            {
                Cell cell = hit.transform.gameObject.GetComponent<Cell>();
                cell.islandData.CellChanger(cell_SO, cell.posInArray);
            }
        }
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
