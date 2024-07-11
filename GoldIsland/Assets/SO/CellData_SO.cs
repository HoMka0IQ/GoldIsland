using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CellData_SO", menuName = "Cells/New Cells Data")]
public class CellData_SO : ScriptableObject
{
    public Cell_SO[] allCells;

    public Cell_SO emptyCell;
}
