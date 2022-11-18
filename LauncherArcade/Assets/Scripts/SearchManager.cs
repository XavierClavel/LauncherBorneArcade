using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchManager : MonoBehaviour
{
    [SerializeField] GridNavigator nbJoueursGrid;
    [SerializeField] GridNavigator genreGrid;
    [SerializeField] GridNavigator searchButtonGrid;
    static SearchManager instance;

    private void OnEnable()
    {
        instance = this;
        nbJoueursGrid.Initialize(ChoiceUniqueSelectable.choices);
        genreGrid.Initialize(ChoiceMultiSelectable.choices);

        SC_LauncherControler.gridNavigator = nbJoueursGrid;
        nbJoueursGrid.Activate();
    }

    public void InitializeDisplay()
    {
        instance = this;
        genreGrid.Initialize(Genre.collections);
    }

    public static void OnSearch()
    {
        List<Item> genreItems = ChoiceMultiSelectable.SearchGenre();
        List<Item> nbJoueursItems = ChoiceUniqueSelectable.SearchNbJoueurs();
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
            SC_LauncherControler.gridNavigator.Initialize(searchResults);
        }
    }

    static void OnNoResult()
    {
        //Display message
    }
}
