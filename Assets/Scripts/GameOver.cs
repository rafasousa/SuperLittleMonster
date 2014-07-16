using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour
{
    int playerScore = 0;

    int playerCoin = 0;

    int bestScore = 0;

    public GUISkin guiSkin;

    void Start()
    {
        SoundEffectsHelper.Instance.MakeBackgroundSound(SoundType.GameOver);

        playerScore = PlayerPrefs.GetInt("Score", 0);

        playerCoin = PlayerPrefs.GetInt("Coin", 0);

        bestScore = PlayerPrefs.GetInt("BestScore", 0);

        if (playerScore > bestScore)
            PlayerPrefs.SetInt("BestScore", playerScore);
    }

    void OnGUI()
    {
        GUI.skin = guiSkin;

        var guiStyleScore = new GUIStyle(GUI.skin.GetStyle("label"));
        guiStyleScore.normal.textColor = Color.yellow;

        var guiStyleCoin = new GUIStyle(GUI.skin.GetStyle("label"));
        guiStyleCoin.normal.textColor = Color.yellow;

        GUI.Box(new Rect(10, 10, Screen.width - 20, Screen.height - 20), "GAME OVER", GUI.skin.GetStyle("box"));

        GUI.Box(new Rect(HUD.Left - 80, 70, 370, 120), "", GUI.skin.GetStyle("box"));

        GUI.Label(new Rect(HUD.Left - 30, 75, 350, HUD.Height), LangHelper.GetInstance().GetString("YourScoreLabel") + playerScore, guiStyleScore);

        GUI.Label(new Rect(HUD.Left - 30, 125, 350, HUD.Height), LangHelper.GetInstance().GetString("YourCoinLabel") + playerCoin, guiStyleCoin);


        if (GUI.Button(new Rect(HUD.Left, 200, HUD.Width, HUD.Height), LangHelper.GetInstance().GetString("RetryButton"), GUI.skin.GetStyle("button")))
            Application.LoadLevel("Level");

        if (GUI.Button(new Rect(HUD.Left, 280, HUD.Width, HUD.Height), LangHelper.GetInstance().GetString("ExitButton"), GUI.skin.GetStyle("button")))
            Application.LoadLevel("Menu");
    }
}
