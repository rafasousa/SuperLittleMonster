using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour
{
    public static bool IsExit = false;

    public GUISkin guiSkin;

    public float playerCoin = 0;

    public float playerScore = 0;

    public int playerMonster = 0;

    public static float AverageScreenX = Screen.width / 2;

    public static float AverageScreenY = Screen.height / 2;

    public static float Width = (AverageScreenX / 1);

    public static float Height = (AverageScreenY / 3.6f);

    public static float Left = AverageScreenX - (Width) / 2;

    public static int TitleSize = Screen.width / 14;

    public static int LabelSize = Screen.width / 20;

    public static int ButtonSize = Screen.width / 16;

    public static float Margin = 5f;

    private delegate void GUIMethod();

    private GUIMethod currentGUIMethod;

    void Start()
    {
        SoundEffectsHelper.Instance.MakeBackgroundSound(SoundType.Background);

        IsExit = false;
    }

    void Update()
    {
        playerScore += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Escape))
            IsExit = !IsExit;

        if (IsExit)
        {
            Time.timeScale = 0.00001f;
            SoundEffectsHelper.Instance.SetMute(true);
        }
        else
        {
            Time.timeScale = 1f;
            SoundEffectsHelper.Instance.SetMute(false);
        }

        if (VelocityController.IsBoostSpeedMax)
            SoundEffectsHelper.Instance.SetPitchBackgroundSound(1.02f);
    }

    void OnGUI()
    {
        GUI.skin = guiSkin;

        guiSkin.label.fontSize = HUD.LabelSize;
        guiSkin.button.fontSize = HUD.ButtonSize;

        var guiStyle = new GUIStyle(GUI.skin.GetStyle("label"));
        guiStyle.normal.textColor = Color.yellow;

		GUI.Label(new Rect(10, 10, Screen.width, HUD.Height), LangHelper.GetInstance().GetString("CoinLabel") + playerCoin, guiStyle);

		GUI.Label(new Rect(10, guiStyle.lineHeight + 10, Screen.width, HUD.Height), LangHelper.GetInstance().GetString("ScoreLabel") + (int)(playerScore * 100));

        if (IsExit)
            PauseMenuGUI();
    }

    void PauseMenuGUI()
    {
        GUI.skin = guiSkin;

        guiSkin.label.fontSize = HUD.LabelSize;
        guiSkin.button.fontSize = HUD.ButtonSize;

        GUI.Box(new Rect(10, 10, Screen.width - 20, Screen.height - 20), LangHelper.GetInstance().GetString("OptionLabel"), GUI.skin.GetStyle("box"));

        if (GUI.Button(new Rect(HUD.Left, HUD.GetPositionTop(), HUD.Width, HUD.Height), LangHelper.GetInstance().GetString("BackButton"), GUI.skin.GetStyle("button")))
            IsExit = false;

        if (GUI.Button(new Rect(HUD.Left, HUD.GetPositionTop(2), HUD.Width, HUD.Height), LangHelper.GetInstance().GetString("SoundButton") + (SoundEffectsHelper.Instance.HasSound ? "On" : "Off"), GUI.skin.GetStyle("button")))
            SoundEffectsHelper.Instance.SetSound();

        if (GUI.Button(new Rect(HUD.Left, HUD.GetPositionTop(3), HUD.Width, HUD.Height), LangHelper.GetInstance().GetString("MusicButton") + (SoundEffectsHelper.Instance.HasMusic ? "On" : "Off"), GUI.skin.GetStyle("button")))
            SoundEffectsHelper.Instance.SetMusic();

        if (GUI.Button(new Rect(HUD.Left, HUD.GetPositionTop(4), HUD.Width, HUD.Height), LangHelper.GetInstance().GetString("ExitButton"), GUI.skin.GetStyle("button")))
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

    public static float GetPositionTop(int multiplier = 1)
    {
		float result = (Height * multiplier);

		result = (result * 1.3f) + 20;

		return result;
    }
}
