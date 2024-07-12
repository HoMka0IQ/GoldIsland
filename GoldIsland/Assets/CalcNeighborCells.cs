using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
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
    [SerializeField] Collider[] colliders;
    [SerializeField] Collider[][] colliderss;
    [Space(15f)]
    [Header("Buff")]
    [SerializeField] Cell_SO.CellType checkBuffCellType;

    [Header("Debuff")]
    [SerializeField] Cell_SO.CellType checkDebuffCellType;

    [Header("Buff Text")]
    [SerializeField] GameObject bonusTextParent;
    [SerializeField] GameObject bonusTextPrefab;
    List<TextCellData> allTexts = new List<TextCellData>();

    [Header("Buff Zone")]
    [SerializeField] GameObject bonusZoneParent;
    [SerializeField] GameObject bonusZonePrefab;
    List<ZoneCellData> allConnection = new List<ZoneCellData>();


    [Space(15f)]
    [Header("Other")]
    [SerializeField] Animation anim;
    Camera cam;

    private void Start()
    {
        cam = Camera.main;

    }
    public List<Vector3> FindAllBuffCellsPos(List<Cell> cells)
    {
        List<Vector3> pos = new List<Vector3>();
        for (int i = 0; i < cells.Count; i++)
        {
            if (cells[i].cell_SO.cellTypes == checkBuffCellType)
            {
                pos.Add(cells[i].transform.position);
            }
            
        }
        if (true)
        {
            
        }
        return pos;
    }
    public void CalcNeighborCell()
    {
        colliderss = new Collider[8][];

        colliders = new Collider[0];
        for (int i = 0; i < allCheckPos.Length; i++)
        {
            Vector3 position = allCheckPos[i] + transform.position + Vector3.up * (Vector3.one.y / 2);
            Collider[] cellColliders = Physics.OverlapBox(position, Vector3.one / 2, Quaternion.identity, checkLayer);
            colliders = colliders.Concat(cellColliders).ToArray();

        }
        List<Cell> Cells = new List<Cell>();
        for (int i = 0; i < colliders.Length; i++)
        {
            Cell cell;
            colliders[i].gameObject.TryGetComponent<Cell>(out cell);
            if (cell != null && (cell.cell_SO.cellTypes == checkBuffCellType || cell.cell_SO.cellTypes == checkDebuffCellType))
            {
                Cells.Add(cell);
            }
        }
        List<Vector3> CellsPos = new List<Vector3>();
        for (int i = 0; i < Cells.Count; i++)
        {
            CellsPos.Add(Cells[i].transform.position);
        }


        while (Cells.Count > allTexts.Count)
        {
            allTexts.Add(Instantiate(bonusTextPrefab, bonusTextParent.transform).GetComponent<TextCellData>());
            allConnection.Add(Instantiate(bonusZonePrefab, bonusZoneParent.transform).GetComponent<ZoneCellData>());
        }

        for (int i = 0; i < allTexts.Count; i++)
        {
            allTexts[i].gameObject.SetActive(false);
            allConnection[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < Cells.Count; i++)
        {
            allTexts[i].gameObject.SetActive(true);
            allConnection[i].gameObject.SetActive(true);
            allConnection[i].transform.position = Cells[i].transform.position + Vector3.up;

            if (Cells[i].cell_SO.cellTypes == checkBuffCellType)
            {
                allTexts[i].SetBuffData("+10%", Cells[i].gameObject);
                allConnection[i].SetBuffData();
            }
            if (Cells[i].cell_SO.cellTypes == checkDebuffCellType)
            {
                allTexts[i].SetDebuffData("-10%", Cells[i].gameObject);
                allConnection[i].SetDebuffData();
            }
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
