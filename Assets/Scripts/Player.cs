using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 10f, 
        turnSpeed = 800f, 
        warmupTime = 0.25f, 
        iFrameTime = 0.25f;

    [SerializeField]
    private Bullet bullet;
    [SerializeField]
    private Transform projectilePivot;

    private float warmupTimer = -1f * Mathf.Infinity, 
        iFrameTimer;
    private bool vulnerable = true;

    private Camera mainCamera;
    private CameraTools cameraTools;
    private Vector3 velocity = Vector3.zero, 
        boostVelocity = Vector3.zero;
    private Quaternion rotation = Quaternion.identity;


    private void Start() {

        // Initialize variables
        rotation = transform.rotation;

        // Gather objects
        mainCamera = Camera.main;
        cameraTools = mainCamera.GetComponent<CameraTools>();
    }

    private void Update() {

        PlayerInput();
        Move();
    }

    private void PlayerInput() {
        
        // Get keyboard input
        float xInput = Input.GetAxisRaw("Horizontal");
        float yInput = Input.GetAxisRaw("Vertical");
        velocity = moveSpeed * new Vector3(xInput, yInput, 0f).normalized;

        // Get mouse position
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -1 * mainCamera.transform.position.z));
        mousePosition.z = 0f;

        // Calculate rotation from mouse position
        float angleToMouse = Vector3.SignedAngle(transform.up, mousePosition - transform.position, transform.forward);
        float turnAngle = Mathf.Clamp(angleToMouse, -turnSpeed * Time.deltaTime, turnSpeed * Time.deltaTime);
        rotation *= Quaternion.Euler(0f, 0f, turnAngle);

        // Get mouse inputs
        if (Input.GetMouseButton(0)) { Attack(); }
    }

    private void Move() {

        // Translate position
        transform.position += (velocity + boostVelocity) * Time.deltaTime;

        // Clamp to screen bounds
        transform.position = cameraTools.ClampToScreenBounds(transform.position);

        // Rotate... rotation
        transform.rotation = rotation;
    }

    private void Attack() {

        if(Time.time - warmupTimer > warmupTime) {

            // Reset timer
            warmupTimer = Time.time;

            // Create projectile
            Instantiate(bullet, projectilePivot.position, projectilePivot.rotation);
            bullet.Boost = velocity;
        }
    }

    private void OnTriggerEnter(Collider other) {

        // Get collider position
        Vector3 direction = transform.position - other.gameObject.transform.position;

        // Check if collided with enemy
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy")) { HitEnemy(direction); }
        
        // Check if collided with target
        if (other.gameObject.layer == LayerMask.NameToLayer("Target")) { HitEnemy(direction); }
    }

    private void HitEnemy(Vector3 direction) {

        if (vulnerable) {
            StartCoroutine(Knockback(direction));
        }
    }

    private IEnumerator Knockback(Vector3 direction) {

        // Set timer and boolean
        iFrameTimer = Time.time;
        vulnerable = false;

        while(Time.time - iFrameTimer < iFrameTime) {

            // Add velocity boost
            float t = (Time.time - iFrameTimer) / iFrameTime;
            boostVelocity = Vector3.Lerp(direction.normalized * moveSpeed * 3f, Vector3.zero, t);
            yield return null;
        }

        boostVelocity = Vector3.zero;
        vulnerable = true;
        yield return null;

    }
}
