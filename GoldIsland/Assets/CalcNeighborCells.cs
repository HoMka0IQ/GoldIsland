﻿using System.Collections;
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
    [Space(15f)]
    [Header("Buff")]
    [SerializeField] Cell_SO.CellType checkBuffCellType;

    [Header("Debuff")]
    [SerializeField] Cell_SO.CellType checkDebuffCellType;

    [Header("Buff Text")]
    [SerializeField] GameObject bonusTextParent;
    [SerializeField] GameObject bonusTextPrefab;
    [SerializeField] List<TextCellData> allTexts = new List<TextCellData>();

    [Header("Buff Zone")]
    [SerializeField] GameObject bonusZoneParent;
    [SerializeField] GameObject bonusZonePrefab;
    [SerializeField] List<ZoneCellData> allZoneCell = new List<ZoneCellData>();


    [Space(15f)]
    [Header("Other")]
    [SerializeField] Animation anim;
    Camera cam;

    private void Start()
    {
        cam = Camera.main;

    }
    public List<Vector3> FindAllCellType(List<Cell> cells, Collider[] colliders, Cell_SO.CellType findType)
    {
        List<Vector3> pos = new List<Vector3>();
        for (int i = 0; i < cells.Count; i++)
        {
            if (cells[i].cell_SO.cellTypes == findType)
            {
                pos.Add(cells[i].transform.position);
            }
            
        }
        if (findType == Cell_SO.CellType.Empty)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] == null)
                {
                    pos.Add(allCheckPos[i] + transform.position);
                }
            }
        }
        //c
        return pos;
    }
    public void CalcNeighborCell()
    {
        colliders = new Collider[0];
        for (int i = 0; i < allCheckPos.Length; i++)
        {
            Vector3 position = allCheckPos[i] + transform.position + Vector3.up * (Vector3.one.y / 2);
            Collider[] cellColliders = Physics.OverlapBox(position, Vector3.one / 2, Quaternion.identity, checkLayer);
            if (cellColliders.Length > 0)
            {
                colliders = colliders.Concat(cellColliders).ToArray();
            }
            else
            {
                colliders = colliders.Concat(new Collider[1]).ToArray();
            }
            

        }
        List<Cell> Cells = new List<Cell>();
        for (int i = 0; i < colliders.Length; i++)
        {
            Cell cell;
            if (colliders[i] != null)
            {
                colliders[i].gameObject.TryGetComponent<Cell>(out cell);
                if (cell != null && (cell.cell_SO.cellTypes == checkBuffCellType || cell.cell_SO.cellTypes == checkDebuffCellType))
                {
                    Cells.Add(cell);
                }
            }
            
        }
        for (int i = 0; i < allTexts.Count; i++)
        {
            allTexts[i].gameObject.SetActive(false);
            allZoneCell[i].gameObject.SetActive(false);
        }

        List<Vector3> allBuffPos = FindAllCellType(Cells, colliders, checkBuffCellType);
        for (int i = 0; i < allBuffPos.Count; i++)
        {
            ZoneCellData zoneCell = GetZone();
            zoneCell.SetBuffData();
            zoneCell.gameObject.transform.position = allBuffPos[i] + Vector3.up;

            TextCellData textCell = GetText();
            textCell.SetBuffData("+10%", allBuffPos[i] + Vector3.up);

        }

        List<Vector3> allDebuffPos = FindAllCellType(Cells, colliders, checkDebuffCellType);
        for (int i = 0; i < allDebuffPos.Count; i++)
        {
            ZoneCellData zoneCell = GetZone();
            zoneCell.SetDebuffData();
            zoneCell.gameObject.transform.position = allDebuffPos[i] + Vector3.up;

            TextCellData textCell = GetText();
            textCell.SetDebuffData("-10%", allDebuffPos[i] + Vector3.up);

        }
    }

    public ZoneCellData GetZone()
    {
        for (int i = 0; i < allZoneCell.Count; i++)
        {
            if (allZoneCell[i].gameObject.activeSelf == false)
            {
                allZoneCell[i].gameObject.SetActive(true);
                return allZoneCell[i];
            }
        }
        allZoneCell.Add(Instantiate(bonusZonePrefab, bonusZoneParent.transform).GetComponent<ZoneCellData>());
        return allZoneCell.Last();
    }
    public TextCellData GetText()
    {
        for (int i = 0; i < allTexts.Count; i++)
        {
            if (allTexts[i].gameObject.activeSelf == false)
            {
                allTexts[i].gameObject.SetActive(true);
                return allTexts[i];
            }
        }
        allTexts.Add(Instantiate(bonusTextPrefab, bonusTextParent.transform).GetComponent<TextCellData>());
        return allTexts.Last();
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
