using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public bool canTrippleShot = false;

	[SerializeField]
	private GameObject _laserPrefab;
	[SerializeField]
	private GameObject _trippleShotPrefab;

	[SerializeField]
	private float _fireRate = 0.25f;

	private float _canFire = 0.0f;

	[SerializeField]
	private float _speed = 5.0f;



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
			if (Time.time > _canFire)
			{
				if (canTrippleShot)
				{
					Instantiate(_trippleShotPrefab, transform.position, Quaternion.identity);
				}
				else
				{
					Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.3f, 0), Quaternion.identity);
				}
				_canFire = Time.time + _fireRate;
			}
		}


	}

	private void Shoot()
	{
		if (Time.time > _canFire)
		{
			Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.3f, 0), Quaternion.identity);
			_canFire = Time.time + _fireRate;
		}
	}

	private void Movement()
	{
		float horizontalInput = Input.GetAxis("Horizontal");
		float verticalInput = Input.GetAxis("Vertical");

		transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
		transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);

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
	public void TripleShotPowerupOn()
	{
		canTrippleShot = true;
		StartCoroutine(TrippleShotPowerDownRoutine());
	}
	public IEnumerator TrippleShotPowerDownRoutine()
	{
		yield return new WaitForSeconds(5.0f);
		canTrippleShot = false;
	}

}


