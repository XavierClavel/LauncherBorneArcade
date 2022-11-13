using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
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
    const int nbColumns = 7;
    List<GameObject> gridTiles = new List<GameObject>();
    RectTransform gridTransform;
    [SerializeField] TextMeshProUGUI nbLinesDisplay;

    [SerializeField] List<GameObject> showOnTop;
    [SerializeField] List<GameObject> hideOnTop;
    [SerializeField] GameObject switchTo_OnRight;
    [SerializeField] GameObject switchTo_OnLeft;
    [SerializeField] GameObject switchTo_OnTop;
    [SerializeField] GameObject switchTo_OnBottom;
    IntPtr launcherWindow;
    int nbLinesToAbsorb = 2;
    int nbLinesAbsorbed = 0;
    int nbLines;
    int currentLine = 1;

    public static SC_LauncherControler launcherControler;
    public static GridNavigator instance;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        UnityEngine.Debug.Log(gridTiles[currentGridIndex].activeSelf);
        gridTiles[currentGridIndex].GetComponent<Animator>().SetBool("Highlighted", true);
        launcherControler.gridNavigator = this;
        nbLinesAbsorbed = 0;
    }

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
            gridTile.SetActive(true);

            gridTiles.Add(gridTile);
        }

        nbGames = gridTiles.Count;

        nbLines = nbGames % nbColumns == 0 ? nbGames / nbColumns : nbGames / nbColumns + 1;

        Activate();
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

        if (currentGridIndex == 0) return;

        if (currentGridIndex % nbColumns == 0)
        {
            switchTo_OnLeft.SetActive(true);
            gameObject.SetActive(false);
        }
        currentGridIndex -= 1;
        Animate();
    }

    public void OnRight()
    {
        if (currentGridIndex == nbGames - 1) return;

        if (currentGridIndex != 0 && currentGridIndex % nbColumns == nbColumns - 1) gridTransform.anchoredPosition += 300 * Vector2.up;
        currentGridIndex += 1;
        Animate();
    }

    public void OnUp()
    {
        if (currentGridIndex < nbColumns)
        {
            gridPage.SetActive(false);
            mainLayout.SetActive(true);
            SC_LauncherControler.isInMainDisplay = true;
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
            nbLinesDisplay.text = currentLine + "/" + nbLines;
            if (nbLinesAbsorbed == 0) gridTransform.anchoredPosition -= 300 * Vector2.up;
            else nbLinesAbsorbed--;
        }
    }

    public void OnDown()
    {
        int newValue = currentGridIndex + nbColumns;
        if (newValue >= nbGames) return;
        currentGridIndex = newValue;
        if (currentLine == 1)
        {
            foreach (GameObject go in showOnTop) go.SetActive(false);
            foreach (GameObject go in hideOnTop) go.SetActive(true);
        }
        currentLine++;
        nbLinesDisplay.text = currentLine + "/" + nbLines;
        Animate();
        if (nbLinesAbsorbed == nbLinesToAbsorb) gridTransform.anchoredPosition += 300 * Vector2.up;
        else nbLinesAbsorbed++;
    }

    #endregion

    public void Animate()
    {
        gridTiles[lastGridIndex].GetComponent<Animator>().SetTrigger("Normal");
        gridTiles[currentGridIndex].GetComponent<Animator>().SetTrigger("Highlighted");
        lastGridIndex = currentGridIndex;
    }

}
