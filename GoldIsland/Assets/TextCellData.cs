using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TextCellData : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    [SerializeField] Color textBuffColor;
    [SerializeField] Color textDebuffColor;

    [SerializeField] ConnectionScreenToWorld connectionScreenToWorld;
    public void SetBuffData(string text, GameObject target)
    {
        this.text.text = text;
        this.text.color = textBuffColor;
        connectionScreenToWorld.target = target;
    }
    public void SetDebuffData(string text, GameObject target)
    {
        this.text.text = text;
        this.text.color = textDebuffColor;
        connectionScreenToWorld.target = target;
    }
}
