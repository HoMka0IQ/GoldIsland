using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class IslandBuildZone : MonoBehaviour
{
    public GameObject islandPrefab;
    private void OnMouseDown()
    {
        if (TouchChecker.instance.GetTouchCount() == 1)
        {
            GameObject island = Instantiate(islandPrefab, transform.position, Quaternion.identity);
            island.transform.SetParent(IslandBuilding.Instance.gameObject.transform);
            IslandBuilding.Instance.AddIsland(island.GetComponent<IslandData>());

            CameraMovement.instance.SetBorders();
        }
    }
}
