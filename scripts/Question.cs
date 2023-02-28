using System.Collections.Generic;
using UnityEngine;

public class Question : MonoBehaviour
{
    List<string> answers;
    List<string> operations;
    int randomNumber1, randomNumber2, correctAnswerIndex, correctAnswer;
    int factor = 5;
    string operation, question;

    void Start()
    {
        operations = new List<string>();
        for(int i = 0; i < 5; i++)
        {
            operations.Add("plus");
            operations.Add("minus");
        }
    }

    public string GetQuestion(int level)
    {
        if((level - 1) % factor == 0) operations.Add("multiply");

        answers = new List<string>(new string[4]);
        randomNumber1 = Random.Range(0 + (5 * Mathf.FloorToInt(level / factor)), 5 + (5 * Mathf.FloorToInt(level / factor)));
        randomNumber2 = Random.Range(0 + (5 * Mathf.FloorToInt(level / factor)), 5 + (5 * Mathf.FloorToInt(level / factor)));
        operation = operations[Random.Range(0,operations.Count)];
        correctAnswerIndex = Random.Range(0,4);

        if(operation == "plus")
        {
            question = randomNumber1.ToString() + " + " + randomNumber2.ToString();
            correctAnswer = randomNumber1 + randomNumber2;
        }
        else if(operation == "minus")
        {
            question = randomNumber1.ToString() + " - " + randomNumber2.ToString();
            correctAnswer = randomNumber1 - randomNumber2;
        }
        else if(operation == "multiply")
        {
            question = randomNumber1.ToString() + " x " + randomNumber2.ToString();
            correctAnswer = randomNumber1 * randomNumber2;
        }

        for(int i = 0; i < 4; i++)
        {
            if(i == correctAnswerIndex)
            {
                answers[i] = correctAnswer.ToString();
            }
            else
            {
                while(true)
                {
                    int temp = Random.Range(-5 * (level / factor + 1), 6 * (level / factor + 1));
                    if(!answers.Contains((correctAnswer + temp).ToString()) && temp != 0)
                    {
                        answers[i] = (correctAnswer + temp).ToString();
                        break;
                    }
                }
            }
        }

        return question;
    }

    public int GetCorrectAnswerIndex()
    {
        return correctAnswerIndex;
    }

    public string GetAnswer(int index)
    {
        return answers[index];
    }

    public int GetFactor()
    {
        return factor;
    }
}
