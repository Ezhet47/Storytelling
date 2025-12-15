using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public string StartGameSceneName => startGameSceneName;
    public string MainMenuSceneName => mainMenuSceneName;

    [Header("Scene Names")]
    [SerializeField] private string startGameSceneName = "Level_0";
    [SerializeField] private string mainMenuSceneName = "MainMenu";

    [Header("Global Collect Flags")]
    public bool milkPicked;
    public bool exerciseBookPicked;
    public bool newspaperPicked;

    [Header("Global NPC Flags")]
    public int annaDialogueStage;

    [Header("Global One-shot Flags")]
    public bool MonologueTriggered;
    public bool cardboardBoxInteracted;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ContinuePlay()
    {
        ChangeScene(startGameSceneName);
    }

    public void GoToMainMenu()
    {
        ChangeScene(mainMenuSceneName);
    }

    public void GoToLevel1()
    {
        annaDialogueStage = 1;
        ChangeScene("Level_1");
    }

    public void RestartScene()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        ChangeScene(sceneName);
    }

    public void ChangeScene(string sceneName)
    {
        Time.timeScale = 1f;
        StartCoroutine(ChangeSceneCo(sceneName));
    }

    private IEnumerator ChangeSceneCo(string sceneName)
    {
        UI_FadeScreen fadeScreen = FindFadeScreenUI();
        if (fadeScreen != null)
        {
            fadeScreen.DoFadeOut();
            yield return fadeScreen.fadeEffectCo;
        }

        SceneManager.LoadScene(sceneName);

        yield return null;

        fadeScreen = FindFadeScreenUI();
        if (fadeScreen != null)
        {
            fadeScreen.DoFadeIn();
        }

        Player player = Player.instance;
        if (player == null)
            yield break;

        Object_Waypoint waypoint = FindFirstObjectByType<Object_Waypoint>();
        if (waypoint != null)
        {
            Vector3 spawnPos = waypoint.GetRespawnPosition();
            player.TeleportPlayer(spawnPos);
        }
    }

    private UI_FadeScreen FindFadeScreenUI()
    {
        if (UI.instance != null)
            return UI.instance.fadeScreenUI;

        return FindFirstObjectByType<UI_FadeScreen>();
    }

    private HashSet<string> triggeredMonologueIds = new HashSet<string>();

    public bool IsMonologueTriggered(string id)
    {
        if (string.IsNullOrEmpty(id))
            return false;

        return triggeredMonologueIds.Contains(id);
    }

    public void MarkMonologueTriggered(string id)
    {
        if (string.IsNullOrEmpty(id))
            return;

        triggeredMonologueIds.Add(id);
    }
}
