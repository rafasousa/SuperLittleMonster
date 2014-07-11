using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour
{
    public static bool pause = false;

    public GUISkin guiSkin;

    public float playerCoin = 0;

    public float playerScore = 0;

    public int playerMonster = 0;

    private bool hasSound;

    private delegate void GUIMethod();

    private GUIMethod currentGUIMethod;

    void Start()
    {
        Debug.Log("HUD - PlayerPrefs.GetString: " + PlayerPrefs.GetString("IsMute"));

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

        GUI.Label(new Rect(10, 10, 350, 100), "Coins: " + playerCoin, guiStyle);

        GUI.Label(new Rect(Screen.width - 170, 10, 300, 70), "Score: " + (int)(playerScore * 100));

        if (pause)
        {
            GUI.Box(new Rect(10, 10, Screen.width - 20, Screen.height - 20), "", GUI.skin.GetStyle("box"));

            if (GUI.Button(new Rect(Screen.width / 2 - 100, 80, 200, 40), "Back", GUI.skin.GetStyle("button")))
                pause = false;

            if (GUI.Button(new Rect(Screen.width / 2 - 100, 140, 200, 40), "Sound - " + (SoundEffectsHelper.Instance.IsMute ? "Off" : "On"), GUI.skin.GetStyle("button")))
                SoundEffectsHelper.Instance.SetMute();

            if (GUI.Button(new Rect(Screen.width / 2 - 100, 200, 200, 40), "Exit", GUI.skin.GetStyle("button")))
                Application.LoadLevel("Menu");
        }
    }

    public void OnDisable()
    {
        PlayerPrefs.SetInt("Score", (int)(playerScore * 100));

        PlayerPrefs.SetInt("Coin", (int)playerCoin);
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
