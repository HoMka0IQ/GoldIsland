using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData : MonoBehaviour
{
    public CellData_SO allCells;
    public GameObject UICanvas;

    public static GlobalData instance;
    private void Awake()
    {
        instance = this;
    }

  
}
