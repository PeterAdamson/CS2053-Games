using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public float speed;
	public Text countText;
	public Text winText;
	public Text timerText;
	public Text welcomeText;

	private Rigidbody rb;
	private int count;
	private int timer;
	private int startTime;
	private int elapsedTime;

	private bool gameStarted;
	private bool gameOver;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		count = 0;
		timer = 20;
		startTime = (int) Time.time;
		elapsedTime = 0;
		SetCountText ();
		winText.text = "";
		welcomeText.text = "Welcome to Roll-A-Ball. Push 'S' to Start.";
		timerText.text = "Time Left: " + timer.ToString();
		gameStarted = false;
		gameOver = false;
	}

	void Update()
	{
		if(Input.GetKeyDown(KeyCode.S))
		{
			startTime = (int) Time.time;
			gameStarted = true;
			welcomeText.text = "";
		}
		if(Input.GetKeyDown(KeyCode.R))
		{
			SceneManager.LoadScene( SceneManager.GetActiveScene().name );
		}
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();
		}
		if(gameStarted == true)
		{
			if(gameOver == false)
			{
				SetTimerText ();
			}
		}
	}

	void FixedUpdate ()
	{
		if(gameStarted == true && gameOver == false)
		{
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis("Vertical");

			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

			rb.AddForce (movement * speed);
		}
	}

	void OnTriggerEnter(Collider other)
	{
			if (other.gameObject.CompareTag("Pick Up"))
			{
				other.gameObject.SetActive (false);
				count = count + 1;
				SetCountText ();
			}
  }

	void SetCountText ()
	{
		countText.text = "Count: " + count.ToString ();
		if (count >= 12 && gameOver == false)
		{
			winText.text = "You Win! Push 'R' to Restart.";
			gameOver = true;
		}
	}

	void SetTimerText ()
	{
		elapsedTime = (int) Time.time - startTime;
		timer = 20 - elapsedTime;
		timerText.text = "Time Left: " + timer.ToString();
		if (timer <= 0)
		{
			winText.text = "Time's Up! You Lose! Push 'R' to Restart.";
			gameOver = true;
		}
	}
}

//the method call below returns a bool indicating if a key has
//been pushed since the last frame
//Input.GetKeyDown(KeyCode.S);
//Input.GetKeyDown(KeyCode.R);
//Input.GetKeyDown(KeyCode.Escape);

//reload your game scene using the following line
//see the hint above for dealing with a darker scene after reload
//SceneManager.LoadScene( SceneManager.GetActiveScene().name );

//to use the line above you will need to first import the package
//containing the SceneManager
//using UnityEngine.SceneManagement;

//to exit a game use the line below
//note that this only works in a built game, and not in the editor
//Application.Quit();
