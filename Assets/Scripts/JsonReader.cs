using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Language
{
    public string lang;
    public string title;
    public string play;
    public string quit;
    public string options;
    public string credits;

}

public class LanguageData
{
    public Language[] languages;
}

public class JsonReader : MonoBehaviour
{
    public TextAsset jsonFile;
    public string currentLanguage;
    private LanguageData languageData;

    public Text title;
    public Text play;
    public Text quit;
    public Text options;
    public Text credits;
    // Start is called before the first frame update
    void Start()
    {
        languageData = JsonUtility.FromJson<LanguageData>(jsonFile.text);
        SetLanguage(currentLanguage);
    }

    public void SetLanguage(string newLang)
    {
        foreach(Language lang in languageData.languages)
        {
            if(lang.lang.ToLower() == newLang.ToLower())
            {
                title.text = lang.title;
                play.text = lang.play;
                quit.text = lang.quit;
                options.text = lang.options;
                credits.text = lang.credits;
            }
        }
    }

    public void SwapLanguage()
    {
        if(currentLanguage == fr)
        {
            currentLanguage = en;
        }
        else if(currentLanguage == en)
        {
            currentLanguage = fr;
        }
        SetLanguage(currentLanguage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
