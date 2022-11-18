using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchButton : Item
{
    public override void OnEnter()
    {
        SearchManager.OnSearch();
    }
}
