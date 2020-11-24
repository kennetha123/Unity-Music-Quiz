using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/Playlist")]
public class PlaylistData : ScriptableObject
{
    public string playlistName;
    public int playlistID;
    public QuizQuestionData[] quizList;
    public bool shufflePlaylist;
}
