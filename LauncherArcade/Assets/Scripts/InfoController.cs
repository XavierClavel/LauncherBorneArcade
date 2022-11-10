using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI LJ;
    [SerializeField] TextMeshProUGUI L1;
    [SerializeField] TextMeshProUGUI L2;
    [SerializeField] TextMeshProUGUI L3;
    [SerializeField] TextMeshProUGUI L4;
    [SerializeField] TextMeshProUGUI L5;
    [SerializeField] TextMeshProUGUI L6;

    [SerializeField] GameObject LJ_GO;
    [SerializeField] GameObject L1_GO;
    [SerializeField] GameObject L2_GO;
    [SerializeField] GameObject L3_GO;
    [SerializeField] GameObject L4_GO;
    [SerializeField] GameObject L5_GO;
    [SerializeField] GameObject L6_GO;
    static InfoController instance;

    private void Start()
    {
        instance = this;
    }

    public static void SetupControlsInfo(ControlsInfo controlsInfo)
    {

        instance.LJ.text = controlsInfo.controls_LJ;
        instance.LJ_GO.SetActive(controlsInfo.controls_LJ != "");
        instance.L1.text = controlsInfo.controls_L1;
        instance.L1_GO.SetActive(controlsInfo.controls_L1 != "");
        instance.L2.text = controlsInfo.controls_L2;
        instance.L2_GO.SetActive(controlsInfo.controls_L2 != "");
        instance.L3.text = controlsInfo.controls_L3;
        instance.L3_GO.SetActive(controlsInfo.controls_L3 != "");
        instance.L4.text = controlsInfo.controls_L4;
        instance.L4_GO.SetActive(controlsInfo.controls_L4 != "");
        instance.L5.text = controlsInfo.controls_L5;
        instance.L5_GO.SetActive(controlsInfo.controls_L5 != "");
        instance.L6.text = controlsInfo.controls_L6;
        instance.L6_GO.SetActive(controlsInfo.controls_L6 != "");

    }
}
