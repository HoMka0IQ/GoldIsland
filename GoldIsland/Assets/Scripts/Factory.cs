using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    [SerializeField] int defaultCollectCoin;
    [SerializeField] int сollectCoinFromCell;
    [SerializeField] CalcNeighborCells neighborCells;
    void Start()
    {
        StartCoroutine(colectCoins());
        neighborCells.CalcNeighborCell();
    }
    
    IEnumerator colectCoins()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            GameData.instance.IncreaseMoney(defaultCollectCoin + (сollectCoinFromCell * neighborCells.buffCellCount) - (сollectCoinFromCell * neighborCells.debuffCellCount));
        }
    }
}
