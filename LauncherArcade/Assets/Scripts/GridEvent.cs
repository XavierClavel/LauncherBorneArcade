using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GridEvent : MonoBehaviour
{
    [SerializeField] RectTransform gridLayout;
    [SerializeField] RectTransform canvas;
    [SerializeField] RectTransform searchPanel;

    [SerializeField] RectTransform toGridArrow;
    [SerializeField] RectTransform toMenuArrow;

    [SerializeField] GridNavigator search_gridNavigator;
    [SerializeField] GridNavigator gridNavigator;
    public static GridEvent instance;
    private void Awake()
    {
        instance = this;
        toMenuArrow.gameObject.SetActive(false);
        toMenuArrow.anchoredPosition = new Vector2(0f, 630f);
        GridNavigator.mainGridDisplay = gridNavigator;
    }
    public static void DisplayGridPanel()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(instance.canvas.DOAnchorPosY(1080f, 0.5f));
        seq.Append(instance.searchPanel.DOAnchorPosX(-1150f, 0.25f).OnComplete(DisplayMenuArrow));
        seq.Append(instance.toMenuArrow.DOAnchorPosY(470f, 0.25f));
    }
    public static void DisplayMainPanel()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(instance.searchPanel.DOAnchorPosX(-1300f, 0.25f));   //retract search panel
        seq.Append(instance.toMenuArrow.DOAnchorPosY(630f, 0.25f).OnComplete(HideMenuArrow));
        seq.Append(instance.canvas.DOAnchorPosY(0f, 0.5f).OnComplete(DisplayGridArrow));
        // seq.Append(instance.toGridArrow.DOAnchorPosY(-460f, 0.25f));
    }

    public static void DisplayGridArrow() => instance.toGridArrow.gameObject.SetActive(true);
    public static void DisplayMenuArrow() => instance.toMenuArrow.gameObject.SetActive(true);
    public static void HideGridArrow() => instance.toGridArrow.gameObject.SetActive(false);
    public static void HideMenuArrow()
    {
        instance.toMenuArrow.gameObject.SetActive(false);
        instance.toMenuArrow.anchoredPosition = new Vector2(0f, 470f);
    }

    public void DisplaySearchPanel()
    {
        gridLayout.DOAnchorPosX(450f, 0.5f);
        GridNavigator.SwitchActiveGridNavigator(search_gridNavigator);
    }

    public void HideSearchPanel()
    {
        gridLayout.DOAnchorPosX(0f, 0.5f);
        GridNavigator.SwitchActiveGridNavigator(gridNavigator);
    }

    public void DisplayExtendedSearchPanel()
    { }
}
