using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

	[SerializeField]
	private float _speed;

	[SerializeField]
	private GameObject _EnemyExplosionPrefab;

	private UIManager _uiManager;

	[SerializeField]
	private AudioClip _clip;

	// Use this for initialization
	void Start () {
		_uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
	}
	
	// Update is called once per frame
	void Update () {

		transform.Translate(Vector3.down * _speed * Time.deltaTime);

		if (transform.position.y < -5.8f)
		{
			float randomX = Random.Range(-7f, 7f);
			transform.position = new Vector3(randomX, 5.8f, 0);
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		Debug.Log("collided with: " + other.name);

		

		if (other.tag == "Player")
		{
			Player player = other.GetComponent<Player>();

			if (player != null)
			{
				player.Damage();
			}
		}
		else if (other.tag == "Laser")
		{
			//if (other.transform.parent != null)
			//{
			//	Destroy(other.transform.parent.gameObject);
			//}
			Destroy(other.gameObject);
		}
		Instantiate(_EnemyExplosionPrefab, transform.position, Quaternion.identity);
		AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);
		Destroy(this.gameObject);

		if (_uiManager != null)
		{
			_uiManager.UpdateScore();
		}

	}
}
