using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public delegate void PlayerPosition();

public class PlayerController : MonoBehaviour {
    // serialized variables
    [SerializeField] Transform playerCamera;

    [SerializeField] float jumpForce = 550f;
    [SerializeField] float speed;
    [SerializeField] float mouseSensitivity;
    [SerializeField] float gravity = -15f;

    [SerializeField] [Range(0.0f, 0.5f)] float moveSmoothTime = 0.3f;
    [SerializeField] [Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;
    [SerializeField] int weaponDamage = 50;
    [SerializeField] float fireRate = 0.2f;
    [SerializeField] TextMeshProUGUI healthText;

    // Private variables
    private float cameraPitch;
    private bool lockedCursor = true;
    private float gravityVelocity;
    private float jumpInput;
    private int health = 100;
    private float nextFire;

    private Vector2 currentDir;
    private Vector2 currentDirVelocity;
    private Vector2 currentMouseDir;
    private Vector2 currentMouseDirVelocity;
    private CharacterController controller;
    private AudioSource gunShot;

    // Start is called before the first frame update
    void Start() {
        controller = GetComponent<CharacterController>();
        gunShot = GetComponent<AudioSource>();

        healthText.text = "Health: " + health;

        if (lockedCursor) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    // Update is called once per frame
    void Update() {
        MouseMovement();
        KeyboardMovement();
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire) {
            fireWeapon();
        }
    }

    private void MouseMovement () {
        Vector2 targetMouseDir = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        currentMouseDir = Vector2.SmoothDamp(currentMouseDir, targetMouseDir, ref currentMouseDirVelocity, mouseSmoothTime);

        cameraPitch += currentMouseDir.y * mouseSensitivity;
        cameraPitch = Mathf.Clamp(cameraPitch, -90f, 90f);
        
        // Camera movement
        transform.Rotate(Vector3.up * currentMouseDir.x * mouseSensitivity);
        playerCamera.localEulerAngles = Vector3.left * cameraPitch;
    }

    private void KeyboardMovement () {
        Vector2 targetDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        jumpInput = Input.GetAxis("Jump");
        targetDir.Normalize();

        gravityVelocity += gravity * Time.deltaTime;
        if (controller.isGrounded) {
            gravityVelocity = 0.0f;

            if (jumpInput == 1) {
                gravityVelocity = jumpForce;
            }
            
        }

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        Vector3 velocity = ((transform.forward * currentDir.y + transform.right * currentDir.x) * speed) + transform.up * gravityVelocity;
        controller.Move(velocity * Time.deltaTime);
    }

    private void fireWeapon () {

        nextFire = Time.time + fireRate;
        float distance = 50;
        RaycastHit hit;

        gunShot.Play();

        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, distance)) {
            if (hit.collider.CompareTag("Enemy")) {
                hit.collider.gameObject.GetComponent<EnemyController>().ReduceHealth(weaponDamage);
            }
        }
    }

    public void ReduceHealth (int damage) {
        health -= damage;
        healthText.text = "Health: " + health;
    }
}
