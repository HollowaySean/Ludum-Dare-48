using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
[RequireComponent(typeof(Rigidbody))]
public class Target : MonoBehaviour
{
    [SerializeField]
    private TMPro.TextMeshPro text;

    [SerializeField]
    private Color good = Color.green, bad = Color.red;

    [SerializeField]
    private ScoreCounter scoreCounter;

    [SerializeField]
    private float letterTime = 0.5f, failureTime = 1f, lingerTime = 1f;

    private bool readyForSentence = true;
    private bool failureMode = false;
    public bool gameStarted = false;

    private int currentIndex;

    private IEnumerator currentSentenceCoroutine;

    private void Awake() {

        // Get components
        text = GetComponentInChildren<TMPro.TextMeshPro>();

    }

    private void Update() {

        if (readyForSentence && gameStarted) {

            // Set flag
            readyForSentence = false;

            // Get random sentence and begin drawing
            currentIndex = Random.Range(0, StringTable.NumDeep);
            currentSentenceCoroutine = DrawSentence(StringTable.Deep[currentIndex]);
            StartCoroutine(currentSentenceCoroutine);
        }
    }

    private void OnTriggerEnter(Collider other) {

        // Check collision type
        switch (LayerMask.LayerToName(other.gameObject.layer)) {
            case "Enemy":
                Enemy enemyHit = other.gameObject.GetComponentInParent<Enemy>();
                if (enemyHit != null) { HitEnemy(enemyHit); }
                break;
        }
    }

    private void HitEnemy(Enemy enemy) {

        // Stop deep thought coroutine, start shallow thought interruption
        if ((currentSentenceCoroutine != null) && !failureMode) {
            failureMode = true;
            StopCoroutine(currentSentenceCoroutine);
            StartCoroutine(RuinSentence(currentIndex, enemy.StringIndex));
        }

        // Destroy enemy
        enemy.Die();
    }

    private IEnumerator SentenceComplete() {

        // Increment score counter
        scoreCounter.IncrementScore();

        // Stick around a bit before starting next
        text.color = good;
        yield return new WaitForSeconds(lingerTime);
        readyForSentence = true;
        text.color = Color.black;
    }

    private IEnumerator DrawSentence(string sentence) {

        string currentString = "";

        for(int i = 0; i < sentence.Length; i++) {

            // Update string
            currentString += sentence[i].ToString();

            // Write to TMP then wait
            text.SetText(currentString);
            yield return new WaitForSeconds(letterTime);
        }

        yield return SentenceComplete();
    }

    private IEnumerator RuinSentence(int deepSentence, int shallowSentence) {

        // Switch to ruined string
        string ruinedString = StringTable.Combos[(deepSentence, shallowSentence)];
        text.SetText(ruinedString);
        text.color = bad;

        // Linger in your shame before starting next
        yield return new WaitForSeconds(failureTime);
        readyForSentence = true;
        failureMode = false;
        text.color = Color.black;
    }
}
