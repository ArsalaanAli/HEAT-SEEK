using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;



public class General : MonoBehaviour
{
    public int score;
    public GameObject scoreHolder;
    public TextMeshProUGUI scoreText;




    private void Start() {
        scoreText = scoreHolder.GetComponent<TextMeshProUGUI>();
    }

    public void GameOver() {
        //Freezes Game
        Time.timeScale = 0.01f;

        //Code for GameOver UI

    }

    public void addScore() {
        score += 1;
        scoreText.SetText("Score: " + score);
        Debug.Log(score);
    }


}
