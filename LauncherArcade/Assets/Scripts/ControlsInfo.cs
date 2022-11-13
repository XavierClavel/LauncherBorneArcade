using System.Collections.Generic;

public class ControlsInfo
{
    public string nb_joueurs;
    public string genre = string.Empty;
    public List<string> collections;
    public bool has_L_controls;
    public bool has_R_controls;

    public string controls_LJ;
    public string controls_L1;
    public string controls_L2;
    public string controls_L3;
    public string controls_L4;
    public string controls_L5;
    public string controls_L6;

    public string controls_RJ;
    public string controls_R1;
    public string controls_R2;
    public string controls_R3;
    public string controls_R4;
    public string controls_R5;
    public string controls_R6;

    public ControlsInfo(string text)
    {
        nb_joueurs = text.GetStrBetweenTag("<nbJoueurs>");
        genre = text.GetStrBetweenTag("<genre>");
        collections = text.GetAllStrBetweenTag("<collection>");

        has_L_controls = text.Contains("<L");
        has_R_controls = text.Contains("<R");

        if (has_L_controls)
        {
            controls_LJ = text.GetStrBetweenTag("<LJ>");
            controls_L1 = text.GetStrBetweenTag("<L1>");
            controls_L2 = text.GetStrBetweenTag("<L2>");
            controls_L3 = text.GetStrBetweenTag("<L3>");
            controls_L4 = text.GetStrBetweenTag("<L4>");
            controls_L5 = text.GetStrBetweenTag("<L5>");
            controls_L6 = text.GetStrBetweenTag("<L6>");
        }

        if (has_R_controls)
        {
            controls_RJ = text.GetStrBetweenTag("<RJ>");
            controls_R1 = text.GetStrBetweenTag("<R1>");
            controls_R2 = text.GetStrBetweenTag("<R2>");
            controls_R3 = text.GetStrBetweenTag("<R3>");
            controls_R4 = text.GetStrBetweenTag("<R4>");
            controls_R5 = text.GetStrBetweenTag("<R5>");
            controls_R6 = text.GetStrBetweenTag("<R6>");
        }

    }
}
