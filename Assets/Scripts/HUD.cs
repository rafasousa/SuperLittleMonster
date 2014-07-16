using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour
{
    public static bool pause = false;

    public GUISkin guiSkin;

    public float playerCoin = 0;

    public float playerScore = 0;

    public int playerMonster = 0;

    public static float Height = 60;

    public static float Width = 200;

    public static float Top = 200;

    public static float Left = Screen.width / 2 - 100;

    private delegate void GUIMethod();

    private GUIMethod currentGUIMethod;

    void Start()
    {
        SoundEffectsHelper.Instance.MakeBackgroundSound(SoundType.Background);

        pause = false;
    }

    void Update()
    {
        playerScore += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Escape))
            pause = !pause;

        if (pause)
            Time.timeScale = 0.00001f;
        else
            Time.timeScale = 1f;

        if (VelocityController.IsBoostSpeedMax)
            SoundEffectsHelper.Instance.SetPitchBackgroundSound(1.02f);
    }

    void OnGUI()
    {
        GUI.skin = guiSkin;

        var guiStyle = new GUIStyle(GUI.skin.GetStyle("label"));
        guiStyle.normal.textColor = Color.yellow;

        GUI.Label(new Rect(10, 10, 350, 100), LangHelper.GetInstance().GetString("CoinLabel") + playerCoin, guiStyle);

        GUI.Label(new Rect(Screen.width - 220, 10, 300, 70), LangHelper.GetInstance().GetString("ScoreLabel") + (int)(playerScore * 100));

        if (pause)
            PauseMenuGUI();
    }

    void PauseMenuGUI()
    {
        GUI.Box(new Rect(10, 10, Screen.width - 20, Screen.height - 20), "", GUI.skin.GetStyle("box"));

        if (GUI.Button(new Rect(HUD.Left, 80, HUD.Width, HUD.Height), LangHelper.GetInstance().GetString("BackButton"), GUI.skin.GetStyle("button")))
            pause = false;

        if (GUI.Button(new Rect(HUD.Left, 160, HUD.Width, HUD.Height), LangHelper.GetInstance().GetString("SoundButton") + (SoundEffectsHelper.Instance.HasSound ? "On" : "Off"), GUI.skin.GetStyle("button")))
            SoundEffectsHelper.Instance.SetSound();

        if (GUI.Button(new Rect(HUD.Left, 240, HUD.Width, HUD.Height), LangHelper.GetInstance().GetString("MusicButton") + (SoundEffectsHelper.Instance.HasMusic ? "On" : "Off"), GUI.skin.GetStyle("button")))
            SoundEffectsHelper.Instance.SetMusic();

        if (GUI.Button(new Rect(HUD.Left, 320, HUD.Width, HUD.Height), LangHelper.GetInstance().GetString("ExitButton"), GUI.skin.GetStyle("button")))
            Application.LoadLevel("Menu");
    }

    public void OnDisable()
    {
        PlayerPrefs.SetInt("Score", (int)(playerScore * 100));

        PlayerPrefs.SetInt("Coin", (int)playerCoin);

        var totalCoin = PlayerPrefs.GetInt("TotalCoins", 0);

        PlayerPrefs.SetInt("TotalCoins", totalCoin + (int)playerCoin);
    }

    public void IncreaseScore(int amount)
    {
        playerScore += amount;
    }

    public void IncreaseCoin(int amount)
    {
        playerCoin += amount;
    }

    public void IncreaseMonster(int amount)
    {
        playerMonster += amount;
    }

}
