using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Factory : MonoBehaviour,IDataRefreshable
{
    [SerializeField] int defaultCollectCoin;
    [SerializeField] int сollectCoinFromCell;
    [SerializeField] CalcNeighborCells neighborCells;
    [SerializeField] int moneyInside;
    [SerializeField] GameObject collectMoneyUI;
    [SerializeField] TMP_Text collectMoneyText;
    Animation moveAnim;

    void Start()
    {
        StartCoroutine(colectCoins());
        neighborCells.CalcNeighborCell();
        moveAnim = GetComponent<Animation>();   
        collectMoneyUI.SetActive(false);
    }
    public int GetMoneyInside()
    {
        return moneyInside;
    }
    public void CearMoneyInside()
    {
        moneyInside = 0;
        collectMoneyUI.SetActive(false);
    }
    IEnumerator colectCoins()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            int calcCollectMoney = defaultCollectCoin + (сollectCoinFromCell * neighborCells.buffCellCount) - (сollectCoinFromCell * neighborCells.debuffCellCount);
            calcCollectMoney = Mathf.Clamp(calcCollectMoney, 0, int.MaxValue);
            moneyInside += calcCollectMoney;
            
            if (moneyInside > calcCollectMoney * 6)
            {
                collectMoneyUI.SetActive(true);
                collectMoneyText.text = moneyInside.ToString();
            }
        }
    }

    public void CollectMoney()
    {
        GameData.instance.IncreaseMoney(moneyInside);
        CearMoneyInside();
    }
    private void Update()
    {

/*
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && neighborCells.zonesIsOn)
        {
            neighborCells.HideZones();
        }*/
    }
    private void OnMouseUp()
    {
        Debug.Log("GO " + !EventSystem.current.IsPointerOverGameObject(0) + " " + (TouchChecker.instance.IsPointerOverUIObject(Input.GetTouch(0)) == false));
        if (CameraMovement.instance.drag == false && !EventSystem.current.IsPointerOverGameObject(0))
        {
            
            neighborCells.ShowZones();
            moveAnim.Play();
        }
    }

    public void RefreshData()
    {
        neighborCells.CalcNeighborCell();
    }
}
