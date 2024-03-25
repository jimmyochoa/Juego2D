using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public int score;
    public Text textScore;
    public void IncreaseScore()
    {
        score++;
        textScore.text = "Score: " + score;
    }
}
