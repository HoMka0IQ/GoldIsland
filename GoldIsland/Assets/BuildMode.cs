using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuildMode : MonoBehaviour
{
    [SerializeField] GameObject prebuildParent;

    List<BuildUI> allBuildUI = new List<BuildUI>();

    public static BuildMode instance;

    private void Awake()
    {
        instance = this;
    }

    public void addPreBuild(BuildUI buildUI)
    {
        allBuildUI.Add(buildUI);
    }
    public void CloseAllPreBuild()
    {
        for (int i = 0; i < allBuildUI.Count; i++)
        {
            allBuildUI[i].prebuildGO.SetActive(false);
            allBuildUI[i].ResetUI();
        }
    }
    public void ShowBuildZones(string layerName)
    {
        List<IslandData> islands = IslandBuilding.Instance.GetIslands();
        for (int i = 0; i < islands.Count; i++)
        {
            for (int j = 0; j < islands[i].GetCells().Length; j++)
            {
                if (islands[i].GetGOCells()[j].layer == LayerMask.NameToLayer(layerName))
                {
                    islands[i].GetGOCells()[j].transform.GetChild(0).gameObject.SetActive(true);
                }
            }
        }

    }
    public void HideBuildZones(string layerName)
    {
        List<IslandData> islands = IslandBuilding.Instance.GetIslands();
        for (int i = 0; i < islands.Count; i++)
        {
            for (int j = 0; j < islands[i].GetCells().Length; j++)
            {
                if (islands[i].GetGOCells()[j].layer == LayerMask.NameToLayer(layerName))
                {
                    islands[i].GetGOCells()[j].transform.GetChild(0).gameObject.SetActive(false);
                }
            }
        }

    }
    public void OnBuildMode()
    {
        prebuildParent.SetActive(true);
        IslandBuilding.Instance.buildZoneParent.SetActive(true);
    }
    public void OffBuildMode()
    {

        prebuildParent.SetActive(false);
        for (int i = 0;i < prebuildParent.transform.childCount;i++)
        {
            prebuildParent.transform.GetChild(i).gameObject.SetActive(false);
        }
        IslandBuilding.Instance.buildZoneParent.SetActive(false);
    }
}
