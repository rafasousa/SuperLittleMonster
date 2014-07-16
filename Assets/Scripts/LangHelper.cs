using System.Collections;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

public class LangHelper
{
    private Hashtable Strings;

    public static LangHelper Instance;

    public LangHelper()
    {
        Strings = new Hashtable();

        SetLanguage();
    }

    public static LangHelper GetInstance()
    {
        if (Instance == null)
            Instance = new LangHelper();

        return Instance;
    }

    public void SetLanguage()
    {
        if (Strings == null || Strings.Count == 0)
        {
            var language = PlayerPrefs.GetString("Language", "English");

            var xmlLang = Resources.Load("lang") as TextAsset;

            var xElement = XElement.Parse(xmlLang.text);

            var elements = xElement.Element(language).Elements().ToList();

            elements.ForEach(element => Strings.Add(element.Attribute("name").Value, element.Value));
        }
    }

    public string GetString(string name)
    {
        if (!Instance.Strings.ContainsKey(name))
        {
            Debug.LogError("The specified string does not exist: " + name);

            return "";
        }

        return Instance.Strings[name] as string;
    }
}
