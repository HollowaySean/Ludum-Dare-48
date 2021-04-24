using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{

    private int score = 0;
    public int Score { get { return score; } }
    
    public void IncrementScore() {
        score++;
        Debug.Log("The score is now: " + score.ToString());
    }
}
