using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class IslandBuilding : MonoBehaviour
{
    [SerializeField] List<IslandData> islands;
    [SerializeField] List<Vector3> emptyPos;
    [SerializeField] List<GameObject> buildZones;
    [SerializeField] Vector3[] checkWays;

    [SerializeField] GameObject buildZonePrefab;
    public GameObject buildZoneParent;

    public static IslandBuilding Instance;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        buildZoneParent.SetActive(false);
        CheckBuildZone();
    }
    public List<IslandData> GetIslands()
    {
        return islands;
    }
    public void AddIsland(IslandData island)
    {
        islands.Add(island);
        CheckBuildZone();
    }
    public void CheckBuildZone()
    {
        if (buildZones.Count > 0)
        {
            foreach (GameObject obj in buildZones)
            {
                if (obj != null)
                {
                    Destroy(obj);
                }
            }
            buildZones.Clear();
        }
        if (emptyPos.Count > 0)
        {
            emptyPos.Clear();
        }
        foreach (IslandData island in islands)
        {
            Debug.Log("here");
            CheckSide(island.transform, island.transform.right * 10);
            CheckSide(island.transform, -island.transform.right * 10);
            CheckSide(island.transform, island.transform.forward * 10);
            CheckSide(island.transform, -island.transform.forward * 10);
        }

        List<Vector3> uniquePositions = emptyPos.Distinct().ToList();
        emptyPos = uniquePositions;

        InstantiateBuildZone();
    }
    public void CheckSide(Transform startPoint, Vector3 ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(startPoint.position, ray, out hit))
        {
            if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Island") )
            {
                return;
            }
        }
        emptyPos.Add(startPoint.position + ray);
    }
    public void InstantiateBuildZone()
    {
        for (int i = 0; i < emptyPos.Count; i++)
        {
            GameObject buildZone = Instantiate(buildZonePrefab, emptyPos[i], Quaternion.identity);
            buildZone.transform.SetParent(buildZoneParent.transform);
            buildZones.Add(buildZone);
        }
    }
    private void OnDrawGizmos()
    {
        Debug.DrawRay(islands[0].transform.position, islands[0].transform.right * 10, Color.red, 1.0f);
        Debug.DrawRay(islands[0].transform.position, -islands[0].transform.right * 10, Color.red, 1.0f);
        Debug.DrawRay(islands[0].transform.position, islands[0].transform.forward * 10, Color.red, 1.0f);
        Debug.DrawRay(islands[0].transform.position, -islands[0].transform.forward * 10, Color.red, 1.0f);
    }
}
