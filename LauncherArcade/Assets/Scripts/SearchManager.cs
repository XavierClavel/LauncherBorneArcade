using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SearchManager : MonoBehaviour
{
    static SearchManager instance;
    [SerializeField] Sprite sprite_validate;
    [SerializeField] Sprite sprite_cancel;
    [SerializeField] List<GameObject> tiles;
    [SerializeField] List<Image> tilesImage;
    [SerializeField] List<TextMeshProUGUI> tilesName;
    [SerializeField] List<Image> tilesStatusImage;
    public static List<Item> searchItems = new List<Item>();
    [SerializeField] GridNavigator gridNavigator;


    private void Awake()
    {
        instance = this;

    }

    public static void Initialize()
    {
        LinkValues();
        instance.gridNavigator.LinkValues(instance.tiles, searchItems);
        ChoiceMultiSelectable.status_yes = instance.sprite_validate;
        ChoiceMultiSelectable.status_no = instance.sprite_cancel;
    }


    public static void LinkValues()
    {
        for (int i = 0; i < 4; i++)
        {
            instance.tilesImage[i].sprite = searchItems[i].image;
            instance.tilesName[i].text = searchItems[i].name;
        }
        for (int i = 0; i < 2; i++)
        {
            ChoiceMultiSelectable choice = (ChoiceMultiSelectable)searchItems[i];
            choice.spriteStatus = instance.tilesStatusImage[i];
        }
    }

    public static void OnSearch()
    {
        Debug.Log("searching");
        List<Item> genreItems = ChoiceMultiSelectable.SearchGenre();
        List<Item> nbJoueursItems = ChoicePlayers.SearchGenre();
        List<Item> searchResults = new List<Item>();
        foreach (Item item in nbJoueursItems)
        {
            if (genreItems.Contains(item))
            {
                searchResults.Add(item);
            }
        }
        if (searchResults.Count == 0) OnNoResult();
        else
        {
            GridNavigator.mainGridDisplay.Initialize(searchResults);
        }
    }

    static void OnNoResult()
    {
        Debug.Log("no result");
        //Display message
    }
}
