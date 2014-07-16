using System.Collections;
using System.Linq;
using System.Xml;
//using System.Xml.Linq;
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
            var xml = new XmlDocument();

            xml.LoadXml(xmlLang.text);

            var el = xml.DocumentElement.SelectSingleNode(language);

            if (el != null)
            {
                foreach (XmlNode item in el.ChildNodes)
                    Strings.Add(item.Attributes["name"].Value, item.InnerText);
            }
        }
    }

    public void ChangeLanguage()
    {
        Strings = new Hashtable();

        SetLanguage();
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
