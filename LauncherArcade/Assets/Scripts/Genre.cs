using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Genre : Item
{
    public List<Item> games = new List<Item>();
    public static List<Item> collections = new List<Item>();
    public static List<string> genres = new List<string>();

    public Genre(Game game)
    {
        this.name = game.controlsInfo.genre;
        this.games.Add(game);

        string pathToImage = SC_LauncherModel.pathToGames + SC_LauncherModel.metaFolder + name + ".png";
        loadImage(pathToImage);

    }


    public static void AttributeGame(Game game)
    {
        string genre = game.controlsInfo.genre;
        if (genre == string.Empty) return;

        if (Exists(genre))
        {
            int index = genres.IndexOf(genre);
            Genre relevantGenre = (Genre)collections[index];
            relevantGenre.games.Add(game);
        }
        else
        {
            genres.Add(genre);
            collections.Add(new Genre(game));
            collections.Add(new Genre(game)); collections.Add(new Genre(game)); collections.Add(new Genre(game)); collections.Add(new Genre(game)); collections.Add(new Genre(game)); collections.Add(new Genre(game)); collections.Add(new Genre(game));
        }
    }
    public static bool Exists(string genre)
    {
        return genres.Contains(genre);
    }

    public override void OnEnter()
    {
        SC_LauncherControler.gridNavigator.Initialize(games);
    }

    public static void OnGenresLoaded()
    {
        new MetaCollection("Genres", collections);
        new ChoiceGenre("Genres", collections);
        foreach (Genre genre in collections)
        {
            // new ChoiceMultiSelectable(genre.name, genre.games);
        }
    }

}
