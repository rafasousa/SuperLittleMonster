using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour
{   
    public bool pause = false;
    
    public GUISkin guiSkin;

    public float playerCoin = 0;

    public float playerScore = 0;

    public int playerMonster = 0;

    private delegate void GUIMethod();

    private GUIMethod currentGUIMethod;

    void Update()
    {
        playerScore += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Escape))
            pause = !pause;

        if (pause)
            Time.timeScale = 0.00001f;
        else
            Time.timeScale = 1f;
    }

    public void IncreaseScore(int amount)
    {
        playerScore += amount;

        Main.PlayerScoreFinal = playerScore;
    }

    public void IncreaseCoin(int amount)
    {
        playerCoin += amount;

        Main.PlayerCoinFinal = playerCoin;
    }

    public void IncreaseMonster(int amount)
    {
        playerMonster += amount;
    }

    void OnGUI()
    {
        GUI.skin = guiSkin;

        //GUI.Label(new Rect(5, 10, 350, 550), Resources.Load("shadow-coin") as Texture);
        //GUI.Label(new Rect(10, 25, 50, 50), Resources.Load("coin") as Texture);
        //GUI.Label(new Rect(55, 45, 20, 20), Resources.Load("x") as Texture);

        var guiStyle = new GUIStyle(GUI.skin.GetStyle("label"));
        guiStyle.normal.textColor = Color.yellow;

        //PlayerCoin
        GUI.Label(new Rect(10, 10, 350, 100), "Coins: " + playerCoin.ToString(), guiStyle);

        //Score
        GUI.Label(new Rect(Screen.width - 170, 10, 300, 70), "Score: " + (int)(playerScore * 100));

        //Pouse
        // || GUI.Button(new Rect(Camera.main.pixelWidth - 80, 10, 65, 65), Resources.Load("pouse") as Texture)
        if (pause)
        {   
            GUI.BeginGroup(new Rect(80, 70, Screen.width - 200, Screen.height - 80));
            {
                GUI.Box(new Rect(0, 0, Screen.width - 200, Screen.height - 80), Main.GameName, GUI.skin.GetStyle("box"));

                if (GUI.Button(new Rect(10, 50, Screen.width - 220, 40), "Back", GUI.skin.GetStyle("button")))
                {
                    pause = false;
                }

                if (GUI.Button(new Rect(10, 100, Screen.width - 220, 40), "Exit", GUI.skin.GetStyle("button")))
                {
                    Application.LoadLevel("Menu");
                }
            }
            GUI.EndGroup();
        }
    }
}
