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
using System.Threading;

public enum state { mainDisplay, games, collections }

///<summary>
///Main class used to control the launcher
///</summary>
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
    const float newsWindow = 5f;
    WaitForSeconds holdPeriod = new WaitForSeconds(0.07f);
    WaitForSeconds holdPeriodScroll = new WaitForSeconds(0.3f);
    WaitForSeconds newsHold = new WaitForSeconds(newsWindow);
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

    [DllImport("user32.dll")] static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")] static extern IntPtr SetActiveWindow(IntPtr hWnd);
    [DllImport("user32.dll", EntryPoint = "SetWindowPos")] public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);
    [DllImport("user32.dll")] public static extern bool GetWindowPlacement(IntPtr hWnd, out WindowPlacement lpwndpl);

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct WindowPlacement
    {
        public int length;
        public int flags;
        public ShowWindowCommands showCmd;
        public System.Drawing.Point ptMinPosition;
        public System.Drawing.Point ptMaxPosition;
        public System.Drawing.Rectangle rcNormalPosition;
    }

    public enum ShowWindowCommands : int
    {
        Hide = 0,
        Normal = 1,
        Minimized = 2,
        Maximized = 3,
    }

    const int HWND_TOPMOST = -1;
    const int SWP_NOMOVE = 0x0002;
    const int SWP_NOSIZE = 0x0001;

    //[DllImport("user32.dll", SetLastError = true)]
    //[return: MarshalAs(UnmanagedType.Bool)]
    //static extern bool LockSetForegroundWindow(uint uLockCode);
    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);
    static IntPtr launcherHandle;

    #endregion

    void Awake()
    {
        Application.runInBackground = true;
        //Application.runInBackground = false;
        Cursor.visible = false;
        //LockSetForegroundWindow(1); //prevent app from putting itself in foreground
        //try SetWinEventHook if it does not work
        launcherHandle = GetActiveWindow();
        //SetWindowPos(launcherHandle, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);

        instance = this;
        gridNavigator = instance.mainGridNavigator;
        GridNavigator.launcherControler = this;

        Application.targetFrameRate = -1; //laisser la possibilité de faire du 144Hz

        controls = new Controls();

        if (Application.isEditor) pathToGames = "C:/Users/xrcla/Documents/Game Dev/Launcher Build/Games/"; //"C:/Users/xrcla/Documents/Game Dev/Launcher Build/Games/"; //debug
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

    ///<summary>
    ///Used to switch between news at the time interball defined by the newsWindow const
    ///</summary>
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

    ///<summary>
    ///Returns the selected item.
    ///</summary>
    static Item getCurrentItem()
    {
        if (isInMainDisplay) return games[currentGameIndex];
        else return gridNavigator.getCurrentItem();
    }

    #region navigation

    // Gestion of controls

    ///<summary>
    ///Called on Enter key press.
    ///</summary>
    public void OnEnter(InputAction.CallbackContext context)
    {
        ExitInfo();

        getCurrentItem().OnEnter();
    }

    ///<summary>
    ///Called on Info key press.
    ///</summary>
    public void OnInfo(InputAction.CallbackContext context)
    {
        getCurrentItem().OnInfo();
    }

    ///<summary>
    ///Displays info window for the selected game.
    ///</summary>
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


    ///<summary>
    ///Disables info window.
    ///</summary>
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

    ///<summary>
    ///Called on Volume key press.
    ///</summary>
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

    static Thread t;

    public static Game gameToLaunch;

    ///<summary>
    ///Launches the selected game.
    ///</summary>
    public static void LaunchGame(Game game)
    {
        instance.controls.Disable();
        instance.ShowGameStartedMessage(game.name);
        //ShowWindow(launcherHandle, 2);

        gameToLaunch = game;

        t = new Thread(test);
        t.Start();

        //Process.Start(gameToLaunch.pathToExe);
        //instance.StartCoroutine("TrySetForeground");

        //instance.bg.color = Color.red;
        //process.WaitForInputIdle();

        /*
        IntPtr handle = process.MainWindowHandle;
        if (handle != IntPtr.Zero)
        {
            SetWindowPos(handle, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);
        }
        /*

        instance.bg.color = Color.green;*/
        //SetForegroundWindow(process.MainWindowHandle);
        //SetForegroundWindow(launcherWindow);
        //instance.StartCoroutine("TrySetForeground");
        //try using method every few seconds in coroutine until unfocus
    }
    /*

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
    private static extern bool ShowWindow(IntPtr hWnd, ShowWindowEnum flags);

    [System.Runtime.InteropServices.DllImport("user32.dll")]
    private static extern int SetForegroundWindow(IntPtr hwnd);

    private enum ShowWindowEnum
    {
        Hide = 0,
        ShowNormal = 1, ShowMinimized = 2, ShowMaximized = 3,
        Maximize = 3, ShowNormalNoActivate = 4, Show = 5,
        Minimize = 6, ShowMinNoActivate = 7, ShowNoActivate = 8,
        Restore = 9, ShowDefault = 10, ForceMinimized = 11
    };

    public static void BringMainWindowToFront(Process bProcess)
    {

        // check if the process is running
        if (bProcess != null)
        {
            // check if the window is hidden / minimized
            if (bProcess.MainWindowHandle == IntPtr.Zero)
            {
                // the window is hidden so try to restore it before setting focus.
                ShowWindow(bProcess.Handle, ShowWindowEnum.Restore);
            }

            // set user the focus to the window
            SetForegroundWindow(bProcess.MainWindowHandle);
        }
        else
        {
            // the process is not running, so start it
            bProcess.Start();
        }
    }


    [DllImport("user32.dll")]
    public static extern IntPtr GetForegroundWindow();

    [DllImport("kernel32.dll")]
    public static extern uint GetCurrentThreadId();

    [DllImport("user32.dll", SetLastError = true)]
    public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

    [DllImport("user32.dll")]
    public static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

    public static void SwitchWindow(IntPtr windowHandle)
    {
        if (GetForegroundWindow() == windowHandle)
            return;

        IntPtr foregroundWindowHandle = GetForegroundWindow();
        uint currentThreadId = GetCurrentThreadId();
        uint temp;
        uint foregroundThreadId = GetWindowThreadProcessId(foregroundWindowHandle, out temp);
        AttachThreadInput(currentThreadId, foregroundThreadId, true);
        SetForegroundWindow(windowHandle);
        AttachThreadInput(currentThreadId, foregroundThreadId, false);

        while (GetForegroundWindow() != windowHandle)
        {
        }
    }

    private const uint WS_MINIMIZE = 0x20000000;

    private const uint SW_SHOW = 0x05;
    private const uint SW_MINIMIZE = 0x06;
    private const uint SW_RESTORE = 0x09;

    [DllImport("user32.dll", SetLastError = true)]
    static extern bool BringWindowToTop(IntPtr hWnd);

    [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
    static extern int GetWindowLong(IntPtr hWnd, int nIndex);


    public static void FocusWindow(IntPtr focusOnWindowHandle)
    {
        int style = GetWindowLong(focusOnWindowHandle, -16);

        // Minimize and restore to be able to make it active.
        if ((style & WS_MINIMIZE) == WS_MINIMIZE)
        {
            ShowWindow(focusOnWindowHandle, ShowWindowEnum.Restore);
        }
        uint outPtr;
        uint currentlyFocusedWindowProcessId = GetWindowThreadProcessId(GetForegroundWindow(), out outPtr);
        uint appThread = GetCurrentThreadId();

        if (currentlyFocusedWindowProcessId != appThread)
        {
            AttachThreadInput(currentlyFocusedWindowProcessId, appThread, true);
            BringWindowToTop(focusOnWindowHandle);
            ShowWindow(focusOnWindowHandle, ShowWindowEnum.Show);
            AttachThreadInput(currentlyFocusedWindowProcessId, appThread, false);
        }

        else
        {
            BringWindowToTop(focusOnWindowHandle);
            ShowWindow(focusOnWindowHandle, ShowWindowEnum.Show);
        }
    }

    //[DllImport("user32.dll", SetLastError = true)]
    //[return: MarshalAs(UnmanagedType.Bool)]
    //static extern bool SystemParametersInfo(SPI uiAction, uint uiParam, IntPtr pvParam, SPIF fWinIni)


    /*
        public static void ForceWindowIntoForeground(IntPtr window)
    {
        uint currentThread = GetCurrentThreadId();

        IntPtr activeWindow = GetForegroundWindow();
        uint activeProcess;
        uint activeThread = GetWindowThreadProcessId(activeWindow, out activeProcess);

        uint windowProcess;
        uint windowThread = GetWindowThreadProcessId(window, out windowProcess);

        if (currentThread != activeThread)
            AttachThreadInput(currentThread, activeThread, true);
        if (windowThread != currentThread)
            AttachThreadInput(windowThread, currentThread, true);

        uint oldTimeout = 0, newTimeout = 0;
        SystemParametersInfo(0x2000, 0, ref oldTimeout, 0);
        SystemParametersInfo(Win32.SPI_SETFOREGROUNDLOCKTIMEOUT, 0, ref newTimeout, 0);
        LockSetForegroundWindow(2);
        AllowSetForegroundWindow(Win32.ASFW_ANY);

        SetForegroundWindow(window);
        ShowWindow(window, 9);

        SystemParametersInfo(SPI_SETFOREGROUNDLOCKTIMEOUT, 0, ref oldTimeout, 0);

        if (currentThread != activeThread)
            AttachThreadInput(currentThread, activeThread, false);
        if (windowThread != currentThread)
            AttachThreadInput(windowThread, currentThread, false);
    }
    */

    //[DllImport("user32.dll")]
    //private static extern int RegisterHotKey(IntPtr hWnd, int id, int modifier, Keys vk);

    //[DllImport("user32.dll")]
    //private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

    ///<summary>
    /// Thread used to launch the game and make sure it starts in the foreground.
    ///</summary>
    public static void test()
    {
        process = Process.Start(gameToLaunch.pathToExe);
        process.WaitForInputIdle();
        IntPtr handle = process.MainWindowHandle;

        while (string.IsNullOrEmpty(process.MainWindowTitle))
        {
            System.Threading.Thread.Sleep(100);
            process.Refresh();
        }

        Thread.Sleep(1000);
        if (!process.HasExited)
        {
            ShowWindow(handle, 3);
            SetActiveWindow(handle);
        }
        Thread.Sleep(5000); //:)
        if (!process.HasExited) ShowWindow(launcherHandle, 6);
        instance.HideGameStartedMessage();
        //SetForegroundWindow(handle);
        //BringMainWindowToFront(process);
        //SwitchWindow(handle);


        /*
        while (process != null && !process.HasExited)// && placement.showCmd == ShowWindowCommands.Hide)
        {
            //Thread.Sleep(5000);
            Thread.Sleep(500);

            SetForegroundWindow(handle);
            //ShowWindow(handle, 3);
            ShowWindow(launcherHandle, 6);
            SetActiveWindow(handle);
        }
        */

        process.WaitForExit();
        //SetWindowPos(handle, HWND_TOPMOST, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);
        //ShowWindow(launcherHandle, 7);
        //instance.bg.color = Color.magenta;
        //SetActiveWindow(handle);

        ShowWindow(launcherHandle, 3);

    }



    #region stuff

    ///<summary>
    ///Fills name and logo for the three games displaye in the main screen.
    ///</summary>
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

    ///<summary>
    ///Sets game logo and name for the tile.
    ///</summary>
    private void loadGameData(Item gameToShow, int tileIndex)
    {
        gamestiles[tileIndex].GetComponent<Image>().sprite = gameToShow.image;
        gamestiles[tileIndex].GetComponentInChildren<TextMeshProUGUI>().text = gameToShow.name;
    }

    ///<summary>
    ///Displays a panel informing the user that a game is starting.
    ///</summary>
    private void ShowGameStartedMessage(string gameName)
    {
        gameStartedTransform.gameObject.SetActive(true);
        gameStartedText.text = "starting " + gameName;
    }

    ///<summary>
    ///Hides the panel informing the user that a game is starting.
    ///</summary>
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
        controls.Volume.Enable();
    }

    private void OnDisable()
    {
        controls.Arcade.Disable();
        controls.Volume.Disable();
    }

    #endregion
}
