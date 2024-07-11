using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public Cell_SO cell_SO;
    public IslandData islandData;
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
}
