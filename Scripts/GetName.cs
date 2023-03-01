using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetName : MonoBehaviour
{
    [SerializeField] TMP_InputField textBox;
    [SerializeField] Button nextButton;
    string upperText;

    void Update()
    {
        if(textBox.text.Length < 3)
        {
            nextButton.interactable = false;
        }
        else
        {
            nextButton.interactable = true;
        }

        upperText = textBox.text.ToUpper();
        if(upperText != textBox.text)
        {
            textBox.text = upperText;
        }         
    }

    public void SetName()
    {
        PlayerPrefs.SetString("Name" + Quiz.record.ToString(), upperText);
    }
}
