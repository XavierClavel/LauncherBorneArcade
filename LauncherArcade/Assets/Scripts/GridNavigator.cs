using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System;

public class GridNavigator : MonoBehaviour
{

    [SerializeField] GameObject mainLayout;
    [SerializeField] GameObject tileTemplate;
    [SerializeField] GameObject gridLayout;
    [SerializeField] GameObject gridPage;
    [HideInInspector] public List<Item> items;
    private int nbGames;
    int currentGridIndex = 0;
    int lastGridIndex = 0;
    [SerializeField] int nbColumns = 7;
    List<GameObject> gridTiles = new List<GameObject>();
    RectTransform gridTransform;
    [SerializeField] TextMeshProUGUI nbLinesDisplay;

    [SerializeField] List<GameObject> showOnTop;
    [SerializeField] List<GameObject> hideOnTop;
    IntPtr launcherWindow;
    int nbLinesToAbsorb = 2;
    int nbLinesAbsorbed = 0;
    int nbLines;
    int currentLine = 1;

    public static SC_LauncherControler launcherControler;
    [Header("Direction Events")]
    [SerializeField] UnityEvent onTop;
    [SerializeField] UnityEvent onLeft;
    [SerializeField] UnityEvent onRight;
    public static GridNavigator mainGridDisplay;
    public List<Image> statusIcons = new List<Image>();

    public void Initialize(List<Item> items)
    {
        currentGridIndex = 0;
        lastGridIndex = 0;

        launcherControler = SC_LauncherControler.instance;
        foreach (GameObject gridTile in gridTiles)
        {
            Destroy(gridTile);
        }
        gridTiles = new List<GameObject>();

        this.items = items;

        RectTransform templateRectTransform = tileTemplate.GetComponent<RectTransform>();
        gridTransform = gridLayout.GetComponent<RectTransform>();

        foreach (Item item in items)
        {
            GameObject gridTile = Instantiate(tileTemplate);
            RectTransform rectTransform = gridTile.GetComponent<RectTransform>();
            rectTransform.SetParent(gridLayout.transform);
            rectTransform.localPosition = Vector3.zero;
            rectTransform.localScale = Vector3.one;
            rectTransform.localRotation = Quaternion.identity;

            gridTile.GetComponent<Image>().sprite = item.image;
            gridTile.GetComponentInChildren<TextMeshProUGUI>().text = item.name;

            Image[] images = gridTile.GetComponentsInChildren<Image>();
            if (images.Length == 2)
            {
                UnityEngine.Debug.Log("sprite status icon");
                ChoiceMultiSelectable choice = (ChoiceMultiSelectable)item;
                choice.spriteStatus = images[1];
            }
            gridTile.SetActive(true);

            gridTiles.Add(gridTile);
        }

        nbGames = gridTiles.Count;

        nbLines = nbGames % nbColumns == 0 ? nbGames / nbColumns : nbGames / nbColumns + 1;
        UnityEngine.Debug.Log(nbGames);
        Activate();
    }

    public void LinkValues(List<GameObject> tiles, List<Item> items)
    {
        this.items = items;
        gridTiles = tiles;
        nbGames = gridTiles.Count;
    }

    public void Activate()
    {
        gridTiles[currentGridIndex].GetComponent<Animator>().SetTrigger("Highlighted");
    }

    public Item getCurrentItem()
    {
        return items[currentGridIndex];
    }

    #region navigation

    public void OnLeft()
    {
        if (currentGridIndex % nbColumns == 0)
        {
            // switchTo_OnLeft.SetActive(true);
            // gameObject.SetActive(false);
            onLeft.Invoke();
            return;
        }
        currentGridIndex -= 1;
        Animate();
    }

    public void OnRight()
    {



        if (currentGridIndex != 0 && currentGridIndex % nbColumns == nbColumns - 1)
        {
            onRight.Invoke();
            if (currentGridIndex == nbGames - 1) return;

            if (nbLinesAbsorbed == nbLinesToAbsorb) gridTransform.anchoredPosition += 300 * Vector2.up;
            else nbLinesAbsorbed++;
        }
        currentGridIndex += 1;
        Animate();
    }

    public void OnUp()
    {
        if (currentGridIndex < nbColumns)
        {
            OnTop();
        }
        else
        {
            currentGridIndex -= nbColumns;
            Animate();
            currentLine--;
            if (currentLine == 1)
            {
                if (showOnTop.Count != 0)
                {
                    foreach (GameObject go in showOnTop) go.SetActive(true);
                    foreach (GameObject go in hideOnTop) go.SetActive(false);
                }
            }
            if (nbLinesDisplay != null) nbLinesDisplay.text = currentLine + "/" + nbLines;
            if (nbLinesAbsorbed == 0) gridTransform.anchoredPosition -= 300 * Vector2.up;
            else nbLinesAbsorbed--;
        }
    }

    void OnTop()
    {
        onTop.Invoke();
        // if (switchTo_GridNavigator_OnTop != null)
        // {
        //     gridTiles[currentGridIndex].GetComponent<Animator>().SetTrigger("Normal");
        //     SC_LauncherControler.gridNavigator = switchTo_GridNavigator_OnTop;
        //     switchTo_GridNavigator_OnTop.Activate();
        //     gridPage.GetComponent<RectTransform>().anchoredPosition -= 300 * Vector2.up;
        // }
        // else
        // {
        //     // gridPage.SetActive(false);
        //     // mainLayout.SetActive(true);
        //     GridEvent.DisplayMainPanel();
        //     SC_LauncherControler.isInMainDisplay = true;
        // }
    }

    public void OnDown()
    {
        int newValue = currentGridIndex + nbColumns;
        if (newValue >= nbGames)
        {
            OnBottom();
            return;
        }
        currentGridIndex = newValue;
        if (currentLine == 1)
        {
            foreach (GameObject go in showOnTop) go.SetActive(false);
            foreach (GameObject go in hideOnTop) go.SetActive(true);
        }
        currentLine++;
        if (nbLinesDisplay != null) nbLinesDisplay.text = currentLine + "/" + nbLines;
        Animate();
        if (nbLinesAbsorbed == nbLinesToAbsorb) gridTransform.anchoredPosition += 300 * Vector2.up;
        else nbLinesAbsorbed++;
    }

    void OnBottom()
    {
        // if (switchTo_GridNavigator_OnBottom != null)
        // {
        //     gridTiles[currentGridIndex].GetComponent<Animator>().SetTrigger("Normal");
        //     SC_LauncherControler.gridNavigator = switchTo_GridNavigator_OnBottom;
        //     switchTo_GridNavigator_OnBottom.Activate();
        //     gridPage.GetComponent<RectTransform>().anchoredPosition += 300 * Vector2.up;
        // }
    }

    #endregion

    public void Animate()
    {
        gridTiles[lastGridIndex].GetComponent<Animator>().SetTrigger("Normal");
        gridTiles[currentGridIndex].GetComponent<Animator>().SetTrigger("Highlighted");
        lastGridIndex = currentGridIndex;
    }

    public void StopAnimating()
    {
        gridTiles[currentGridIndex].GetComponent<Animator>().SetTrigger("Normal");
    }

    public void StartAnimating()
    {
        gridTiles[currentGridIndex].GetComponent<Animator>().SetTrigger("Highlighted");
    }

}
