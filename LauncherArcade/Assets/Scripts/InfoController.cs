using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

public class InfoController : MonoBehaviour
{
    [Header("Info Panels")]
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] Image imageDisplay;
    [SerializeField] TextMeshProUGUI nbJoueursDisplay;
    [SerializeField] TextMeshProUGUI genreDisplay;
    [SerializeField] GameObject blue_controls;
    [SerializeField] GameObject red_controls;
    [SerializeField] RenderTexture videoTexture;

    [Header("Controls Player 1")]
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

    [Header("Controls Player 2")]
    [SerializeField] TextMeshProUGUI RJ;
    [SerializeField] TextMeshProUGUI R1;
    [SerializeField] TextMeshProUGUI R2;
    [SerializeField] TextMeshProUGUI R3;
    [SerializeField] TextMeshProUGUI R4;
    [SerializeField] TextMeshProUGUI R5;
    [SerializeField] TextMeshProUGUI R6;

    [SerializeField] GameObject RJ_GO;
    [SerializeField] GameObject R1_GO;
    [SerializeField] GameObject R2_GO;
    [SerializeField] GameObject R3_GO;
    [SerializeField] GameObject R4_GO;
    [SerializeField] GameObject R5_GO;
    [SerializeField] GameObject R6_GO;

    VideoPlayer videoPlayerCopy;

    private void Awake()
    {
        SC_LauncherControler.infoController = this;
        // Subscribe to loopPointReached event
    }

    public void SetupControlsInfo(Game game)
    {
        ControlsInfo controlsInfo = game.controlsInfo;

        if (game.videoUrl != null)
        {
            imageDisplay.gameObject.SetActive(false);
            videoPlayer.gameObject.SetActive(true);
            videoPlayer.targetTexture.Release();
            videoPlayer.url = game.videoUrl;

        }
        else
        {
            videoPlayer.gameObject.SetActive(false);
            imageDisplay.gameObject.SetActive(true);
            imageDisplay.sprite = game.image;
        }
        string nbDisplay;
        switch (controlsInfo.nb_joueurs)
        {
            case "1":
                nbDisplay = "1 joueur";
                break;

            case "2":
                nbDisplay = "2 joueurs";
                break;

            case "1-2":
                nbDisplay = "1-2 joueurs";
                break;

            default:
                nbDisplay = "? joueurs";
                break;
        }
        nbJoueursDisplay.text = nbDisplay;
        genreDisplay.text = controlsInfo.genre;

        if (controlsInfo.has_L_controls)
        {
            blue_controls.SetActive(true);

            LJ.text = controlsInfo.controls_LJ;
            LJ_GO.SetActive(controlsInfo.controls_LJ != "");
            L1.text = controlsInfo.controls_L1;
            L1_GO.SetActive(controlsInfo.controls_L1 != "");
            L2.text = controlsInfo.controls_L2;
            L2_GO.SetActive(controlsInfo.controls_L2 != "");
            L3.text = controlsInfo.controls_L3;
            L3_GO.SetActive(controlsInfo.controls_L3 != "");
            L4.text = controlsInfo.controls_L4;
            L4_GO.SetActive(controlsInfo.controls_L4 != "");
            L5.text = controlsInfo.controls_L5;
            L5_GO.SetActive(controlsInfo.controls_L5 != "");
            L6.text = controlsInfo.controls_L6;
            L6_GO.SetActive(controlsInfo.controls_L6 != "");
        }
        else blue_controls.SetActive(false);

        if (controlsInfo.has_R_controls)
        {
            red_controls.SetActive(true);

            RJ.text = controlsInfo.controls_RJ;
            RJ_GO.SetActive(controlsInfo.controls_RJ != "");
            R1.text = controlsInfo.controls_R1;
            R1_GO.SetActive(controlsInfo.controls_R1 != "");
            R2.text = controlsInfo.controls_R2;
            R2_GO.SetActive(controlsInfo.controls_R2 != "");
            R3.text = controlsInfo.controls_R3;
            R3_GO.SetActive(controlsInfo.controls_R3 != "");
            R4.text = controlsInfo.controls_R4;
            R4_GO.SetActive(controlsInfo.controls_R4 != "");
            R5.text = controlsInfo.controls_R5;
            R5_GO.SetActive(controlsInfo.controls_R5 != "");
            R6.text = controlsInfo.controls_R6;
            R6_GO.SetActive(controlsInfo.controls_R6 != "");
        }
        else red_controls.SetActive(false);

    }


    public void ResetVideoPlayer()
    {
        //Application.Unload();
    }
}
