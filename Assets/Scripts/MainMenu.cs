using UnityEngine;
using System.Linq;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public GUISkin guiSkin;

    private delegate void GUIMethod();

    private GUIMethod currentGUIMethod;

    private Vector2 scrollViewVector = Vector2.zero;

    private string[] languages = { "English", "Portuguese" };

    private int index, wichLanguage;

    private bool showDropLanguages;

    void Start()
    {
        showDropLanguages = false; index = 0;
        var language = PlayerPrefs.GetString("Language");
        wichLanguage = languages.ToList().IndexOf(language);
        SoundEffectsHelper.Instance.MakeBackgroundSound(SoundType.Menu);
        this.currentGUIMethod = MainMenuGUI;
    }

    public void MainMenuGUI()
    {
        GUI.skin = this.guiSkin;

        guiSkin.label.fontSize = HUD.LabelSize;
        guiSkin.button.fontSize = HUD.ButtonSize;

        if (Input.GetKeyDown(KeyCode.Escape))
            this.currentGUIMethod = ExitMenuGUI;

        var style = GUI.skin.GetStyle("box");
        style.fontSize = HUD.TitleSize;

        GUI.Box(new Rect(10, 10, Screen.width - 20, Screen.height - 20), LangHelper.GetInstance().GetString("AppName"), style);

        if (GUI.Button(new Rect(HUD.Left, HUD.GetPositionTop(), HUD.Width, HUD.Height), LangHelper.GetInstance().GetString("PlayButton")))
            Application.LoadLevel("Level");

        if (GUI.Button(new Rect(HUD.Left, HUD.GetPositionTop(2), HUD.Width, HUD.Height), LangHelper.GetInstance().GetString("OptionButton")))
            this.currentGUIMethod = OptionsMenuGUI;

        if (GUI.Button(new Rect(HUD.Left, HUD.GetPositionTop(3), HUD.Width, HUD.Height), LangHelper.GetInstance().GetString("ExitButton")))
            this.currentGUIMethod = ExitMenuGUI;

        var bestScore = PlayerPrefs.GetInt("BestScore");
        var guiStyleScore = new GUIStyle(GUI.skin.GetStyle("label"));
        guiStyleScore.normal.textColor = Color.yellow;

        var totalCoin = PlayerPrefs.GetInt("TotalCoins", 0);
        var guiStyleCoin = new GUIStyle(GUI.skin.GetStyle("label"));
        guiStyleCoin.normal.textColor = Color.yellow;

		GUI.Label(new Rect(20, Screen.height - guiStyleScore.lineHeight - 20, Screen.width, Screen.height), LangHelper.GetInstance().GetString("BestScoreLabel") + bestScore, guiStyleScore);
		GUI.Label(new Rect(Screen.width - (Screen.width / 3.5f), Screen.height - guiStyleCoin.lineHeight - 20, Screen.width, 100), LangHelper.GetInstance().GetString("TotalCoinsLabel") + totalCoin, guiStyleCoin);
    }

    public void OptionsMenuGUI()
    {
        GUI.skin = this.guiSkin;

        GUI.Box(new Rect(10, 10, Screen.width - 20, Screen.height - 20), LangHelper.GetInstance().GetString("OptionLabel"), GUI.skin.GetStyle("box"));

        if (GUI.Button(new Rect(HUD.Left, HUD.GetPositionTop(), HUD.Width, HUD.Height), LangHelper.GetInstance().GetString("SoundButton") + (SoundEffectsHelper.Instance.HasSound ? "On" : "Off"), GUI.skin.GetStyle("button")))
            SoundEffectsHelper.Instance.SetSound();

        if (GUI.Button(new Rect(HUD.Left, HUD.GetPositionTop(2), HUD.Width, HUD.Height), LangHelper.GetInstance().GetString("MusicButton") + (SoundEffectsHelper.Instance.HasMusic ? "On" : "Off"), GUI.skin.GetStyle("button")))
            SoundEffectsHelper.Instance.SetMusic();

        if (GUI.Button(new Rect(HUD.Left, HUD.GetPositionTop(3), HUD.Width, HUD.Height), LangHelper.GetInstance().GetString("LanguageLabel")))
            if (!showDropLanguages) showDropLanguages = true; else showDropLanguages = false;

        if (showDropLanguages)
        {
			scrollViewVector = GUI.BeginScrollView(new Rect(HUD.Left + HUD.Width - 20, HUD.GetPositionTop(3), HUD.Width / 4,  HUD.Height), scrollViewVector, new Rect(0, 0, HUD.Width / 5.5f, (HUD.Height * languages.Length)));

            for (index = 0; index < languages.Length; index++)
            {
				if (GUI.Button(new Rect(0, index * HUD.Height, HUD.Width / 5, HUD.Height), ""))
                {
                    showDropLanguages = false; wichLanguage = index;
                    PlayerPrefs.SetString("Language", languages[index]);
                    LangHelper.GetInstance().ChangeLanguage();
                }

				GUI.Label(new Rect(12, index * HUD.Height, HUD.Width, HUD.Height), Resources.Load(languages[index]) as Texture);
            }
            GUI.EndScrollView();
        }
        else
			GUI.Label(new Rect(HUD.Left + HUD.Width - 10, HUD.GetPositionTop(3), HUD.Width, HUD.Height), Resources.Load(languages[wichLanguage]) as Texture);

        if (GUI.Button(new Rect(HUD.Left, HUD.GetPositionTop(4), HUD.Width, HUD.Height), LangHelper.GetInstance().GetString("BackButton"), GUI.skin.GetStyle("button")))
            this.currentGUIMethod = MainMenuGUI;
    }

    public void ExitMenuGUI()
    {
        GUI.skin = this.guiSkin;
        
		GUI.Box(new Rect(10, 10, Screen.width - 20, Screen.height - 20), LangHelper.GetInstance().GetString("ConfirmMessageExit"), GUI.skin.GetStyle("box"));

        if (GUI.Button(new Rect(HUD.Left + 20, HUD.GetPositionTop(2), HUD.Width / 2, HUD.Height), LangHelper.GetInstance().GetString("ConfirmExitButton"), GUI.skin.GetStyle("button")))
        {
            PlayerPrefs.Save();
            Application.Quit();
        }

		if (GUI.Button(new Rect((HUD.Left + HUD.Left) - 20, HUD.GetPositionTop(2), HUD.Width / 2, HUD.Height), LangHelper.GetInstance().GetString("CancelExitButton"), GUI.skin.GetStyle("button")))
            this.currentGUIMethod = MainMenuGUI;
    }

    // Update is called once per frame
    public void OnGUI()
    {
        this.currentGUIMethod();
    }
}