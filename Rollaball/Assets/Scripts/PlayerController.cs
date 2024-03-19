using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI timerText;

    public GameObject gameEndPanel; // Reference to the GameEnd Panel GameObject

    public TimerScript timerScript; // Reference to the TimerScript component

    public AudioSource audioSource;
    public AudioClip pickupSound;
    public AudioClip clockBoostSound;

    private Rigidbody rb;
    private int count;
    private Vector2 movementInput;
    private bool gameIsOver = false; // New variable to keep track of the game state

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        count = 0;
        SetCountText();
        gameEndPanel.SetActive(false);
    }

    void OnMove(InputValue movementValue)
    {
        if (gameIsOver) return; // Ignore input if the game is over
        movementInput = movementValue.Get<Vector2>();
    }

    void SetCountText()
    {
        countText.text = "RINGS " + count.ToString() + "/79";
    }

    void FixedUpdate()
    {
        if (gameIsOver) return; // Don't process movement if the game is over

        Vector3 movement = new Vector3(movementInput.x, 0.0f, movementInput.y) * speed * Time.fixedDeltaTime;

        // Check for forward movement collisions
        RaycastHit hit;
        if (!Physics.Raycast(transform.position, movement.normalized, out hit, movement.magnitude))
        {
            rb.MovePosition(rb.position + movement);
        }

        if (movement != Vector3.zero)
        {
            // Instantly rotate the player to face the direction of movement
            Quaternion toRotation = Quaternion.LookRotation(movement);
            rb.rotation = toRotation;
        }
    }



    void OnTriggerEnter(Collider other)
    {
        if (gameIsOver) return; // Ignore collisions if the game is over

        bool isFXOn = PlayerPrefs.GetInt("fxaudio", 1) == 1; // Retrieve the FX audio setting

        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count++;
            SetCountText();
            if (isFXOn)
            {
                audioSource.PlayOneShot(pickupSound);
            }

            if (count >= 79)
            {
                EndGame("Game Over\nYou Won!");
            }
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            EndGame("Game Over\nYou Lost");
        }
        else if (other.gameObject.CompareTag("ClockBoost") && timerScript != null)
        {

            other.gameObject.SetActive(false);
            timerScript.TimeLeft += 5f;
            // Play the clock boost sound only if FX audio is enabled
            if (isFXOn)
            {
                audioSource.PlayOneShot(clockBoostSound);
            }
        }
    }

    public void EndGame(string resultText)
    {
        gameIsOver = true;
        enabled = false; // Disable the script

        if (timerScript != null)
        {
            timerScript.TimerOn = false;
        }

        // Stop all enemies
        EnemyMovement[] enemies = FindObjectsOfType<EnemyMovement>();
        foreach (EnemyMovement enemy in enemies)
        {
            enemy.StopMovement();
        }

        // Change the countText position
        if (countText != null)
        {
            RectTransform countTextRectTransform = countText.GetComponent<RectTransform>();
            countTextRectTransform.anchoredPosition = new Vector2(280, -150);
        }

        if (timerText != null)
        {
            RectTransform timerTextRectTransform = timerText.GetComponent<RectTransform>();
            timerTextRectTransform.anchoredPosition = new Vector2(430, -220);
        }

        // Display the end game panel
        gameEndPanel.SetActive(true);
        TextMeshProUGUI endText = gameEndPanel.GetComponentInChildren<TextMeshProUGUI>();
        endText.text = resultText;
    }
}
