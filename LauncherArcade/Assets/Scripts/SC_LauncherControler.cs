using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using System;
using Plugins.SystemVolumePlugin;
using System.Runtime.InteropServices;
using UnityEngine.Video;

public enum state { mainDisplay, games, collections }

public class SC_LauncherControler : MonoBehaviour
{
    public static string pathToGames;
    public TextMeshProUGUI newsZone;
    public TextMeshProUGUI gameNumberTxt;

    public Transform gameStartedTransform;
    private TextMeshProUGUI gameStartedText;

    private static Process process;
    private SC_LauncherModel model;
    private Controls controls;

    [SerializeField] private GameObject[] gamestiles = new GameObject[3];
    [SerializeField] GameObject mainLayout;
    [SerializeField] GameObject gridPage;
    public static List<Item> games;
    private int nbGames;
    private static int currentGameIndex;
    public static bool isInMainDisplay = true;
    [SerializeField] GameObject infoWindow;
    [SerializeField] TextMeshProUGUI infoName;
    [SerializeField] TextMeshProUGUI infoDescription;
    [SerializeField] GameObject volumeWindow;
    [SerializeField] Slider volumeSlider;
    WaitForSeconds holdPeriod = new WaitForSeconds(0.07f);
    WaitForSeconds holdPeriodScroll = new WaitForSeconds(0.3f);
    WaitForSeconds newsHold = new WaitForSeconds(5f);
    public Image bg;
    public static InfoController infoController;
    IntPtr launcherWindow;
    [SerializeField] GridNavigator mainGridNavigator;

    public static GridNavigator gridNavigator;
    public static SC_LauncherControler instance;
    public static state currentState = state.mainDisplay;
    [SerializeField] SearchManager searchManager;
    [Header("Grid Layouts")]
    [SerializeField] GridNavigator search_gridNavigator;
    public GridNavigator genreNavigator;


    #region DLLs
    //[DllImport("user32.dll")] static extern bool SetForegroundWindow(IntPtr hWnd);

    //[DllImport("user32.dll")] static extern IntPtr GetActiveWindow();

    //[DllImport("user32.dll", EntryPoint = "SetWindowPos")] public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);
    //const int HWND_TOPMOST = -1;
    //const int SWP_NOMOVE = 0x0002;
    //const int SWP_NOSIZE = 0x0001;

    //[DllImport("user32.dll", SetLastError = true)]
    //[return: MarshalAs(UnmanagedType.Bool)]
    //static extern bool LockSetForegroundWindow(uint uLockCode);

    #endregion

    void Awake()
    {
        //LockSetForegroundWindow(1); //prevent app from putting itself in foreground
        //try SetWinEventHook if it does not work

        instance = this;
        gridNavigator = instance.mainGridNavigator;
        GridNavigator.launcherControler = this;

        //launcherWindow = GetActiveWindow();

        Application.runInBackground = false;
        Application.targetFrameRate = -1; //laisser la possibilité de faire du 144Hz

        controls = new Controls();

        if (Application.isEditor) pathToGames = "C:/Users/xrcla/Documents/Game Dev/Launcher Build/Games/"; //debug
        else pathToGames = Application.dataPath + "/../Games/";

        model = new SC_LauncherModel(pathToGames);
        currentGameIndex = 0;

        // Hide GameStarted message
        gameStartedText = gameStartedTransform.Find("GameStartedBackground").Find("GameStartedText").GetComponent<TextMeshProUGUI>();
        HideGameStartedMessage();
        infoWindow.SetActive(false);
        volumeWindow.SetActive(false);


        // Get games list and show them
        games = model.GetGamesList();
        model.OnGamesLoaded();
        nbGames = games.Count;

        updateGamesPreviews(currentGameIndex);

        // Update texts
        gameNumberTxt.text = "1/" + games.Count;
        DisplayNews();

        // Bind controls
        controls.Arcade.Enter.performed += OnEnter;
        controls.Arcade.Left.started += OnLeft_started;
        controls.Arcade.Left.canceled += OnLeft_canceled;
        controls.Arcade.Right.started += OnRight_started;
        controls.Arcade.Right.canceled += OnRight_canceled;
        controls.Arcade.Up.started += OnUp_started;
        controls.Arcade.Up.canceled += OnUp_canceled;
        controls.Arcade.Down.started += OnDown_started;
        controls.Arcade.Down.canceled += OnDown_canceled;
        controls.Arcade.Info.performed += OnInfo;
        controls.Arcade.Volume.performed += OnVolume;

        controls.Volume.VolumeUp.started += VolumeUp_started;
        controls.Volume.VolumeUp.canceled += VolumeUp_canceled;
        controls.Volume.VolumeDown.started += VolumeDown_started;
        controls.Volume.VolumeDown.canceled += VolumeDown_canceled;
        controls.Volume.VolumeExit.performed += OnVolumeExit;

        //Get system volume
        SystemVolumePlugin.InitializeVolume();
        volumeSlider.value = SystemVolumePlugin.GetVolume() * 100;

        // gridNavigator.Initialize(Collection.collections);
        // gridNavigator.Initialize(Genre.collections);
        //gridNavigator.Initialize(Game.onePlayer_games, this);
        // gridNavigator.Initialize(games);
        // gridNavigator.Initialize(Game.twoPlayer_games, this);
        // gridNavigator.Initialize(Genre.GetGames("Arcade"), this);
        //gridNavigator.Initialize(Collection.GetGames("Jeux sous le sapin 2021"), this);
        // List<Item> searchItems =

        gridNavigator.Initialize(games);
        SearchManager.Initialize();
        UnityEngine.Debug.Log("amount of genres : " + Genre.choices.Count);
        genreNavigator.Initialize(Genre.choices);

    }

    void DisplayNews()
    {
        List<string> news = model.GetNews();
        int nbNews = news.Count;
        if (nbNews == 0) return;
        else if (nbNews == 1) newsZone.text = news[0];
        else StartCoroutine("RotateNews", news);
    }

    IEnumerator RotateNews(List<String> news)
    {
        int nbNews = news.Count;
        while (true)
        {
            int newsIndex = 0;
            while (true)
            {
                newsZone.text = news[newsIndex];
                newsIndex = modulo(newsIndex + 1, nbNews);
                yield return newsHold;
            }
        }
    }


    IEnumerator OnApplicationFocus(bool focusStatus)
    {
        //on utilise la méthode sous forme d'IEnumerator sinon elle est exécutée au début de la frame ce qui fait que le message "Game started"
        //disparait pendant une frame avant le lancement du jeu, ce qui est visible

        if (process == null) yield break;

        if (focusStatus)    //game is closing
        {
            process = null;
            controls.Enable();
        }
        else
        {  //game is starting
            //int nbNews = news.Count;
            StopCoroutine("TrySetForeground");
            HideGameStartedMessage();
        }
        yield return null;
    }

    static Item getCurrentItem()
    {
        if (isInMainDisplay) return games[currentGameIndex];
        else return gridNavigator.getCurrentItem();
    }

    #region navigation

    // Gestion of controls
    public void OnEnter(InputAction.CallbackContext context)
    {
        ExitInfo();

        getCurrentItem().OnEnter();
    }

    public void OnInfo(InputAction.CallbackContext context)
    {
        getCurrentItem().OnInfo();
    }

    public static void DisplayInfo(Game game)
    {
        if (instance.infoWindow.activeInHierarchy)
        {
            instance.ExitInfo();
            return;
        }

        instance.infoName.text = game.name;
        instance.infoDescription.text = game.description;
        infoController.SetupControlsInfo(game);
        instance.infoWindow.SetActive(true);
    }

    public void SetInfo(Game game)
    {
        if (infoWindow.activeInHierarchy)
        {
            ExitInfo();
            return;
        }

        infoName.text = game.name;
        infoDescription.text = game.description;
        infoController.SetupControlsInfo(game);
        infoWindow.SetActive(true);
    }

    public void ExitInfo()
    {
        infoController.ResetVideoPlayer();
        infoWindow.SetActive(false);
    }

    public void OnLeft_started(InputAction.CallbackContext context)
    {
        StopCoroutine("OnRightHold");
        StartCoroutine("OnLeftHold");
    }

    public void OnLeft_canceled(InputAction.CallbackContext context)
    {
        StopCoroutine("OnLeftHold");
    }

    IEnumerator OnLeftHold()
    {
        while (true)
        {
            OnLeft();
            yield return holdPeriodScroll;
        }
    }

    public void OnLeft()
    {
        ExitInfo();
        if (isInMainDisplay)
        {
            currentGameIndex = modulo(currentGameIndex - 1, nbGames);
            updateGamesPreviews(currentGameIndex);
        }
        else gridNavigator.OnLeft();
    }

    public void OnRight_started(InputAction.CallbackContext context)
    {
        StopCoroutine("OnLeftHold");
        StartCoroutine("OnRightHold");
    }

    public void OnRight_canceled(InputAction.CallbackContext context)
    {
        StopCoroutine("OnRightHold");
    }

    IEnumerator OnRightHold()
    {
        while (true)
        {
            OnRight();
            yield return holdPeriodScroll;
        }
    }

    public void OnRight()
    {
        ExitInfo();
        if (isInMainDisplay)
        {
            currentGameIndex = modulo(currentGameIndex + 1, nbGames);
            updateGamesPreviews(currentGameIndex);
        }
        else gridNavigator.OnRight();
    }

    public void OnUp_started(InputAction.CallbackContext context)
    {
        StopCoroutine("OnDownHold");
        StartCoroutine("OnUpHold");
    }

    public void OnUp_canceled(InputAction.CallbackContext context)
    {
        StopCoroutine("OnUpHold");
    }

    IEnumerator OnUpHold()
    {
        while (true)
        {
            OnUp();
            yield return holdPeriodScroll;
        }
    }

    public void OnUp()
    {
        ExitInfo();
        if (isInMainDisplay) return;
        else gridNavigator.OnUp();

    }

    public void OnDown_started(InputAction.CallbackContext context)
    {
        StopCoroutine("OnUpHold");
        StartCoroutine("OnDownHold");
    }

    public void OnDown_canceled(InputAction.CallbackContext context)
    {
        StopCoroutine("OnDownHold");
    }

    IEnumerator OnDownHold()
    {
        while (true)
        {
            OnDown();
            yield return holdPeriodScroll;
        }
    }

    public void OnDown()
    {
        ExitInfo();
        if (isInMainDisplay)
        {
            // mainLayout.SetActive(false);
            // gridPage.SetActive(true);
            isInMainDisplay = false;
            GridEvent.DisplayGridPanel();

            //searchManager.gameObject.SetActive(true);
            //gridNavigator.Initialize(MetaCollection.metaCollections);

        }
        else gridNavigator.OnDown();
    }

    #endregion

    #region volume

    public void OnVolume(InputAction.CallbackContext context)
    {
        volumeWindow.SetActive(true);
        controls.Arcade.Disable();
        controls.Volume.Enable();
    }

    public void OnVolumeExit(InputAction.CallbackContext context)
    {
        volumeWindow.SetActive(false);
        controls.Arcade.Enable();
        controls.Volume.Disable();
    }

    public void VolumeUp_started(InputAction.CallbackContext context)
    {
        StartCoroutine("VolumeUp");
    }

    public void VolumeUp_canceled(InputAction.CallbackContext context)
    {
        StopCoroutine("VolumeUp");
    }

    IEnumerator VolumeUp()
    {
        while (true)
        {
            volumeSlider.value++;
            SetSystemVolume(volumeSlider.value);
            yield return holdPeriod;
        }
    }

    public void VolumeDown_started(InputAction.CallbackContext context)
    {
        StartCoroutine("VolumeDown");
    }

    public void VolumeDown_canceled(InputAction.CallbackContext context)
    {
        StopCoroutine("VolumeDown");
    }

    IEnumerator VolumeDown()
    {
        while (true)
        {
            volumeSlider.value--;
            SetSystemVolume(volumeSlider.value);
            yield return holdPeriod;
        }
    }

    public void SetSystemVolume(float newVolume)
    {
        SystemVolumePlugin.SetVolume(newVolume / 100f);
    }

    #endregion

    // Launch the game (at the current game index) in the main emplacement 
    public static void LaunchGame(Game game)
    {
        instance.controls.Disable();
        instance.ShowGameStartedMessage(game.name);
        process = Process.Start(game.pathToExe);
        //instance.bg.color = Color.red;
        //process.WaitForInputIdle();

        /*IntPtr handle = process.MainWindowHandle;
        if (handle != IntPtr.Zero)
        {
            SetWindowPos(handle, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);
        }

        instance.bg.color = Color.green;*/
        //SetForegroundWindow(process.MainWindowHandle);
        //SetForegroundWindow(launcherWindow);
        //instance.StartCoroutine("TrySetForeground");
        //try using method every few seconds in coroutine until unfocus
    }

    IEnumerator TrySetForeground()
    {
        WaitForEndOfFrame waitFrame = new WaitForEndOfFrame();
        WaitForSeconds tryForegroundPeriod = new WaitForSeconds(5f);
        // yield return new WaitWhile(() => launcherWindow == GetActiveWindow());

        bg.color = Color.green;
        yield return new WaitWhile(() => process.Responding);
        // while (!process.Responding)
        // {
        //     //bg.color = Color.magenta;
        //     //SetForegroundWindow(process.MainWindowHandle);
        //     SetForegroundWindow(launcherWindow);
        //     yield return waitFrame;
        //     //yield return tryForegroundPeriod
        // }
        bg.color = Color.blue;
    }

    // Show the correct game depending on the currenGameIndex
    public void updateGamesPreviews(int currentGameIndex)
    {
        int nbGames = games.Count;
        if (nbGames < 3)
        {
            // On peut charger un jeu avant dans la liste
            if (currentGameIndex != 0)
            {
                loadGameData(games[modulo(currentGameIndex - 1, nbGames)], 0);
                // Supprimer la case de droite comme il n'y a que 2 jeux
                gamestiles[2].GetComponent<Image>().sprite = null;
                gamestiles[2].GetComponent<Image>().color = new Color(0x5B, 0x5B, 0x5B);
                gamestiles[2].GetComponentInChildren<TextMeshProUGUI>().text = "";
            }

            // On peut charger un jeu après dans la liste
            if (currentGameIndex != games.Count - 1)
            {
                loadGameData(games[modulo(currentGameIndex + 1, nbGames)], 2);
                // Supprimer la case de gauche comme il n'y a que 2 jeux
                gamestiles[0].GetComponent<Image>().sprite = null;
                gamestiles[0].GetComponent<Image>().color = new Color(0x5B, 0x5B, 0x5B);
                gamestiles[0].GetComponentInChildren<TextMeshProUGUI>().text = "";
            }

            loadGameData(games[currentGameIndex], 1);
        }
        else
        {
            loadGameData(games[modulo(currentGameIndex - 1, nbGames)], 0);
            loadGameData(games[currentGameIndex], 1);
            loadGameData(games[modulo(currentGameIndex + 1, nbGames)], 2);
        }

        gameNumberTxt.text = currentGameIndex + 1 + "/" + games.Count;

    }

    // Load the gameToShow in the correct emplacement (tile)
    private void loadGameData(Item gameToShow, int tileIndex)
    {
        gamestiles[tileIndex].GetComponent<Image>().sprite = gameToShow.image;
        gamestiles[tileIndex].GetComponentInChildren<TextMeshProUGUI>().text = gameToShow.name;
    }

    private void ShowGameStartedMessage(string gameName)
    {
        gameStartedTransform.gameObject.SetActive(true);
        gameStartedText.text = "starting " + gameName;
    }

    private void HideGameStartedMessage()
    {
        gameStartedTransform.gameObject.SetActive(false);
    }

    private int modulo(int a, int n)
    {
        return ((a % n) + n) % n;
    }

    private void OnEnable()
    {
        controls.Arcade.Enable();
        controls.Volume.Disable();
    }

    private void OnDisable()
    {
        controls.Arcade.Disable();
        controls.Volume.Disable();
    }
}
