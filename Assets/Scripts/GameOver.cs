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

        //guiSkin.label.fontSize = HUD.LabelSize;
        //guiSkin.button.fontSize = HUD.ButtonSize;

        var guiStyleScore = new GUIStyle(GUI.skin.GetStyle("label"));
        guiStyleScore.normal.textColor = Color.yellow;

        var guiStyleCoin = new GUIStyle(GUI.skin.GetStyle("label"));
        guiStyleCoin.normal.textColor = Color.yellow;

        GUI.Box(new Rect(10, 10, Screen.width - 20, Screen.height - 20), "GAME OVER", GUI.skin.GetStyle("box"));
        
        GUI.Label(new Rect(HUD.Left - 30, HUD.GetPositionTop() - 20, Screen.width, HUD.Height), LangHelper.GetInstance().GetString("YourScoreLabel") + playerScore, guiStyleScore);

        GUI.Label(new Rect(HUD.Left - 30, HUD.GetPositionTop(2) - 60, Screen.width, HUD.Height), LangHelper.GetInstance().GetString("YourCoinLabel") + playerCoin, guiStyleCoin);


        if (GUI.Button(new Rect(HUD.Left, HUD.GetPositionTop(2), HUD.Width, HUD.Height), LangHelper.GetInstance().GetString("RetryButton"), GUI.skin.GetStyle("button")))
            Application.LoadLevel("Level");

        if (GUI.Button(new Rect(HUD.Left, HUD.GetPositionTop(3), HUD.Width, HUD.Height), LangHelper.GetInstance().GetString("ExitButton"), GUI.skin.GetStyle("button")))
            Application.LoadLevel("Menu");
    }
}