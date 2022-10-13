using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TranslationAlert : MonoBehaviour
{
    public TextMeshPro DescriptionText;
    public bool TranslationStatus = false;
    public Button Translate;

    // Start is called before the first frame update
    void Start()
    {
        Translate.onClick.AddListener(TaskOnClick);
    }

    private void TaskOnClick()
    {
        private string TranslateTrue = "Translation has been turned on";
        private string TranslateFalse = "Translation has been turned off";
        if (TranslationStatus)
        {
            DescriptionText.text = TranslateFalse;
            TranslationStatus = false;
        }
        /*else
        {
            DescriptionText.text = TranslateFalse;
            TranslationStatus = false;
        }*/
    }
}