using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildZonesManager : MonoBehaviour
{
    [SerializeField] GameObject[] zones;
    [SerializeField] MeshRenderer[] zonesMR;

    [SerializeField] TextCellData[] zonesText;

    [SerializeField] Color textBuffColor;
    [SerializeField] Color textDebuffColor;

    [SerializeField] Material buffMat;
    [SerializeField] Material debuffMat;

    bool openTimer;

    public static BuildZonesManager instance;
    private void Awake()
    {
        instance = this;

    }
    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended && TouchChecker.instance.GetTrueTouch() && openTimer == false && TouchChecker.instance.IsPointerOverUIObject(Input.GetTouch(0)) == false)
        {
            HideAllZones();
        }
    }
    public void ShowZones(List<Vector3> buffZonesPos, List<Vector3> debuffZonesPos, GameObject go)
    {
        openTimer = true;
        HideAllZones();
        setZones(buffZonesPos, buffMat, textBuffColor, "+10%");
        setZones(debuffZonesPos, debuffMat, textDebuffColor, "-10%");
        Invoke("offOpenTimer", 0.01f);
    }
    void offOpenTimer()
    {
        openTimer = false;
    }

    void setZones(List<Vector3> Pos, Material zoneTypeMat, Color textColor, string text)
    {
        for (int i = 0; i < Pos.Count; i++)
        {
            for (int j = 0; j < zones.Length; j++)
            {
                if (zones[j].gameObject.activeSelf == false)
                {
                    zonesMR[j].material = zoneTypeMat;

                    zonesText[j].gameObject.SetActive(true);
                    zonesText[j].SetData(textColor, text);
                    zones[j].gameObject.SetActive(true);
                    zones[j].transform.position = Pos[i];
                    break;
                }
            }
        }
    }
    public void HideAllZones()
    {
        for (int i = 0; i < zones.Length; i++)
        {
            zones[i].gameObject.SetActive(false);
            zonesText[i].gameObject.SetActive(false);
        }

    }
}
