using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class SC_LauncherModel
{
    public static string pathToGames;
    public static string metaFolder = "/Meta/";

    public SC_LauncherModel(string pathToGamesValue)
    {
        pathToGames = pathToGamesValue;
    }

    // Charge le contenue de "news.txt" du dossier Games/ dans la zone de news
    public List<string> GetNews()
    {
        var pathNewsTxt = pathToGames + metaFolder + "news.txt";
        var strBuilder = new StringBuilder();


        if (File.Exists(pathNewsTxt))
        {
            StreamReader reader = new StreamReader(pathNewsTxt);

            while (!reader.EndOfStream)
            {
                strBuilder.Append(reader.ReadLine() + "\n");
            }

            reader.Close();
        }
        else
        {
            strBuilder.Append("Pas de news");
        }

        return strBuilder.ToString().GetAllStrBetweenTag("<news>");
    }


    // Get all games in Games/ directory and fill games with all the info
    public List<Item> GetGamesList()
    {
        var dirInfo = new DirectoryInfo(pathToGames);
        var directoryInfo = dirInfo.GetDirectories();

        List<Item> games = new List<Item>();

        foreach (var directory in directoryInfo)
        {
            string pathToGameDir = pathToGames + directory.Name;

            var files = System.IO.Directory.GetFiles(pathToGameDir, "*.exe");

            if (files.Length == 0) continue;

            // TODO check si c'est le bon .exe
            string pathToExe = files[0];

            string name = files[0].Split('\\')[1].Split('.')[0];

            var pathToGameMeta = pathToGameDir + "/GameMeta";
            // Create the directory only if it don't exist
            System.IO.Directory.CreateDirectory(pathToGameMeta);

            games.Add(new Game(pathToGameDir, pathToExe, pathToGameMeta, name));
        }
        Resources.UnloadUnusedAssets(); //to avoid keeping all pictures loaded in memory

        return games;
    }

    public void OnGamesLoaded()
    {
        Game.OnGamesLoaded();
        Genre.OnGenresLoaded();
        Collection.OnCollectionsLoaded();
        ChoiceMultiSelectable.selectedGames = SC_LauncherControler.games;
        ChoiceUniqueSelectable.selectedItems = SC_LauncherControler.games;
    }
}
