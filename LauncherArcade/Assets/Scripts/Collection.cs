using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collection : Item
{
    public List<Item> games = new List<Item>();
    public static List<Item> collections = new List<Item>();
    public static List<string> collectionNames = new List<string>();
    public static Dictionary<string, List<Game>> dictionary = new Dictionary<string, List<Game>>(); //for testing

    public Collection(Game game, string collection)
    {
        this.name = collection;
        this.games.Add(game);
        collections.Add(this);

        string pathToImage = SC_LauncherModel.pathToGames + SC_LauncherModel.metaFolder + name + ".png";
        loadImage(pathToImage);
    }


    public static void AttributeGame(Game game)
    {
        List<string> collections = game.controlsInfo.collections;

        foreach (string collection in collections)
        {
            AttributeCollection(game, collection);
        }
    }

    public static void AttributeCollection(Game game, string collection)
    {
        if (collection == string.Empty) return;

        if (Exists(collection))
        {
            int index = collectionNames.IndexOf(collection);
            Collection releventCollection = (Collection)collections[index];
            releventCollection.games.Add(game);
        }
        else
        {
            collectionNames.Add(collection);
            new Collection(game, collection);
        }
    }


    public static bool Exists(string genre)
    {
        return collectionNames.Contains(genre);
    }

    public static void InitializeCollections()
    {
        // foreach (Collection collection in collections)
        // {
        //     dictionary[collection.name] = (List<Game>) collection.games;
        // }
    }

    public static List<Game> GetGames(string collection)
    {
        if (!Exists(collection)) return null;
        return dictionary[collection];
    }

    public override void OnEnter()
    {
        SC_LauncherControler.instance.gridNavigator.Initialize(games);
    }

    public static void OnCollectionsLoaded()
    {
        new MetaCollection("Collections", collections);
    }

}
