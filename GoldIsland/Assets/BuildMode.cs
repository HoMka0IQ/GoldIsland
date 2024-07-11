using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildMode : MonoBehaviour
{
    [SerializeField] GameObject prebuildParent;
    public void OnBuildMode()
    {
        List<IslandData> islands = IslandBuilding.Instance.GetIslands();
        for (int i = 0; i < islands.Count; i++)
        {
            for (int j = 0; j < islands[i].GetCells().Length; j++)
            {
                if (islands[i].GetCells()[j].cellTypes == Cell_SO.CellType.Empty)
                {
                    islands[i].GetGOCells()[j].transform.GetChild(0).gameObject.SetActive(true);
                }
            }
        }
        prebuildParent.SetActive(true);
        IslandBuilding.Instance.buildZoneParent.SetActive(true);
    }
    public void OffBuildMode()
    {
        List<IslandData> islands = IslandBuilding.Instance.GetIslands();
        for (int i = 0; i < islands.Count; i++)
        {
            for (int j = 0; j < islands[i].GetCells().Length; j++)
            {
                if (islands[i].GetCells()[j].cellTypes == Cell_SO.CellType.Empty)
                {
                    islands[i].GetGOCells()[j].transform.GetChild(0).gameObject.SetActive(false);
                }
            }
        }
        prebuildParent.SetActive(false);
        for (int i = 0;i < prebuildParent.transform.childCount;i++)
        {
            prebuildParent.transform.GetChild(i).gameObject.SetActive(false);
        }
        IslandBuilding.Instance.buildZoneParent.SetActive(false);
    }
}
