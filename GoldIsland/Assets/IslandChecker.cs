using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandChecker : MonoBehaviour, IBuildInteractonZone
{
    [SerializeField] LayerMask islandLayer;
    [SerializeField] GameObject zone;
    [HideInInspector]
    public bool zoneIsOn;

    public void ShowZone()
    {
        GameObject island = GetIsland();
        zone.SetActive(true);
        Vector3 pos = island.transform.position;
        pos.y += 8.5f;
        zone.transform.position = pos;
        zoneIsOn = true;
    }
    public void HideZone()
    {
        zone.SetActive(false);
        zoneIsOn = false;
    }
    public GameObject GetIsland()
    {
        GameObject island;
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 5, Vector3.down * 7, out hit, 100, islandLayer))
        {
            island = hit.collider.gameObject;
            return island;
        }
        return null;
    }
    private void OnDrawGizmos()
    {
        Debug.DrawLine(transform.position + Vector3.up * 5, transform.position + Vector3.down * 7, Color.red);
    }
}
