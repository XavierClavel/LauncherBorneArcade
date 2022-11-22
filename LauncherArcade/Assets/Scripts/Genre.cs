using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Genre : ChoiceMultiSelectable
{
    public List<Item> games = new List<Item>();
    public static List<Item> choices = new List<Item>();
    public static List<string> genres = new List<string>();

    public Genre(Game game)
    {
        Debug.Log("new genre");
        this.name = game.controlsInfo.genre;
        this.games.Add(game);

        string pathToImage = SC_LauncherModel.pathToGames + SC_LauncherModel.metaFolder + name + ".png";
        loadImage(pathToImage);

        choices.Add(this);

    }


    public static void AttributeGame(Game game)
    {
        string genre = game.controlsInfo.genre;
        if (genre == string.Empty) return;

        if (Exists(genre))
        {
            int index = genres.IndexOf(genre);
            Genre relevantGenre = (Genre)choices[index];
            relevantGenre.games.Add(game);
        }
        else
        {
            genres.Add(genre);
            new Genre(game);
            // choices.Add(new Genre(game));
            // choices.Add(new Genre(game)); choices.Add(new Genre(game)); choices.Add(new Genre(game)); choices.Add(new Genre(game)); choices.Add(new Genre(game)); choices.Add(new Genre(game)); choices.Add(new Genre(game));
        }
    }
    public static bool Exists(string genre)
    {
        return genres.Contains(genre);
    }

    public static List<Item> SearchGenre()
    {
        selectedGames = new List<Item>();
        foreach (Genre choice in choices)
        {
            choice.OnSearch();
        }
        return selectedGames;
    }

    public void OnSearch()
    {
        if (!selected) return;

        foreach (Item game in games)
        {
            if (!selectedGames.Contains(game))  //check to support games in multiple genres
            {
                selectedGames.Add(game);
            }
        }
    }


    public static void OnGenresLoaded()
    {
        new MetaCollection("Genres", choices);
        // new ChoiceGenre("Genres", collections);
    }

}
