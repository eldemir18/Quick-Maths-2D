using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    public static Quiz instance;

    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] TextMeshProUGUI questionCount;
    Question question;
    public static int level;
    public static int record;
    bool isNewRecord = false;

    [Header("Answers")]
    [SerializeField] GameObject[] answers;
    int correctAnswerIndex;
    bool hasAnswered;
    
    [Header("Button Colors")]
    [SerializeField] Sprite spriteDefaultAnswer;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    [SerializeField] TextMeshProUGUI timerCount;
    Timer timer;

    [Header("Progress Bar")]
    [SerializeField] Slider progressBar;

    [Header("GameManager")]
    GameManager gameManager;
    bool isAnswerWrong;

    void Awake() 
    {
        timer = FindObjectOfType<Timer>();
        question = FindObjectOfType<Question>(); 
        gameManager = FindObjectOfType<GameManager>();
    }

    void Start()
    {
        level = -1;
        record = -1;
        progressBar.maxValue = question.GetFactor();
        progressBar.value = 1;

        isAnswerWrong = false;
        GetNextQuestion();
    }

    void Update()
    {
        if(timer.loadNextQuestion)
        {
            timerCount.text = 0.ToString();
            if(isAnswerWrong) 
            {
                for(int i = 0; i < 5; i++)
                {
                    if(level > PlayerPrefs.GetInt("Highscore" + i.ToString(), 0) && !isNewRecord)
                    {
                        isNewRecord = true;

                        string tempName;
                        int tempScore; 

                        // Shift the scores
                        for(int j = 4; j >= i; j--)
                        {
                            tempName = PlayerPrefs.GetString("Name" + j.ToString(), "-");
                            tempScore = PlayerPrefs.GetInt("Highscore" + j.ToString(), 0);

                            PlayerPrefs.SetString("Name" + (j+1).ToString(), tempName);
                            PlayerPrefs.SetInt("Highscore" + (j+1).ToString(), tempScore);
                        }

                        PlayerPrefs.SetInt("Highscore" + i.ToString(), level);
                        record = i;
                        break;
                    }
                }

                if(isNewRecord)
                {
                    gameManager.LoadNewRecord();
                }
                else
                {
                    gameManager.LoadEndScene();
                }
            }
            else
            {
                hasAnswered = false;
                timer.loadNextQuestion = false;
                GetNextQuestion();
            }
        }
        else if(!hasAnswered && !timer.isAnsewringQuestion)
        {
            timerCount.text = 0.ToString();
            DisplayAnswer(-1);
            SetButtonState(false);
        }
        else
        {
            timerImage.fillAmount = timer.fillFraction;
            timerCount.text = (Mathf.CeilToInt(timer.GetTimerValue())).ToString();
            questionCount.text = "LEVEL " + level.ToString();
        }
    }

    void GetNextQuestion()
    {
        level++;   
        timer.UpdateTimeForQuestion(level);
        SetButtonState(true);
        DisplayQuestion();
    }



    private void DisplayQuestion()
    {
        questionText.text = question.GetQuestion(level);
        for (int i = 0; i < answers.Length; i++)
        {
            TextMeshProUGUI buttonText = answers[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.GetAnswer(i);

            Image buttonImage = answers[i].GetComponent<Image>();
            buttonImage.color = Color.white;
        }
    }

    private void SetButtonState(bool state)
    {
        for(int i = 0; i < answers.Length; i++)
        {
            Button button = answers[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    public void OnAnswerSelected(int index)
    {
        hasAnswered = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
    }


    private void DisplayAnswer(int index)
    {
        correctAnswerIndex = question.GetCorrectAnswerIndex();
        Image buttonImage = answers[correctAnswerIndex].GetComponent<Image>();
        buttonImage.color = new Color(0, 200, 0);
        
        if(index == correctAnswerIndex)
        {
            questionText.text = "CORRECT!";
            progressBar.value = (progressBar.value < 5) ? (++progressBar.value) : 1;
        }
        else if(index >= 0 && index <= 3)
        {
            questionText.text = "WRONG!";
            buttonImage = answers[index].GetComponent<Image>();
            buttonImage.color = new Color(200,0,0);
            isAnswerWrong = true;
        }
        else
        {
            questionText.text = "TIME IS UP!";
            isAnswerWrong = true;
            hasAnswered = true;
        }
    }
}
