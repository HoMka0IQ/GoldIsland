using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    [SerializeField] int defaultCollectCoin;
    [SerializeField] int сollectCoinFromCell;
    [SerializeField] CalcNeighborCells neighborCells;
    Animation moveAnim;
    void Start()
    {
        StartCoroutine(colectCoins());
        neighborCells.CalcNeighborCell();
        moveAnim = GetComponent<Animation>();   
        Collider[] colls = neighborCells.GetColliders();
        for (int i = 0; i < colls.Length; i++)
        {
            CalcNeighborCells calcNeighborCells;
            if (colls[i] != null)
            {
                if (colls[i].gameObject.TryGetComponent<CalcNeighborCells>(out calcNeighborCells))
                {
                    calcNeighborCells.CalcNeighborCell();
                }
            }
        }
    }
    
    IEnumerator colectCoins()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            GameData.instance.IncreaseMoney(defaultCollectCoin + (сollectCoinFromCell * neighborCells.buffCellCount) - (сollectCoinFromCell * neighborCells.debuffCellCount));
        }
    }

    private void OnMouseUp()
    {
        if(CameraMovement.instance.drag == false)
        {
            neighborCells.ShowZones();
            moveAnim.Play();
        }
    }
    private void OnMouseDrag()
    {
        
    }
    
}
