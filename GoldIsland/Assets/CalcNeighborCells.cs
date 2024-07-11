using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalcNeighborCells : MonoBehaviour
{
    [Header("Check Zone")]
    [SerializeField] LayerMask checkLayer;
    Vector3[] allCheckPos = new Vector3[]
    {
        new Vector3(2.5f,0,0),
        new Vector3(-2.5f,0,0),
        new Vector3(2.5f,0,2.5f),
        new Vector3(2.5f,0,-2.5f),
        new Vector3(-2.5f,0,2.5f),
        new Vector3(-2.5f,0,-2.5f),
        new Vector3(0,0,-2.5f),
        new Vector3(0,0,2.5f)
    };
    Collider[] colliders;

    [Space(15f)]
    [Header("Buff")]
    [SerializeField] Cell_SO.CellType checkBuffCellType;

    [Header("Debuff")]
    [SerializeField] Cell_SO.CellType checkDebuffCellType;

    [Header("Buff Text")]
    [SerializeField] GameObject bonusTextParent;
    [SerializeField] GameObject bonusTextPrefab;
    List<ConnectionScreenToWorld> allTexts = new List<ConnectionScreenToWorld>();

    [Header("Buff Zone")]
    [SerializeField] GameObject bonusZoneParent;
    [SerializeField] GameObject bonusZonePrefab;
    List<GameObject> allConnection = new List<GameObject>();


    [Space(15f)]
    [Header("Other")]
    [SerializeField] Animation anim;
    Camera cam;

    private void Start()
    {
        cam = Camera.main;

    }
    [ContextMenu("Reset Values")]
    void ResetValues()
    {
        // Логіка для скидання значень
    }
    public void CalcNeighborCell()
    {
        
        for (int i = 0; i < allCheckPos.Length; i++)
        {
            Vector3 position = allCheckPos[i] + transform.position + Vector3.up * (Vector3.one.y / 2);
            colliders = Physics.OverlapBox(position, Vector3.one / 2, Quaternion.identity, checkLayer);
        }
        List<Cell> cells = new List<Cell>();
        for (int i = 0; i < colliders.Length; i++)
        {
            Cell cell;
            colliders[i].gameObject.TryGetComponent<Cell>(out cell);
            if (cell != null && cell.cell_SO.cellTypes == checkBuffCellType)
            {
                cells.Add(cell);
            }
        }

        while (cells.Count > allTexts.Count)
        {
            allTexts.Add(Instantiate(bonusTextPrefab, bonusTextParent.transform).GetComponent<ConnectionScreenToWorld>());
            allConnection.Add(Instantiate(bonusZonePrefab, bonusZoneParent.transform));
        }

        for (int i = 0; i < allTexts.Count; i++)
        {
            allTexts[i].gameObject.SetActive(false);
            allConnection[i].SetActive(false);
        }

        for (int i = 0; i < cells.Count; i++)
        {
            allTexts[i].gameObject.SetActive(true);
            allTexts[i].target = cells[i].gameObject;
            allTexts[i].moveFromCenter = Vector3.up * 2;


            allConnection[i].SetActive(true);
            allConnection[i].transform.position = cells[i].transform.position + Vector3.up;
        }
    }
    public void MoveAnimPlay()
    {
        anim.Play();
    }
    void OnDrawGizmos()
    {
        for (int i = 0; i < allCheckPos.Length; i++)
        {
            Gizmos.color = Color.red;
            Vector3 position = allCheckPos[i] + transform.position + Vector3.up * (Vector3.one.y / 2);
            Gizmos.DrawCube(position, Vector3.one);
        }

    }
}
