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

        var style = GUI.skin.GetStyle("box");
        style.fontSize = 52;

        GUI.Box(new Rect(10, 10, Screen.width - 20, Screen.height - 20), "Super Little Monster", style);

        if (GUI.Button(new Rect(Screen.width / 2 - 100, 120, 200, 40), "Play", GUI.skin.GetStyle("button")))
            Application.LoadLevel("Level");

        if (GUI.Button(new Rect(Screen.width / 2 - 100, 180, 200, 40), "Options", GUI.skin.GetStyle("button")))
            this.currentGUIMethod = OptionsMenuGUI;

        if (GUI.Button(new Rect(Screen.width / 2 - 100, 240, 200, 40), "Exit", GUI.skin.GetStyle("button")))
            Application.Quit();
    }

    public void OptionsMenuGUI()
    {
        GUI.skin = this.guiSkin;

        GUI.Box(new Rect(10, 10, Screen.width - 20, Screen.height - 20), "Options", GUI.skin.GetStyle("box"));

        if (GUI.Button(new Rect(Screen.width / 2 - 100, 120, 200, 40), "Sound - " + (SoundEffectsHelper.Instance.IsMute ? "Off" : "On"), GUI.skin.GetStyle("button")))
            SoundEffectsHelper.Instance.SetMute();

        if (GUI.Button(new Rect(Screen.width / 2 - 100, 180, 200, 40), "Music - " + (SoundEffectsHelper.Instance.IsSound ? "Off" : "On"), GUI.skin.GetStyle("button")))
            SoundEffectsHelper.Instance.SetSound();

        if (GUI.Button(new Rect(Screen.width / 2 - 100, 240, 200, 40), "Back", GUI.skin.GetStyle("button")))
            this.currentGUIMethod = MainMenuGUI;
    }

    // Update is called once per frame
    public void OnGUI()
    {
        this.currentGUIMethod();
    }
}