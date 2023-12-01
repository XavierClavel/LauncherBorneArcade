using UnityEngine;
using UnityEngine.Video;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class Game : Item
{
    public string pathToGameDir { get; set; }
    public string pathToExe { get; set; }
    public string pathToGameMeta { get; set; }
    public string description = null;
    public string videoUrl;
    public ControlsInfo controlsInfo;
    public static List<Item> onePlayer_games = new List<Item>();
    public static List<Item> twoPlayer_games = new List<Item>();

    ///<summary>
    ///Constructor for a "Game" object that loads all information regarding the game.
    ///</summary>
    public Game(string pathToGameDir, string pathToExe, string pathToGameMeta, string name)
    {
        this.pathToGameDir = pathToGameDir;
        this.pathToExe = pathToExe;
        this.pathToGameMeta = pathToGameMeta;
        this.name = name;

        loadImage(pathToGameMeta + "/logo.png");
        loadDescription();
        loadControls();
        loadVideo();

        Genre.AttributeGame(this);
        Collection.AttributeGame(this);
    }

    ///<summary>
    ///Reads the description.txt file in the GameMeta folder and sets text content to the variable "description".
    ///</summary>
    void loadDescription()
    {
        string pathToDescription = pathToGameMeta + "/description.txt";

        description = loadText(pathToDescription, "Un jeu de 7Fault");
    }

    ///<summary>Reads the controls.txt file in the GameMeta folder and stores its content in a new ControlsInfo object. 
    ///Also places the game in a static list corresponding to its number of players.</summary>
    void loadControls()
    {
        string pathToDescription = pathToGameMeta + "/controls.txt";

        controlsInfo = new ControlsInfo(loadText(pathToDescription, ""));

        switch (controlsInfo.nb_joueurs)
        {
            case "1":
                onePlayer_games.Add(this);
                break;

            case "2":
                twoPlayer_games.Add(this);
                break;

            case "1-2":
                onePlayer_games.Add(this);
                twoPlayer_games.Add(this);
                break;
        }
    }

    ///<summary>Sets the "videoUrl" variable to the path of the demo.mp4 file if it exists, or null if it doesn't.</summary>
    void loadVideo()
    {
        videoUrl = pathToGameMeta + "/demo.mp4";
        if (!File.Exists(videoUrl)) videoUrl = null;
    }

    public override void OnEnter()
    {
        SC_LauncherControler.LaunchGame(this);
    }

    public override void OnInfo()
    {
        SC_LauncherControler.DisplayInfo(this);
    }

    public static void OnGamesLoaded()
    {
        new ChoicePlayers("1 Player", onePlayer_games);
        new ChoicePlayers("2 Players", twoPlayer_games);

        // new MetaCollection("1 Player", onePlayer_games);
        // new MetaCollection("2 Players", onePlayer_games);
        // new ChoiceUniqueSelectable("1|2 Players", SC_LauncherControler.games);
    }


}