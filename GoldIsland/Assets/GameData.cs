using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameData : MonoBehaviour
{
    [SerializeField] long money;
    [SerializeField] TMP_Text moneyText;

    public static GameData instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        ReloadText();
    }
    public void IncreaseMoney(int count)
    {
        money += count;
        ReloadText();
    }
    public void DecreaseMoney(int count)
    {
        money -= count;
        if (money < 0)
        {
            money = 0;
        };
        ReloadText();
    }
    public void ReloadText()
    {
        moneyText.text = FormatMoney(money);
    }
    private string FormatMoney(long amount)
    {
        if (amount >= 1_000_000_000_000_000)
        {
            return (amount / 1_000_000_000_000_000.0).ToString("0.#") + "Q";
        }
        else if (amount >= 1_000_000_000_000)
        {
            return (amount / 1_000_000_000_000.0).ToString("0.#") + "T";
        }
        else if (amount >= 1_000_000_000)
        {
            return (amount / 1_000_000_000.0).ToString("0.#") + "B";
        }
        else if (amount >= 1_000_000)
        {
            return (amount / 1_000_000.0).ToString("0.#") + "M";
        }
        else if (amount >= 1_000)
        {
            return (amount / 1_000.0).ToString("0.#") + "k";
        }
        else
        {
            return amount.ToString();
        }
    }
    public void add1000()
    {
        IncreaseMoney(1000);
    }
}
