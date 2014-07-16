﻿using UnityEngine;
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

        GUI.Box(new Rect(10, 10, Screen.width - 20, Screen.height - 20), LangHelper.GetInstance().GetString("AppName"), style);

        if (GUI.Button(new Rect(Screen.width / 2 - 100, 120, 200, 40), LangHelper.GetInstance().GetString("PlayButton"), GUI.skin.GetStyle("button")))
            Application.LoadLevel("Level");

        if (GUI.Button(new Rect(Screen.width / 2 - 100, 180, 200, 40), LangHelper.GetInstance().GetString("OptionButton"), GUI.skin.GetStyle("button")))
            this.currentGUIMethod = OptionsMenuGUI;

        if (GUI.Button(new Rect(Screen.width / 2 - 100, 240, 200, 40), LangHelper.GetInstance().GetString("ExitButton"), GUI.skin.GetStyle("button")))
        {
            PlayerPrefs.Save();

            Application.Quit();
        }

        var bestScore = PlayerPrefs.GetInt("BestScore");
        var guiStyle = new GUIStyle(GUI.skin.GetStyle("label"));
        guiStyle.normal.textColor = Color.yellow;

        GUI.Label(new Rect(Screen.width / 2 - 100, 300, 400, 40), LangHelper.GetInstance().GetString("BestScoreLabel") + bestScore, guiStyle);
    }

    public void OptionsMenuGUI()
    {
        GUI.skin = this.guiSkin;

        GUI.Box(new Rect(10, 10, Screen.width - 20, Screen.height - 20), LangHelper.GetInstance().GetString("OptionLabel"), GUI.skin.GetStyle("box"));

        if (GUI.Button(new Rect(Screen.width / 2 - 100, 120, 200, 40), LangHelper.GetInstance().GetString("SoundButton") + (SoundEffectsHelper.Instance.HasSound ? "On" : "Off"), GUI.skin.GetStyle("button")))
            SoundEffectsHelper.Instance.SetSound();

        if (GUI.Button(new Rect(Screen.width / 2 - 100, 180, 200, 40), LangHelper.GetInstance().GetString("MusicButton") + (SoundEffectsHelper.Instance.HasMusic ? "On" : "Off"), GUI.skin.GetStyle("button")))
            SoundEffectsHelper.Instance.SetMusic();

        if (GUI.Button(new Rect(Screen.width / 2 - 100, 240, 200, 40), LangHelper.GetInstance().GetString("BackButton"), GUI.skin.GetStyle("button")))
            this.currentGUIMethod = MainMenuGUI;
    }

    // Update is called once per frame
    public void OnGUI()
    {
        this.currentGUIMethod();
    }
}