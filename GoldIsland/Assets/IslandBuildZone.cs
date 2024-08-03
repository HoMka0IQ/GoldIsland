using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;

public class IslandBuildZone : MonoBehaviour
{
    public GameObject islandPrefab;
    private void OnMouseUp()
    {
        if (!EventSystem.current.IsPointerOverGameObject(0))
        {
            GameObject island = Instantiate(islandPrefab, transform.position, Quaternion.identity);
            island.transform.SetParent(IslandBuilding.Instance.gameObject.transform);
            IslandBuilding.Instance.AddIsland(island.GetComponent<IslandData>());

            CameraMovement.instance.SetBorders();
        }
    }
}
