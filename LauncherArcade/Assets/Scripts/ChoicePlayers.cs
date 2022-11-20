using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoicePlayers : ChoiceMultiSelectable
{
    public static List<Item> choices = new List<Item>();
    public static List<Item> selectedGames = new List<Item>();
    public ChoicePlayers(string collection, List<Item> games)
    {
        this.name = collection;
        this.games = games;

        string pathToImage = SC_LauncherModel.pathToGames + SC_LauncherModel.metaFolder + name + ".png";
        loadImage(pathToImage);

        choices.Add(this);
        SearchManager.searchItems.Add(this);
        Debug.Log(games.Count);
    }

    public static List<Item> SearchGenre()
    {
        selectedGames = new List<Item>();
        foreach (ChoicePlayers choice in choices)
        {
            choice.OnSearch();
        }
        return selectedGames;
    }

    public void OnSearch()
    {
        Debug.Log(selected);
        Debug.Log(games.Count);
        if (!selected) return;

        foreach (Item game in games)
        {
            if (!selectedGames.Contains(game))  //check to support games in multiple genres
            {
                selectedGames.Add(game);
            }
        }
    }


}
