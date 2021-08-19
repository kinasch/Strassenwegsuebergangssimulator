using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private GameObject scoreTextObject, player;

    private int score = 0;
    private Text scoreText;
    private Rigidbody2D playerRigidbody2D;

    private void Start()
    {
        scoreText = scoreTextObject.GetComponent<Text>();
        playerRigidbody2D = player.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (playerRigidbody2D.position.x + 7.5f > score)
        {
            score = (int) Mathf.Round(playerRigidbody2D.position.x + 7.5f);
            UpdateScoreText();
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }
}
