using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

	public float speed;
	public float tilt;
	public float fireRate;
	public Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;

	private float nextFire;

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");	
		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rigidbody.velocity = movement * speed;

		rigidbody.position = new Vector3 (
			Mathf.Clamp (rigidbody.position.x, boundary.xMin, boundary.xMax),
			0.0f, 
			Mathf.Clamp (rigidbody.position.z, boundary.zMin, boundary.zMax)
			);

		rigidbody.rotation = Quaternion.Euler (0.0f, 0.0f, rigidbody.velocity.x* -tilt);
	}

	void Update(){
		if (Input.GetButton ("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			audio.Play();
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
		}
	}
}
