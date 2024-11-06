using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class QuizView : MonoBehaviour
{
    public Text questionText;
    public ButtonOptions[] optionButtons;
    public GameObject treaseureBoxObj;
    public GameObject wrongAnsObj;
    private QuizManager quizManager;
    public Color correctAnsColor;
    public Color wrongAnsColor;
    private HashSet<int> displayedQuestions = new HashSet<int>();
    public Question currentQuestion;
    private bool hasOptionSelected = false;
    [Space]
    public GameObject wrongAnsPanel;
    public GameObject nothaveCoinsText;
    private void Start()
    {
        quizManager = QuizManager.instance;
        DisplayQuestion();
    }
    private void OnEnable()
    {
        if (quizManager)
            DisplayQuestion();
    }

    private void DisplayQuestion()
    {
        //SoundManager.PlayQuizMusic();
        currentQuestion = GetNextQuestion();
        if (currentQuestion != null)
        {
            hasOptionSelected = false;
            questionText.gameObject.SetActive(true);
            wrongAnsObj.gameObject.SetActive(false);
            treaseureBoxObj.gameObject.SetActive(false);
            wrongAnsPanel.SetActive(false);
            nothaveCoinsText.gameObject.SetActive(false);
            questionText.text = currentQuestion.questionText;

            for (int i = 0; i < currentQuestion.options.Length; i++)
            {
                optionButtons[i].buttonObj.SetActive(true);
                optionButtons[i].rightAnsTick.SetActive(false);
                optionButtons[i].wrongAnsCross.SetActive(false);
                optionButtons[i].optionText.text = currentQuestion.options[i];
            }

            // Hide remaining buttons if there are less options in the current question
            for (int i = currentQuestion.options.Length; i < optionButtons.Length; i++)
            {
                optionButtons[i].buttonObj.SetActive(false);
            }
        }
        else
        {
            Debug.LogWarning("No more questions!");
        }
    }

    private Question GetNextQuestion()
    {
        Question nextQuestion = null;
        while (nextQuestion == null && displayedQuestions.Count < quizManager.questions.Length)
        {
            int randomIndex = Random.Range(0, quizManager.questions.Length);
            if (!displayedQuestions.Contains(randomIndex))
            {
                nextQuestion = quizManager.questions[randomIndex];
                displayedQuestions.Add(randomIndex);
            }
        }
        return nextQuestion;
    }

    public void OptionSelected(int optionIndex)
    {
        if (hasOptionSelected)
            return;

        hasOptionSelected = true;
        bool isAnserTrue = currentQuestion.correctOptionIndex == optionIndex ? true : false;


        if (isAnserTrue)
        {
            GameManager.Instance.AddCoin(100);
            questionText.gameObject.SetActive(false);
            treaseureBoxObj.SetActive(true);

            for (int i = 0; i <= optionButtons.Length - 1; i++)
            {
                optionButtons[i].gameObject.gameObject.SetActive(false);

            }

            quizManager.currentQuestionIndex++;
            StartCoroutine(HidePanel());
        }
        else
        {
            wrongAnsPanel.SetActive(true);
        }

    }

    public void ShowAnsWithCoins()
    {
        if (GameManager.Instance.Coin >= 100)
        {
            GameManager.Instance.CreditCoin(100);
            RightAnswer();
         
        }
        else
        {
            if (!nothaveCoinsText.activeSelf)
            { 
                nothaveCoinsText.SetActive(true);
                StartCoroutine(HideText());
            }
        }
    }


    IEnumerator HideText()
    {
        yield return new WaitForSecondsRealtime(2);
      
            nothaveCoinsText.SetActive(false);
    }
    public void ShowWithRewardedAd()
    {
        GoogleAdMobController.Instance.ShowInterstitialAd(RightAnswer);
    }

    void RightAnswer()
    {
        wrongAnsPanel.SetActive(false);

        wrongAnsObj.SetActive(true);

        for (int i = 0; i <= optionButtons.Length - 1; i++)
        {
            if (currentQuestion.correctOptionIndex == i)
            {
                optionButtons[i].rightAnsTick.gameObject.SetActive(true);
                optionButtons[i].wrongAnsCross.gameObject.SetActive(false);
            }
            else
            {
                optionButtons[i].rightAnsTick.gameObject.SetActive(false);
                optionButtons[i].wrongAnsCross.gameObject.SetActive(true);
            }
        }

        quizManager.currentQuestionIndex++;
        StartCoroutine(HidePanel());
    }

    public void ShowGameFailPanel()
    {
       
        Time.timeScale = 1;
        nothaveCoinsText.SetActive(false);
        LevelManager.Instance.KillPlayer();
        wrongAnsPanel.SetActive(false);
        this.gameObject.SetActive(false);
    }

    IEnumerator HidePanel()
    {
        yield return new WaitForSecondsRealtime(2);
        //SoundManager.PlayMusic();
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }
}