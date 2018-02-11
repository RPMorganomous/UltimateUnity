using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {

	[SerializeField]
	private float _speed = 3.0f;
	[SerializeField]
	private int powerupID;  // 0=triple shot, 1=speed boost, 2=shields
	[SerializeField]
	private AudioClip _clip;

	private void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("collided with: " + other.name);

		if (other.tag == "Player")
		{
			Player player = other.GetComponent<Player>();

			if (player != null)
			{
				if (powerupID == 0)
				{
					player.TripleShotPowerupOn();
				}
				else if (powerupID == 1)
				{
					player.SpeedBoostOn();
				}
				else if (powerupID == 2)
				{
					player.ShieldOn();
				}
			}
			AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
			Destroy(this.gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.down * Time.deltaTime * _speed);

		if (transform.position.y < -7)
		{
			Destroy(this.gameObject);
		}
	}
}
