using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlaylistObject : MonoBehaviour
{
    private TextMeshProUGUI textObj;
    public PlaylistData playlistData;
    private GameManager gameManager;

    private GameObject targetMovePos;
    private float moveSpeed = 100f;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        targetMovePos = GameObject.FindGameObjectWithTag("TargetMovePos");
        textObj = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        textObj.text = playlistData.playlistName;
    }

    private void Update()
    {
        if (gameManager.startGame)

        if(gameManager.playlistIDPlayed != playlistData.playlistID && gameManager.startGame)
        {
            GetComponent<Animator>().SetBool("PlaylistPicked", true);
        }
        else if(gameManager.playlistIDPlayed == playlistData.playlistID && gameManager.startGame)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetMovePos.transform.position, moveSpeed * Time.deltaTime);
        }
    }

    public void OnGameStart()
    {
        // Set canvas to disabled when we clicking the playlist and open the scene of the gameplay.
        gameManager.playlistIDPlayed = playlistData.playlistID;
        gameManager.startGame = true;
        gameManager.OnChoosePlaylist();
    }
}
