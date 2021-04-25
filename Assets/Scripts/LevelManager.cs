using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Image image;
    public GameObject button;
    public Player player;
    public Animator animator;
    public Target target;

    private bool isPaused = false;

    public TMPro.TextMeshPro text;

    private void Start() {

        FindObjectOfType<AudioManager>().Play("Music");

        StartCoroutine(StartupSequence());

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {

            // Toggle pause
            if (isPaused) {

                FindObjectOfType<AudioManager>().UnpauseAll();

                isPaused = false;
                Time.timeScale = 1.0f;

                button.SetActive(false);

                Color32 newColor = image.color;
                newColor.a = 0;
                image.color = newColor;

            } else {

                FindObjectOfType<AudioManager>().PauseAll();

                isPaused = true;
                Time.timeScale = 0.0f;

                button.SetActive(true);

                Color32 newColor = image.color;
                newColor.a = 100;
                image.color = newColor;
            }
        }
    }

    public void ExitGame() {

        FindObjectOfType<AudioManager>().Play("Blip");

        Application.Quit();
    }

    private IEnumerator StartupSequence() {

        float textDelay = 5f;

        yield return ClickThroughText(textDelay, 
            "I've spent too long being a stupid dumb guy");
        yield return ClickThroughText(textDelay,
            "It's time to show the world I can think deep thoughts");
        yield return ClickThroughText(textDelay,
            "I hope I can keep myself from getting too distracted");

        text.SetText("");
        animator.SetBool("gameStarted", true);
        FindObjectOfType<EnemyManager>().gameStarted = true;
        target.gameStarted = true;
        FindObjectOfType<AudioManager>().Play("Typing");

        Vector3 startPosition = transform.position + 10f * Vector3.up;
        startPosition.z = 0f;
        Instantiate(player, startPosition, Quaternion.identity);
        
    }

    private IEnumerator ClickThroughText(float textTime, string newText) {

        float timer = Time.time;

        text.SetText(newText);
        while (Time.time - timer < textTime) {

            if (Input.GetMouseButtonDown(0)) {
                break;
            } else {
                yield return null;
            }
        }

        yield return null;
    }
}
