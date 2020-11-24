using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class GameplayManager : MonoBehaviour
{
    // The quiz multi choice.
    public List<GameObject> multiChoice;
    public GameManager gameManager;
    public Image imageQuestion;
    public AudioSource audioSource;

    public int currentQuestionNumber;

    public AudioClip rightAnswer;
    public AudioClip wrongAnswer;

    public GameObject resultPage;
    public TextMeshProUGUI resultScoreUI;
    public int resultScore;

    private int score;
    public AudioClip endClip;

    public bool[] keyScore;
    public List<Image> keyAnswerImage;

    public GameObject endingImage;
    private void Start()
    {
        Init();
    }

    private void Update()
    {
    }

    private void Init()
    {
        if (gameManager == null)
            gameManager = FindObjectOfType<GameManager>();
        if (imageQuestion == null)
            Debug.LogError("Missing Image UI for displaying song image!");
        if (audioSource == null)
            Debug.LogError("Missing Audio Source!");
        if (multiChoice.Count == 0)
            Debug.LogError("Button for multiple choice is not assigned.");

        resultPage.SetActive(false);
    }

    public void LoadQuestion()
    {
        Debug.Log("Current question number : " + currentQuestionNumber);
        Debug.Log("Playlist ID : " + gameManager.playlistIDPlayed);
        // Show Image
        imageQuestion.sprite = gameManager.playlistData[gameManager.playlistIDPlayed].quizList[currentQuestionNumber].answerPicture;
        // Run Audio
        audioSource.PlayOneShot(gameManager.playlistData[gameManager.playlistIDPlayed].quizList[currentQuestionNumber].answerSong);
        for (int i = 0; i < multiChoice.Count; i++)
        {
            multiChoice[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = gameManager.playlistData[gameManager.playlistIDPlayed].quizList[currentQuestionNumber].answerList[i].artist;
            multiChoice[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = gameManager.playlistData[gameManager.playlistIDPlayed].quizList[currentQuestionNumber].answerList[i].title;
        }
    }

    public void Answer(int answerChosen)
    {
        audioSource.Stop();

        //why? because the answer index start from 1.
        if (answerChosen == gameManager.playlistData[gameManager.playlistIDPlayed].quizList[currentQuestionNumber].answerIndex)
        {
            Debug.Log("You Right");
            StartCoroutine(DisplayAnswerResult(3f, answerChosen, true));
            score++;
        }
        else
        {
            Debug.Log("You Wrong");
            StartCoroutine(DisplayAnswerResult(3f, answerChosen, false));
        }
    }

    /// <summary>
    /// Wait for few second to display the answer is right or wrong.
    /// </summary>
    /// <param name="timer">how long it will be showned.</param>
    /// <param name="answerID">what is the answer ID.</param>
    /// <param name="answerIsTrue">it is correct answer?.</param>
    /// <returns></returns>
    IEnumerator DisplayAnswerResult(float timer,int answerID, bool answerIsTrue)
    {

        if (answerIsTrue)
        {
            audioSource.PlayOneShot(rightAnswer);
            multiChoice[answerID].transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = Color.green;
            multiChoice[answerID].transform.GetChild(2).GetComponent<TextMeshProUGUI>().color = Color.green;
            keyScore[currentQuestionNumber] = true;
        }
        else
        {
            audioSource.PlayOneShot(wrongAnswer);
            multiChoice[answerID].transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = Color.red;
            multiChoice[answerID].transform.GetChild(2).GetComponent<TextMeshProUGUI>().color = Color.red;
            multiChoice[gameManager.playlistData[gameManager.playlistIDPlayed].quizList[currentQuestionNumber].answerIndex].transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = Color.green;
            multiChoice[gameManager.playlistData[gameManager.playlistIDPlayed].quizList[currentQuestionNumber].answerIndex].transform.GetChild(2).GetComponent<TextMeshProUGUI>().color = Color.green;
        }
        yield return new WaitForSeconds(timer);
        currentQuestionNumber++;

        if (currentQuestionNumber >= gameManager.playlistData[gameManager.playlistIDPlayed].quizList.Length)
        {
            for (int i = 0; i < multiChoice.Count; i++)
            {
                multiChoice[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = Color.black;
                multiChoice[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().color = Color.black;
            }
            audioSource.Stop();
            ShowResult();
        }

        LoadQuestion();
        for (int i = 0; i < multiChoice.Count; i++)
        {
            multiChoice[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().color = Color.black;
            multiChoice[i].transform.GetChild(2).GetComponent<TextMeshProUGUI>().color = Color.black;
        }
    }

    private void ShowResult()
    {
        resultPage.SetActive(true);
        resultScore = score * 100 / gameManager.playlistData[gameManager.playlistIDPlayed].quizList.Length;
        resultScoreUI.text = resultScore.ToString();
        for (int i = 0; i < keyAnswerImage.Count; i++)
        {
            if (keyScore[i])
                keyAnswerImage[i].color = Color.green;
            else
                keyAnswerImage[i].color = Color.red;
        }
        audioSource.PlayOneShot(endClip);
    }

    public void EndGameContinue()
    {
        //gameManager.Init();
        currentQuestionNumber = 0;
        resultScore = 0;
        score = 0;
        StartCoroutine(WaitEnd());
    }

    IEnumerator WaitEnd()
    {
        endingImage.SetActive(true);
        gameManager.gameplayCanvas.transform.GetChild(0).GetComponent<Animator>().SetBool("Out", true);
        yield return new WaitForSeconds(1f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
