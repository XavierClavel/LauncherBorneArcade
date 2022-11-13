using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetaCollection : Item
{
    public List<Item> items = new List<Item>();
    public static List<Item> metaCollections = new List<Item>();

    public MetaCollection(string name, List<Item> items)
    {
        this.name = name;
        this.items = items;

        metaCollections.Add(this);
        string pathToImage = SC_LauncherModel.pathToGames + SC_LauncherModel.metaFolder + name + ".png";
        loadImage(pathToImage);
    }

    public override void OnEnter()
    {
        GridNavigator.instance.Initialize(items);
    }
}
