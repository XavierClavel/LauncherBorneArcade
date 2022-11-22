using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceGenre : ChoiceMultiSelectable
{

    public ChoiceGenre(string collection, List<Item> games)
    {
        this.name = collection;
        this.games = games;

        string pathToImage = SC_LauncherModel.pathToGames + SC_LauncherModel.metaFolder + name + ".png";
        loadImage(pathToImage);

        choices.Add(this);
        // SearchManager.searchItems.Add(this);
    }

    public override void OnEnter()
    {
        // base.OnEnter();
        Debug.Log("enter");

        GridEvent.instance.DisplayExtendedSearchPanel();
        GridEvent.SwitchActiveGridNavigator(SC_LauncherControler.instance.genreNavigator);
    }

}
