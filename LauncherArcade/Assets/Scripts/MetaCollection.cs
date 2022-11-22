using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaCollection : ChoiceMultiSelectable
{
    public MetaCollection(string name, List<Item> items)
    {
        this.name = name;
        this.games = items;

        string pathToImage = SC_LauncherModel.pathToGames + SC_LauncherModel.metaFolder + name + ".png";
        loadImage(pathToImage);

        choices.Add(this);
        SearchManager.searchItems.Add(this);
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

    public override void OnEnter()
    {
        GridEvent.instance.DisplayExtendedSearchPanel();
        GridEvent.SwitchActiveGridNavigator(SC_LauncherControler.instance.genreNavigator);
    }
}
