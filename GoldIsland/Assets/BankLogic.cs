using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BankLogic : MonoBehaviour,IDataRefreshable
{
    [SerializeField] IslandChecker islandChecker;
    [SerializeField] IslandData currentIsland;
    [SerializeField] List<Factory> factoryOnIsland;
    private void Start()
    {
        currentIsland = islandChecker.GetIsland().GetComponent<IslandData>();
        CalcFactoryOnIland();
        StartCoroutine(CollectMoney());
    }
    public void CalcFactoryOnIland()
    {
        if (factoryOnIsland.Count > 0)
        {
            factoryOnIsland.Clear();
        }

        Cell_SO[] cellOnIsland = currentIsland.GetCells();
        GameObject[] allCells = currentIsland.GetGOCells();
        for (int i = 0; i < cellOnIsland.Length; i++)
        {
            if (cellOnIsland[i].cellTypes == Cell_SO.CellType.Building)
            {
                BuildCell_So buildCell = cellOnIsland[i] as BuildCell_So;
                if (buildCell.buildType == BuildCell_So.BuildType.Factory)
                {
                    factoryOnIsland.Add(allCells[i].GetComponent<Factory>());
                }
            }
        }
    }
    IEnumerator CollectMoney()
    {
        while (true)
        {
            int collectMoney = 0;
            for (int i = 0; i < factoryOnIsland.Count; i++)
            {
                collectMoney += factoryOnIsland[i].GetMoneyInside();
                factoryOnIsland[i].CearMoneyInside();
            }
            GameData.instance.IncreaseMoney(collectMoney);
            yield return new WaitForSeconds(5f);
        }
    }
    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && islandChecker.zoneIsOn)
        {
            islandChecker.HideZone();
        }
    }
    private void OnMouseUp()
    {
        if (CameraMovement.instance.drag == false && !EventSystem.current.IsPointerOverGameObject(0))
        {
            islandChecker.ShowZone();
        }
    }

    public void RefreshData()
    {
        Debug.Log("Bank refreshing", gameObject);
        CalcFactoryOnIland();
    }
}
