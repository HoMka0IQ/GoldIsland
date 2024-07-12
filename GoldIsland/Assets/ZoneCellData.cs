using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneCellData : MonoBehaviour
{
    [SerializeField] Material buffMat;
    [SerializeField] Material debuffMat;

    [SerializeField] MeshRenderer meshRenderer;
    public void SetBuffData()
    {
        meshRenderer.material = buffMat;
    }
    public void SetDebuffData()
    {
        meshRenderer.material = debuffMat;
    }
}
