using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class Localization : MonoBehaviour
{
    public string tableReference;
    public string key;
    //public Locale targetLocal;

    private LocalizedString localizedString;
    private Text textComp;

    // Start is called before the first frame update
    void Start()
    {
        textComp = GetComponent<Text>();
        localizedString = new LocalizedString{TableReference = tableReference, TableEntryReference = key};

        LocalizationSettings.SelectedLocaleChanged += UpdateText;

    }

    void UpdateText(Locale locale)
    {
        textComp = GetComponent<Text>();
        textComp.text = localizedString.GetLocalizedString();
    }

    void OnDisable()
    {
        LocalizationSettings.SelectedLocaleChanged -= UpdateText;
    }

    void OnEnable()
    {
        LocalizationSettings.SelectedLocaleChanged += UpdateText;
    }


    
}
