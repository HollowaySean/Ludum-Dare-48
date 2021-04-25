using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCounter : MonoBehaviour
{

    private int score = 0;
    public int Score { get { return score; } set { score = value; } }

    public Animator animator;
    public GameObject lightbulb;
    public Transform lightbulbPivot;

    public Slider slider;
    
    public void IncrementScore() {
        score++;
        Debug.Log("The score is now: " + score.ToString());

        StartCoroutine(AnimationFlag());

        slider.value = score / 15f;
        
    }

    private IEnumerator AnimationFlag() {

        animator.SetBool("success", true);
        yield return new WaitForSeconds(2f);
        GameObject newLB = Instantiate(lightbulb, lightbulbPivot);
        newLB.transform.localPosition = Vector3.zero;
        animator.SetBool("success", false);
        yield return new WaitForSeconds(1f);
        Destroy(newLB);
    }
}
