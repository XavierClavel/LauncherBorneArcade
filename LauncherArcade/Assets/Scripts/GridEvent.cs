using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GridEvent : MonoBehaviour
{
    [SerializeField] RectTransform gridLayout;
    [SerializeField] RectTransform canvas;
    [SerializeField] RectTransform searchPanel;
    [SerializeField] GridNavigator search_gridNavigator;
    [SerializeField] GridNavigator gridNavigator;
    public static GridEvent instance;
    private void Awake()
    {
        instance = this;
    }
    public static void DisplayGridPanel()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(instance.canvas.DOAnchorPosY(1080f, 0.5f));
        seq.Append(instance.searchPanel.DOAnchorPosX(-1150f, 0.25f));
    }
    public static void DisplayMainPanel()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(instance.searchPanel.DOAnchorPosX(-1300f, 0.25f));
        seq.Append(instance.canvas.DOAnchorPosY(0f, 0.5f));
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
}
