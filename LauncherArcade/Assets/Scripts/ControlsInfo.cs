
public class ControlsInfo
{
    public string nb_joueurs;
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
        controls_LJ = text.GetStrBetweenTag("<LJ>");
        controls_L1 = text.GetStrBetweenTag("<L1>");
        controls_L2 = text.GetStrBetweenTag("<L2>");
        controls_L3 = text.GetStrBetweenTag("<L3>");
        controls_L4 = text.GetStrBetweenTag("<L4>");
        controls_L5 = text.GetStrBetweenTag("<L5>");
        controls_L6 = text.GetStrBetweenTag("<L6>");
    }
}
