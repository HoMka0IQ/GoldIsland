using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class IslandData : MonoBehaviour
{
    [SerializeField] Transform[] allPos = new Transform[16];
    [SerializeField] Cell_SO[] cellOnIsland = new Cell_SO[16];
    [SerializeField] GameObject[] allCells = new GameObject[16];
    [Range(0,100)]
    [SerializeField] float emptyCellChance;
    public int islandPosInArray { get; private set; }
    void Start()
    {
        GenerateCellsOnIsland();
    }
    public void SetPosInArray(int id)
    {
        islandPosInArray = id;
    }
    public Cell_SO[] GetCells()
    {
        return cellOnIsland; 
    }
    public GameObject[] GetGOCells()
    {
        return allCells;
    }
    public void RefreshBuildsDataExcept(int id)
    {
        for (int i = 0; i < cellOnIsland.Length; i++)
        {
            if (cellOnIsland[i].cellTypes == Cell_SO.CellType.Building && i != id)
            {
                allCells[i].GetComponent<IDataRefreshable>().RefreshData();
            }
        }
    }
    public void CellChanger(Cell_SO cell, int id)
    {
        cellOnIsland[id] = cell;
        Destroy(allCells[id]);
        allCells[id] = InstantiateCell(cellOnIsland[id].cellPrefab, allPos[id], Quaternion.Euler(0, 0, 0), id);
    }
    public void GenerateCellsOnIsland()
    {
        int numberOfEmptyCells = Mathf.FloorToInt(allPos.Length * (emptyCellChance / 100));

        // Випадково вибрати індекси для пустих кубів
        List<int> emptyIndices = Enumerable.Range(0, allPos.Length).OrderBy(x => Random.value).Take(numberOfEmptyCells).ToList();

        for (int i = 0; i < allPos.Length; i++)
        {
            Cell_SO randCell;
            if (emptyIndices.Contains(i))
            {
                randCell = GlobalData.instance.allCells.emptyCell;
                cellOnIsland[i] = randCell;
                allCells[i] = InstantiateCell(randCell.cellPrefab, allPos[i], Quaternion.Euler(0, 0, 0), i);
            }
            else
            {
                randCell = GlobalData.instance.allCells.allCells[Random.Range(0, GlobalData.instance.allCells.allCells.Length)];
                cellOnIsland[i] = randCell;
                allCells[i] = InstantiateCell(randCell.cellPrefab, allPos[i], Quaternion.Euler(0, Random.Range(0, 360), 0), i);
            }
            allCells[i].SetActive(false);
        }
        StartCoroutine(ShowingAnim());
    }
    IEnumerator ShowingAnim()
    {
        for(int i = 0;i < allCells.Length;i++)
        {
            yield return new WaitForSeconds(0.1f);
            allCells[i].SetActive(true);
        }
    }
    public GameObject InstantiateCell(GameObject cellPrefab, Transform spawnTransform, Quaternion rot, int id)
    {
        GameObject cell = Instantiate(cellPrefab, spawnTransform.position, Quaternion.identity);
        cell.transform.SetParent(spawnTransform, true);
        cell.transform.rotation = rot;
        Cell cellScript = cell.GetComponent<Cell>();
        cellScript.SetData(this, id);
        return cell;
    }
}
