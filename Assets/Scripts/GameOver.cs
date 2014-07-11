using UnityEngine;
using System.Collections;

public class GameOver : MonoBehaviour
{
    int playerScore = 0;

    int playerCoin = 0;

    public GUISkin guiSkin;


    void Start()
    {
        Debug.Log("GameOver - PlayerPrefs.GetString: " + PlayerPrefs.GetString("IsMute"));

        SoundEffectsHelper.Instance.MakeBackgroundSound(SoundType.GameOver);

        playerScore = PlayerPrefs.GetInt("Score");

        playerCoin = PlayerPrefs.GetInt("Coin");
    }

    void OnGUI()
    {
        GUI.skin = guiSkin;

        var guiStyleScore = new GUIStyle(GUI.skin.GetStyle("label"));
        guiStyleScore.normal.textColor = Color.blue;

        var guiStyleCoin = new GUIStyle(GUI.skin.GetStyle("label"));
        guiStyleCoin.normal.textColor = Color.yellow;

        GUI.Box(new Rect(10, 10, Screen.width - 20, Screen.height - 20), "GAME OVER", GUI.skin.GetStyle("box"));

        GUI.Label(new Rect(Screen.width / 2 - 100, 60, Screen.width - 40, Screen.height - 40), "Your Score: " + playerScore, guiStyleScore);

        GUI.Label(new Rect(Screen.width / 2 - 100, 100, Screen.width - 40, Screen.height - 40), "Your Coins: " + playerCoin, guiStyleCoin);

        if (GUI.Button(new Rect(Screen.width / 2 - 100, 160, 200, 40), "Retry?", GUI.skin.GetStyle("button")))
            Application.LoadLevel("Level");

        if (GUI.Button(new Rect(Screen.width / 2 - 100, 210, 200, 40), "Exit", GUI.skin.GetStyle("button")))
            Application.LoadLevel("Menu");
    }
}
