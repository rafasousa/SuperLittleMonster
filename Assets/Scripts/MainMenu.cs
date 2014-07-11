using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public GUISkin guiSkin;

    private delegate void GUIMethod();

    private GUIMethod currentGUIMethod;

    void Start()
    {
        SoundEffectsHelper.Instance.MakeBackgroundSound(SoundType.Menu);

        this.currentGUIMethod = MainMenuGUI;
    }

    public void MainMenuGUI()
    {
        GUI.skin = this.guiSkin;

        GUI.Box(new Rect(10, 10, Screen.width - 20, Screen.height - 20), "Super Little Monster", GUI.skin.GetStyle("box"));

        if (GUI.Button(new Rect(Screen.width / 2 - 100, 80, 200, 40), "Play", GUI.skin.GetStyle("button")))
            Application.LoadLevel("Level");

        if (GUI.Button(new Rect(Screen.width / 2 - 100, 140, 200, 40), "Options", GUI.skin.GetStyle("button")))
            this.currentGUIMethod = OptionsMenuGUI;

        if (GUI.Button(new Rect(Screen.width / 2 - 100, 200, 200, 40), "Exit", GUI.skin.GetStyle("button")))
            Application.Quit();
    }

    public void OptionsMenuGUI()
    {
        GUI.skin = this.guiSkin;

        GUI.Box(new Rect(10, 10, Screen.width - 20, Screen.height - 20), "Options", GUI.skin.GetStyle("box"));

        if (GUI.Button(new Rect(Screen.width / 2 - 100, 80, 200, 40), "Sound - " + (SoundEffectsHelper.Instance.IsMute ? "Off" : "On"), GUI.skin.GetStyle("button")))
            SoundEffectsHelper.Instance.SetMute();

        if (GUI.Button(new Rect(Screen.width / 2 - 100, 140, 200, 40), "Menu", GUI.skin.GetStyle("button")))
            this.currentGUIMethod = MainMenuGUI;
    }

    // Update is called once per frame
    public void OnGUI()
    {
        this.currentGUIMethod();
    }
}