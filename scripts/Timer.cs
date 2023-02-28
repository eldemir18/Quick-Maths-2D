using UnityEngine;

public class Timer : MonoBehaviour
{

    [SerializeField] float timeForQuestion = 10f;
    [SerializeField] float timeForCorrectAnswer = 5f;
    public bool isAnsewringQuestion;
    public bool loadNextQuestion;
    public float fillFraction;

    Question question;

    float timerValue;

    void Start()
    {
        question = FindObjectOfType<Question>();
        timerValue = 0;
        isAnsewringQuestion = false;
        loadNextQuestion = false;
    }

    void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer()
    {
        timerValue = 0;
    }

    public float GetTimerValue()
    {
        return timerValue;
    }

    public void UpdateTimeForQuestion(int level)
    {
        timeForQuestion = 10.5f - 0.5f * (level / question.GetFactor() + 1);
    }

    private void UpdateTimer()
    {
        timerValue -= Time.deltaTime;

        if(timerValue > 0)
        {
            fillFraction = (isAnsewringQuestion) ? (timerValue / timeForQuestion) : (timerValue / timeForCorrectAnswer);
        }
        else
        {
            isAnsewringQuestion ^= true;
            if(isAnsewringQuestion)
            {
                timerValue = timeForQuestion;
                loadNextQuestion = true;
            }
            else
            {
                timerValue = timeForCorrectAnswer;
            }
        }
    }
}
