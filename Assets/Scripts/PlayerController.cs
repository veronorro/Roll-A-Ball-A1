using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody rb;
    private int pickupCount;
    private Timer timer;
    private bool gameOver;


    [Header("UI")]
    public GameObject inGamePanel;
    public GameObject winPanel;
    public TMP_Text scoreText;
    public TMP_Text timerText;
    public TMP_Text winTimeText;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Get number of pickups in our scene
        pickupCount = GameObject.FindGameObjectsWithTag("PickUp").Length;
        //Run pickups function
        CheckPickups();
        //Get the timer object and start the timer
        timer = FindObjectOfType<Timer>();
        timer.StartTimer();
        //Turn on in game panel
        inGamePanel.SetActive(true);
        //Turn off win panel
        winPanel.SetActive(false);
    }

    private void Update()
    {
        timerText.text = "Time: " + timer.GetTime().ToString("F2");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameOver == true)
            return;
     
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        rb.AddForce(movement * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PickUp")
        {
            Destroy(other.gameObject);
            //Decrement the pickup count
            pickupCount -= 1;
            //Run the check pickups function
            CheckPickups();
        } 
    }

    void CheckPickups()
    {
        //Display the amount of pickups left in scene
        scoreText.text = "Score: " + pickupCount;

        if (pickupCount == 0)
        {
            WinGame();
        }
    }

    void WinGame()
    {
        //set game over to true
        gameOver = true;
       //Stop the timer
        timer.StopTimer();
        //Turn on Win panel
        winPanel.SetActive(true);
        //Turn off In game panel
        inGamePanel.SetActive(false);
        //display timer on the win time text
        winTimeText.text = "Your Win Time Was: " + timer.GetTime().ToString("F2");

        //Set velocity of rigidbody to 0
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    //Code to restart the game once finished
    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene
            (UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);

    }

    //Code to quit the game application when built
    public void QuitGame()
    {
        Application.Quit();
    }
}
