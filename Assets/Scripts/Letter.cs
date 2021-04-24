using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Letter : MonoBehaviour
{
    [SerializeField]
    private float deathTime = 0.25f,
        fadeInTime = 1f,
        moveSpeed = 20f, 
        spinSpeed = 500f;

    private float deathTimer, fadeInTimer;

    private char character;

    private Collider _collider;

    private TMPro.TextMeshPro text;

    public char Character {
        get { return text.text[0]; }
        set {
            character = value;
            if(text != null)
                text.SetText(character.ToString());
        }
    }

    private void Awake() {

        // Get components
        _collider = GetComponent<Collider>();
        text = GetComponentInChildren<TMPro.TextMeshPro>();
        SetLetter(character);
    }

    private void Start() {

        // Fade in letter
        StartCoroutine(FadeIn());
    }

    public void SetLetter(char newChar) {
        Character = newChar;
    }

    public void RemoveLetter() {

        // Turn off collisions
        _collider.isTrigger = false;

        // Remove layer, potentially solving a bug
        gameObject.layer = LayerMask.NameToLayer("Benign");

        // De-parent from enemy
        transform.parent = null;

        // Start death throes
        StartCoroutine(DeathThroes());
    }

    private IEnumerator FadeIn() {

        // Set timer
        fadeInTimer = Time.time;

        // Fade in during time
        while(Time.time - fadeInTimer < fadeInTime) {

            // Lerp the opacity
            float opacity = Mathf.Lerp(0f, 255f, (Time.time - fadeInTimer) / fadeInTime);
            
            // Set the color
            Color32 faceColor = text.faceColor;
            faceColor.a = (byte)Mathf.FloorToInt(opacity);
            text.faceColor = faceColor;

            yield return null;
        }
    }

    private IEnumerator DeathThroes() {

        // Set timer
        deathTimer = Time.time;

        // Set direction of flight
        float forwarditude = Random.Range(-0.5f, 0.5f);
        float direction = (Random.Range(-1f, 1f) > 0f) ? -1f : 1f;
        Vector3 velocity = moveSpeed * (forwarditude * transform.right + direction * transform.up).normalized;

        // Perform death motion during timer
        while(Time.time - deathTimer < deathTime) {

            // Apply motion to transform
            transform.position += velocity * Time.deltaTime;
            transform.rotation *= Quaternion.Euler(0f, 0f, spinSpeed * Time.deltaTime);

            yield return null;

        }

        // Destroy object after time is up
        Destroy(gameObject);
        yield return null;

    }
}
