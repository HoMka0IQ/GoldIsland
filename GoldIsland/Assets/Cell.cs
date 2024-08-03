using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour
{
    public Cell_SO cell_SO;
    [HideInInspector]
    public IslandData islandData;
    [HideInInspector]
    public int posInArray;

    public Animation showingAnim;

    private void OnEnable()
    {
        if (showingAnim == null)
        {
            TryGetComponent<Animation>(out showingAnim);
        }
        showingAnim.Play();
        
    }
    public void SetData(IslandData islandData, int posInArray)
    {
        this.islandData = islandData;
        this.posInArray = posInArray;   
    }

    public void PlayAnim()
    {
        if (showingAnim == null)
        {
            return;
        }
        showingAnim.Play();
    }
    private void OnMouseUp()
    {
        if (CameraMovement.instance.drag == false && !EventSystem.current.IsPointerOverGameObject(0) && cell_SO.cellTypes != Cell_SO.CellType.Empty)
        {
            CellMenu.instance.OpenCellMenu(this, gameObject);
        }

    }
}
