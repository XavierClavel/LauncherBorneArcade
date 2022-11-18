using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceUniqueSelectable : Item
{

    public static List<Item> choices = new List<Item>();
    public static List<Item> selectedItems = new List<Item>();
    List<Item> games = new List<Item>();
    public ChoiceUniqueSelectable(string collection, List<Item> games)
    {
        this.games = games;
        this.name = collection;

        string pathToImage = SC_LauncherModel.pathToGames + SC_LauncherModel.metaFolder + name + ".png";
        loadImage(pathToImage);

        // choices.Add(this);
        SC_LauncherControler.searchList.Add(this);
    }

    public override void OnEnter()
    {
        selectedItems = this.games;
        //select grid item
        //Deselect previously selected item
    }

    public static List<Item> SearchNbJoueurs()
    {
        return selectedItems;
    }
}
