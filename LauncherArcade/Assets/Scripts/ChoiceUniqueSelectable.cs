using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceUniqueSelectable : Item
{

    public static List<Item> choices = new List<Item>();
    public static List<Item> selectedItems = new List<Item>();
    public Image spriteStatus;
    public static Sprite status_yes;
    public static Sprite status_no;
    public bool validated = true;
    static bool lastValue = true;
    List<Item> games = new List<Item>();
    public ChoiceUniqueSelectable(string collection, List<Item> games)
    {
        this.games = games;
        this.name = collection;

        string pathToImage = SC_LauncherModel.pathToGames + SC_LauncherModel.metaFolder + name + ".png";
        loadImage(pathToImage);

        // choices.Add(this);
        SC_LauncherControler.searchList.Add(this);
        SearchManager.searchItems.Add(this);
    }

    public override void OnEnter()
    {

        validated = !validated;
        if (!lastValue && !validated)
        {
            selectedItems = SC_LauncherControler.games;
            lastValue = true;
        }
        else
        {
            selectedItems = this.games;
        }
        lastValue = validated;
        spriteStatus.sprite = validated ? status_yes : status_no;

        SearchManager.OnSearch();
        //select grid item
        //Deselect previously selected item
    }

    public static List<Item> SearchNbJoueurs()
    {
        return selectedItems;
    }
}
