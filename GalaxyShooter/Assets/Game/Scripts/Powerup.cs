using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour {

	[SerializeField]
	private float _speed = 3.0f;

	private void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("collided with: " + other.name);

		if (other.tag == "Player")
		{
			Player player = other.GetComponent<Player>();

			if (player != null)
			{
				player.TripleShotPowerupOn();
			}

			Destroy(this.gameObject);
		}
	}

	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.down * Time.deltaTime * _speed);
	}
}
