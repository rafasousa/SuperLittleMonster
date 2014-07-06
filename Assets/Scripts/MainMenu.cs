using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public GUISkin guiSkin;

    private delegate void GUIMethod();

    private GUIMethod currentGUIMethod;

    void Start()
    {
        this.currentGUIMethod = MainMenuGUI;
    }

    public void MainMenuGUI()
    {
        GUI.skin = this.guiSkin;

        GUI.BeginGroup(new Rect(80, 70, Screen.width - 200, Screen.height - 80));
        {
            GUI.Box(new Rect(0, 0, Screen.width - 200, Screen.height - 80), Main.GameName, GUI.skin.GetStyle("box"));

            // First button
            if (GUI.Button(new Rect(10, 50, Screen.width - 220, 40), "Play", GUI.skin.GetStyle("button")))
            {
                Application.LoadLevel("Level");
            }

            if (GUI.Button(new Rect(10, 100, Screen.width - 220, 40), "Options", GUI.skin.GetStyle("button")))
            {
                // go to next menu
                this.currentGUIMethod = OptionsMenuGUI;
            }

            if (GUI.Button(new Rect(10, 150, Screen.width - 220, 40), "Exit", GUI.skin.GetStyle("button")))
            {
                Application.Quit();
            }
        }
        GUI.EndGroup();
    }

    public void OptionsMenuGUI()
    {
        GUI.skin = this.guiSkin;

        GUI.BeginGroup(new Rect(80, 70, Screen.width - 200, Screen.height - 80));
        {
            GUI.Box(new Rect(0, 0, Screen.width - 200, Screen.height - 80), Main.GameName, GUI.skin.GetStyle("box"));

            if (GUI.Button(new Rect(10, 50, Screen.width - 220, 40), "Sound", GUI.skin.GetStyle("button")))
            {
                //doing sometinhg here
            }

            if (GUI.Button(new Rect(10, 100, Screen.width - 220, 40), "Music", GUI.skin.GetStyle("button")))
            {
                //doing sometinhg here
            }

            if (GUI.Button(new Rect(10, 150, Screen.width - 220, 40), "Menu", GUI.skin.GetStyle("button")))
            {
                this.currentGUIMethod = MainMenuGUI;
            }
        }
        GUI.EndGroup();
    }

    // Update is called once per frame
    public void OnGUI()
    {
        this.currentGUIMethod();
    }
}