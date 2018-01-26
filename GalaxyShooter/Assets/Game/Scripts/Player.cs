using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public GameObject laserPrefab;

	public float fireRate = 0.25f;
	public float canFire = 0.0f;

	[SerializeField]
	private float speed = 5.0f;

	// Use this for initialization
	void Start () {
		Debug.Log("Name: " + name);
		Debug.Log("XPos: " + transform.position.x);
		transform.position = new Vector3(0, 0, 0);
		//speed = 10;
	}
	
	// Update is called once per frame
	void Update () {
		Movement();

		// if space key pressed
		// spawn lazer at player position

		if ((Input.GetKeyDown(KeyCode.Space)) || Input.GetMouseButtonDown(0))
		{
			if (Time.time > canFire)
			{
				Instantiate(laserPrefab, transform.position + new Vector3(0, 0.3f, 0), Quaternion.identity);
				canFire = Time.time + fireRate;
			}
		}


	}

	private void Movement()
	{
		float horizontalInput = Input.GetAxis("Horizontal");
		float verticalInput = Input.GetAxis("Vertical");

		transform.Translate(Vector3.right * speed * horizontalInput * Time.deltaTime);
		transform.Translate(Vector3.up * speed * verticalInput * Time.deltaTime);

		if (transform.position.y > 0)
		{
			transform.position = new Vector3(transform.position.x, 0, 0);
		}
		else if (transform.position.y < -4.2f)
		{
			transform.position = new Vector3(transform.position.x, -4.2f, 0);
		}

		if (transform.position.x > 7.7f)
		{
			transform.position = new Vector3(-7.7f, transform.position.y, 0);
		}
		else if (transform.position.x < -7.7f)
		{
			transform.position = new Vector3(7.7f, transform.position.y, 0);
		}
	}
}


