using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Quiz Question")]
public class QuizQuestionData : ScriptableObject
{
    public QuizData[] answerList = new QuizData[4];
    public int answerIndex;
    public Sprite answerPicture;
    public AudioClip answerSong;

    public void OnEnable()
    {
        for (int i = 0; i < answerList.Length; i++)
        {
            answerList[i].index = i;
        }
    }
}

[System.Serializable]
public struct QuizData
{
    public string title;
    public string artist;
    public int index;
}