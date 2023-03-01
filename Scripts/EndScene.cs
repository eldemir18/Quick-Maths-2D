using UnityEngine;
using TMPro;

public class EndScene : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI score;

    void Start()
    {
        score.text = "GAME OVER!\nLEVEL " + Quiz.level.ToString();
    }
}
