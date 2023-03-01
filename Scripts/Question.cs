using System.Collections.Generic;
using UnityEngine;

public class Question : MonoBehaviour
{

    private enum Operation
    {
        PLUS,
        MINUS,
        MULTIPLY,
    }

    List<string> answers;
    List<Operation> operations;
    Operation operation;

    int randomNumber1, randomNumber2, correctAnswerIndex, correctAnswer;
    int factor = 5;
    string question;

    void Start()
    {
        operations = new List<Operation>();
        for(int i = 0; i < 5; i++)
        {
            operations.Add(Operation.PLUS);
            operations.Add(Operation.MINUS);
        }
    }

    public string GetQuestion(int level)
    {
        if((level - 1) % factor == 0) operations.Add(Operation.MULTIPLY);

        answers = new List<string>(new string[4]);
        randomNumber1 = Random.Range(0 + (5 * Mathf.FloorToInt(level / factor)), 5 + (5 * Mathf.FloorToInt(level / factor)));
        randomNumber2 = Random.Range(0 + (5 * Mathf.FloorToInt(level / factor)), 5 + (5 * Mathf.FloorToInt(level / factor)));
        operation = operations[Random.Range(0,operations.Count)];
        correctAnswerIndex = Random.Range(0,4);

        switch (operation)
        {
            case Operation.PLUS:
                question = randomNumber1.ToString() + " + " + randomNumber2.ToString();
                correctAnswer = randomNumber1 + randomNumber2;
                break;
            case Operation.MINUS:
                question = randomNumber1.ToString() + " - " + randomNumber2.ToString();
                correctAnswer = randomNumber1 - randomNumber2;
                break;
            case Operation.MULTIPLY:
                question = randomNumber1.ToString() + " x " + randomNumber2.ToString();
                correctAnswer = randomNumber1 * randomNumber2;
                break;
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
