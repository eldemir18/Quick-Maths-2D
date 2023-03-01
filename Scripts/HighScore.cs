using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI[] names;
    [SerializeField] TextMeshProUGUI[] highScores;


    void Start()
    {
        for(int i = 0; i < 5; i++)
        {
            names[i].text = PlayerPrefs.GetString("Name" + i.ToString(), "-");
            highScores[i].text = "LEVEL " +  PlayerPrefs.GetInt("Highscore" + i.ToString(), 0).ToString();
        }
    }

}
