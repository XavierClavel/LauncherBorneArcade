using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceMultiSelectable : Item
{
    public Image spriteStatus;
    public static Sprite status_yes;
    public static Sprite status_no;
    public bool selected = true;

    public static List<Item> choices = new List<Item>();
    public static List<Item> selectedGames = new List<Item>();
    public List<Item> games = new List<Item>();
    // public ChoiceMultiSelectable(string collection, List<Item> games)
    // {
    //     this.name = collection;
    //     this.games = games;

    //     string pathToImage = SC_LauncherModel.pathToGames + SC_LauncherModel.metaFolder + name + ".png";
    //     loadImage(pathToImage);

    //     choices.Add(this);
    //     SearchManager.searchItems.Add(this);
    // }

    public override void OnEnter()
    {
        selected = !selected;
        spriteStatus.sprite = selected ? status_yes : status_no;
        SearchManager.OnSearch();

        //select or unselect
    }

    public static List<Item> SearchGenre()
    {
        selectedGames = new List<Item>();
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
