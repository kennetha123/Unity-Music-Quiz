using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Get the main canvas.
    public GameObject masterCanvas;
    // Get the gameplay canvas.
    public GameObject gameplayCanvas;
    // This will be spawned into master canvas.
    public GameObject playlistImageObject;
    // We can add more playlist here.
    public List<PlaylistData> playlistData;

    // ID that store playlist currently played.
    public int playlistIDPlayed;

    // Sent information animation to parameter through this var.
    public List<Animator> animatorEventCallback;

    public GameObject buttonContinue;
    public bool startGame;
    public TextMeshProUGUI countdown;
    private void Start()
    {
        Init();

        for (int i = 0; i < playlistData.Count; i++)
        {
            GameObject a = Instantiate(playlistImageObject, masterCanvas.transform.GetChild(0).GetChild(0));
            a.GetComponent<PlaylistObject>().playlistData = playlistData[i];
        }
    }

    public void Init()
    {
        masterCanvas.GetComponent<Canvas>().enabled = true;
        gameplayCanvas.SetActive(false);
        countdown.enabled = false;

    }

    public void OnChoosePlaylist()
    {
        for (int i = 0; i < animatorEventCallback.Count; i++)
        {
            animatorEventCallback[i].SetBool("PlaylistPicked", true);
        }
        buttonContinue.SetActive(true);
    }

    public void PlayGame()
    {
        masterCanvas.transform.GetChild(0).GetComponent<Animator>().SetBool("IsPlay", true);
        StartCoroutine(StartPlayQuiz(1f));
    }

    IEnumerator StartPlayQuiz(float timer)
    {
        gameplayCanvas.SetActive(true);
        countdown.enabled = true;

        countdown.text = "3";
        yield return new WaitForSeconds(1f);
        countdown.text = "2";
        yield return new WaitForSeconds(1f);
        countdown.text = "1";

        yield return new WaitForSeconds(timer);
        countdown.enabled = false;
        masterCanvas.GetComponent<Canvas>().enabled = false;
        FindObjectOfType<GameplayManager>().LoadQuestion();
    }
}
