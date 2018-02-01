using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public bool canTrippleShot = false;
	public bool isSpeedBoostActive = false;
	public int lives = 3;

	[SerializeField]
	private GameObject _laserPrefab;
	[SerializeField]
	private GameObject _trippleShotPrefab;
	[SerializeField]
	private GameObject _laserTcenterPrefab;
	[SerializeField]
	private GameObject _laserTrightPrefab;
	[SerializeField]
	private GameObject _laserTleftPrefab;
	[SerializeField]
	private GameObject _ExplosionPrefab;

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
					//Instantiate(_trippleShotPrefab, transform.position, Quaternion.identity);
					Instantiate(_laserTcenterPrefab, transform.position + new Vector3(0, 0.3f, 0), Quaternion.identity);
					Instantiate(_laserTrightPrefab, transform.position + new Vector3(0.55f, 0, 0), Quaternion.identity);
					Instantiate(_laserTleftPrefab, transform.position + new Vector3(-0.55f, 0, 0), Quaternion.identity);
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

		if (isSpeedBoostActive)
		{
			transform.Translate(Vector3.right * _speed * 1.5f * horizontalInput * Time.deltaTime);
			transform.Translate(Vector3.up * _speed * 1.5f * verticalInput * Time.deltaTime);
		}
		else
		{
			transform.Translate(Vector3.right * _speed * horizontalInput * Time.deltaTime);
			transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);
		}

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

	public void Damage()
	{
		if (lives > 1)
		{
			lives--;
		}
		else
		{
			Destroy(this.gameObject);
			Instantiate(_ExplosionPrefab, transform.position, Quaternion.identity);
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

	public void SpeedBoostOn()
	{
		isSpeedBoostActive = true;
		StartCoroutine(SpeedBoostOff());
	}
	public IEnumerator SpeedBoostOff()
	{
		yield return new WaitForSeconds(5.0f);
		isSpeedBoostActive = false;
	}
}


