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

public class SC_LauncherControler : MonoBehaviour
{
    public static string pathToGames;
    public TextMeshProUGUI newsZone;
    public TextMeshProUGUI gameNumberTxt;

    public Transform gameStartedTransfort;
    private TextMeshProUGUI gameStartedText;

    private Process process;
    private SC_LauncherModel model;
    private Controls controls;

    [SerializeField] private GameObject[] gamestiles = new GameObject[3];
    [SerializeField] GameObject mainLayout;
    [SerializeField] GameObject tileTemplate;
    [SerializeField] GameObject gridLayout;
    private List<Game> games;
    private int nbGames;
    private int currentGameIndex;
    int currentGridIndex = 0;
    int lastGridIndex = 0;
    const int nbColumns = 7;
    bool isInMainDisplay = true;
    List<GameObject> gridTiles = new List<GameObject>();
    RectTransform gridTransform;
    [SerializeField] GameObject infoWindow;
    [SerializeField] TextMeshProUGUI infoName;
    [SerializeField] TextMeshProUGUI infoDescription;
    [SerializeField] GameObject volumeWindow;
    [SerializeField] Slider volumeSlider;
    [SerializeField] VideoPlayer videoPlayer;
    [SerializeField] Image imageDisplay;
    [SerializeField] TextMeshProUGUI nbJoueursDisplay;
    WaitForSeconds holdPeriod = new WaitForSeconds(0.07f);
    WaitForSeconds holdPeriodScroll = new WaitForSeconds(0.3f);
    [SerializeField] Image bg;

    [DllImport("user32.dll")] static extern bool SetForegroundWindow(IntPtr hWnd);

    [DllImport("user32.dll")] static extern IntPtr GetActiveWindow();
    IntPtr launcherWindow;

    void Awake()
    {
        launcherWindow = GetActiveWindow();

        Application.runInBackground = false;
        Application.targetFrameRate = -1; //laisser la possibilité de faire du 144Hz

        controls = new Controls();

        pathToGames = Application.dataPath + "/../Games/";
        model = new SC_LauncherModel(pathToGames);
        currentGameIndex = 0;

        // Hide GameStarted message
        gameStartedText = gameStartedTransfort.Find("GameStartedBackground").Find("GameStartedText").GetComponent<TextMeshProUGUI>();
        HideGameStartedMessage();
        infoWindow.SetActive(false);
        volumeWindow.SetActive(false);

        // Get games list and show them
        games = model.GetGamesList();
        //games = SetupTestEnvironment.getGamesDummy(); // For coding/Debug
        nbGames = games.Count;

        updateGamesPreviews(currentGameIndex);

        // Update texts
        gameNumberTxt.text = "1/" + games.Count;
        newsZone.text = model.GetNews();

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

        RectTransform templateRectTransform = tileTemplate.GetComponent<RectTransform>();
        gridTransform = gridLayout.GetComponent<RectTransform>();

        foreach (Game game in games)
        {
            //for (int i = 0; i < 4; i++)
            //{
            GameObject gridTile = Instantiate(tileTemplate);
            RectTransform rectTransform = gridTile.GetComponent<RectTransform>();
            rectTransform.SetParent(gridLayout.transform);
            rectTransform.localPosition = Vector3.zero;
            rectTransform.localScale = Vector3.one;
            rectTransform.localRotation = Quaternion.identity;

            gridTile.GetComponent<Image>().sprite = game.logo;
            gridTile.GetComponentInChildren<TextMeshProUGUI>().text = game.name;
            gridTile.SetActive(true);

            gridTiles.Add(gridTile);
            //}
        }

        gridTiles[0].GetComponent<Animator>().SetTrigger("Highlighted");

        //nbGames = gridTiles.Count;
        SystemVolumePlugin.InitializeVolume();
        volumeSlider.value = SystemVolumePlugin.GetVolume() * 100;

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
            bg.color = Color.red;
            StopCoroutine("TrySetForeground");
            HideGameStartedMessage();
        }
        yield return null;
    }

    Game getCurrentGame()
    {
        if (isInMainDisplay) return games[currentGameIndex];
        else return games[currentGridIndex];
    }

    #region navigation

    // Gestion of controls
    public void OnEnter(InputAction.CallbackContext context)
    {
        infoWindow.SetActive(false);
        LaunchGame(getCurrentGame());
    }

    public void OnInfo(InputAction.CallbackContext context)
    {
        if (infoWindow.activeInHierarchy)
        {
            infoWindow.SetActive(false);
            return;
        }

        Game currentGame = getCurrentGame();
        infoName.text = currentGame.name;
        infoDescription.text = currentGame.description;
        InfoController.SetupControlsInfo(currentGame.controlsInfo);
        if (currentGame.videoUrl != null)
        {
            imageDisplay.gameObject.SetActive(false);
            videoPlayer.gameObject.SetActive(true);
            videoPlayer.url = currentGame.videoUrl;
        }
        else
        {
            videoPlayer.gameObject.SetActive(false);
            imageDisplay.gameObject.SetActive(true);
            imageDisplay.sprite = currentGame.logo;
        }
        string nbDisplay;
        switch (currentGame.controlsInfo.nb_joueurs)
        {
            case "1":
                nbDisplay = "1 joueur";
                break;

            case "2":
                nbDisplay = "2 joueurs";
                break;

            case "1-2":
                nbDisplay = "1-2 joueurs";
                break;

            default:
                nbDisplay = "? joueurs";
                break;
        }
        nbJoueursDisplay.text = nbDisplay;
        infoWindow.SetActive(true);
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
        infoWindow.SetActive(false);
        if (isInMainDisplay)
        {
            currentGameIndex = modulo(currentGameIndex - 1, nbGames);
            updateGamesPreviews(currentGameIndex);
        }
        else
        {
            if (currentGridIndex == 0) return;

            if (currentGridIndex % nbColumns == 0) gridTransform.anchoredPosition -= 300 * Vector2.up;
            currentGridIndex -= 1;
            Animate();
        }
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
        infoWindow.SetActive(false);
        if (isInMainDisplay)
        {
            currentGameIndex = modulo(currentGameIndex + 1, nbGames);
            updateGamesPreviews(currentGameIndex);
        }
        else
        {
            if (currentGridIndex == nbGames - 1) return;

            if (currentGridIndex != 0 && currentGridIndex % nbColumns == nbColumns - 1) gridTransform.anchoredPosition += 300 * Vector2.up;
            currentGridIndex += 1;
            Animate();
        }
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
        infoWindow.SetActive(false);
        if (isInMainDisplay) return;
        if (currentGridIndex < nbColumns)
        {
            gridLayout.SetActive(false);
            mainLayout.SetActive(true);
            isInMainDisplay = true;
        }
        else
        {
            currentGridIndex -= nbColumns;
            Animate();
            gridTransform.anchoredPosition -= 300 * Vector2.up;
        }
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
        infoWindow.SetActive(false);
        if (isInMainDisplay)
        {
            mainLayout.SetActive(false);
            gridLayout.SetActive(true);
            isInMainDisplay = false;
            gridTiles[currentGridIndex].GetComponent<Animator>().SetTrigger("Highlighted");
        }
        else
        {
            int newValue = currentGridIndex + nbColumns;
            if (newValue >= nbGames) return;
            currentGridIndex = newValue;
            Animate();
            gridTransform.anchoredPosition += 300 * Vector2.up;
        }
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


    public void Animate()
    {
        gridTiles[lastGridIndex].GetComponent<Animator>().SetTrigger("Normal");
        gridTiles[currentGridIndex].GetComponent<Animator>().SetTrigger("Highlighted");
        lastGridIndex = currentGridIndex;
    }

    // Launch the game (at the current game index) in the main emplacement 
    private void LaunchGame(Game game)
    {
        controls.Disable();
        ShowGameStartedMessage(game.name);
        process = Process.Start(game.pathToExe);
        //SetForegroundWindow(process.MainWindowHandle);
        SetForegroundWindow(launcherWindow);
        StartCoroutine("TrySetForeground");
        //try using method every few seconds in coroutine until unfocus
    }

    IEnumerator TrySetForeground()
    {
        WaitForEndOfFrame waitFrame = new WaitForEndOfFrame();
        WaitForSeconds tryForegroundPeriod = new WaitForSeconds(5f);
        //yield return new WaitWhile(() => launcherWindow == GetActiveWindow());
        while (true)
        {
            bg.color = Color.magenta;
            //SetForegroundWindow(process.MainWindowHandle);
            SetForegroundWindow(launcherWindow);
            yield return waitFrame;
            //yield return tryForegroundPeriod
        }
    }

    // Show the correct game depending on the currenGameIndex
    private void updateGamesPreviews(int currentGameIndex)
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
    private void loadGameData(Game gameToShow, int tileIndex)
    {
        gamestiles[tileIndex].GetComponent<Image>().sprite = gameToShow.logo;
        gamestiles[tileIndex].GetComponentInChildren<TextMeshProUGUI>().text = gameToShow.name;
    }

    private void ShowGameStartedMessage(string gameName)
    {
        gameStartedTransfort.gameObject.SetActive(true);
        gameStartedText.text = "starting " + gameName;
    }

    private void HideGameStartedMessage()
    {
        gameStartedTransfort.gameObject.SetActive(false);
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
