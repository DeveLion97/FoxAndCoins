using UnityEngine;

public class QuizManager : MonoBehaviour
{
    public static QuizManager instance;
    public Question[] questions;
    [HideInInspector]public int currentQuestionIndex = 0;
    public AudioClip questionBoxMusic;
    public QuizView quizView;
    private void Awake()
    {
        instance = this;
    }
    public void ShowQuiz()
    {
        if (!quizView.gameObject.activeSelf)
        {
            quizView.gameObject.SetActive(true);
            Time.timeScale = 0.000001f;
        }


    }

    public Question GetCurrentQuestion()
    {
        if (currentQuestionIndex < questions.Length)
            return questions[currentQuestionIndex];
        else
            return null;
    }

    public bool AnswerSelected(int selectedOption)
    {
        if (selectedOption == GetCurrentQuestion().correctOptionIndex)
        {
            Debug.Log("Correct!");
          
            return true;
            // Handle correct answer logic here
        }
        else
        {
            Debug.Log("Incorrect!");
            return false;
            // Handle incorrect answer logic here
        }
        // Move to the next question
    }
}
