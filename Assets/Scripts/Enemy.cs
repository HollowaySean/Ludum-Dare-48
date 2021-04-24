using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Enemy : MonoBehaviour
{

    [SerializeField]
    private float iFrameTime = 0.25f, letterSeparation = 1.5f, moveSpeed = 2.5f;
    public float MoveSpeed { set { moveSpeed = value; } }

    [SerializeField]
    private string text = "";
    private int stringIndex;
    public int StringIndex { 
        get { return stringIndex; } 
        set { stringIndex = value;
            SetWord(StringTable.Shallow[value]); } 
    }

    [SerializeField]
    private Letter letterPrototype;

    private EnemyManager enemyManager;
    public EnemyManager EnemyManager { set { enemyManager = value; } }

    private readonly float iFrameTimer = -1f * Mathf.Infinity;

    private readonly List<Letter> letters = new List<Letter>();

    private Vector3 target;
    public Vector3 Target { set { target = value; } get { return target; } }

    private int lettersPerHit = 1;
    public int LettersPerHit { set { lettersPerHit = value; } get { return lettersPerHit; } }

    private void Start() {

        // Pass in text to letters
        SetWord(text);
    }

    private void Update() {

        // Distance to target
        Vector3 difference = target - transform.position;

        // Move towards target
        Vector3 moveAmount = Vector3.ClampMagnitude(
            moveSpeed * Time.deltaTime * difference.normalized, difference.magnitude);
        transform.position += moveAmount;

        // Rotate towards target
        Vector3 orientation = (difference.x > 0f) ? Vector3.forward : Vector3.back;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, Vector3.Cross(orientation, difference));

    }

    public void SetWord(string word) {

        int spaceCount = 0;

        foreach(char character in word) {

            if (character == ' ') {
                spaceCount++;
            } else {

                // Generate new letter
                Letter newLetter = Instantiate(letterPrototype, transform);

                // Offset from previous
                newLetter.transform.localPosition +=
                    Vector3.right * letterSeparation * spaceCount;

                // Set char and add to list
                newLetter.SetLetter(character);
                letters.Add(newLetter);
                spaceCount++;
            }
        }
    }

    public void TakeDamage() {

        // Check iFrame timer
        if (Time.time - iFrameTimer > iFrameTime) {
            for (int i = 0; i < lettersPerHit; i++) {

                // Remove letter or destroy
                if (letters.Count > 0) {
                    RemoveLetter();
                } else {
                    Die();
                }
            }

            if(letters.Count == 0) {
                Die();
            }
        }
    }

    private void RemoveLetter() {

        // Call removal script and remove from list
        int randomIndex = Random.Range(0, letters.Count - 1);
        letters[randomIndex].RemoveLetter();
        letters.RemoveAt(randomIndex);
    }

    public void Die() {

        // Tell manager to remove enemy
        enemyManager.RemoveEnemy(this);
    }
}
