using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceMultiSelectable : Item
{
    public static List<Item> choices = new List<Item>();
    public static List<Item> selectedGames = new List<Item>();
    List<Item> games = new List<Item>();
    bool selected = false;
    public ChoiceMultiSelectable(string collection, List<Item> games)
    {
        this.name = collection;
        this.games = games;

        string pathToImage = SC_LauncherModel.pathToGames + SC_LauncherModel.metaFolder + name + ".png";
        loadImage(pathToImage);

        choices.Add(this);
    }

    public override void OnEnter()
    {
        selected = !selected;

        //select or unselect
    }

    public static List<Item> SearchGenre()
    {
        foreach (ChoiceMultiSelectable choice in choices)
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
}
