using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public Image image;
    public GameObject button;
    private bool isPaused = false;

    private void Start() {

        FindObjectOfType<AudioManager>().Play("Music");
        FindObjectOfType<AudioManager>().Play("Typing");

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
}
