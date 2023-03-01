using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GetReadyTimer : MonoBehaviour
{
    [SerializeField] GameObject getReadyCanvas;
    [SerializeField] GameObject gameCanvas;
    [SerializeField] Image timerImage;
    [SerializeField] TextMeshProUGUI timerCount;

    [SerializeField] float gettingReadyTime = 3f;
    float fillFraction;
    float timerValue;


    void Start()
    {
        timerValue = gettingReadyTime;
    }

    void Update()
    {
        timerValue -= Time.deltaTime;
        fillFraction = timerValue / gettingReadyTime;
        timerImage.fillAmount = fillFraction;
        timerCount.text = Mathf.CeilToInt(timerValue).ToString();
        
        if(timerValue <= 0)
        {
            getReadyCanvas.SetActive(false);
            gameCanvas.SetActive(true);
        }
    }
}
