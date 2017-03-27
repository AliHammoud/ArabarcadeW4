using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour {

	public float player_speed = 5f;

	private Transform player_cam;
	private float x_rot = 0f;
	private float y_rot = 0f;
	private Rigidbody _rb;
	private float max_speed = 4f;
	private float sin_increment;
	private Collider highlighted_cube;

	void Start () {

		sin_increment = 0;

		Cursor.visible = false;

		_rb = GetComponent<Rigidbody>();

		try {

			player_cam = transform.GetChild (0);
			Debug.Log(player_cam);

		} catch (Exception e) {

			Debug.LogWarning("No camera attached to player");

		}

	}

	void Update () {

		//Mouse Controller
		x_rot += -Input.GetAxis ("Horizontal") * Time.deltaTime;
		y_rot += Input.GetAxis ("Vertical") * Time.deltaTime;

		//Debug.Log (x_rot + "\n" + y_rot + "\n");

		x_rot = Mathf.Clamp (x_rot, -45, 45);

		if (Mathf.Abs (y_rot) > 360)
			y_rot = 0;

		player_cam.eulerAngles = new Vector3 (
			x_rot,
			player_cam.eulerAngles.y,
			player_cam.eulerAngles.z
		);

		transform.eulerAngles = new Vector3 (
			transform.eulerAngles.x, 
			y_rot, 
			transform.eulerAngles.z
		);

		//WASD Controller

		if (Input.GetKey(KeyCode.W)) {

			_rb.AddRelativeForce(
				0, 0, player_speed * Time.deltaTime, ForceMode.VelocityChange
			);
		}

		if (Input.GetKey(KeyCode.A)){

			_rb.AddRelativeForce(
				-player_speed * Time.deltaTime, 0, 0, ForceMode.VelocityChange
			);
		}

		if (Input.GetKey(KeyCode.S)){

			_rb.AddRelativeForce(
				0, 0, -player_speed * Time.deltaTime, ForceMode.VelocityChange
			);
		}

		if (Input.GetKey(KeyCode.D)){

			_rb.AddRelativeForce(
				player_speed * Time.deltaTime, 0, 0, ForceMode.VelocityChange
			);
		}

		if (
			Input.GetKey(KeyCode.W) ||
			Input.GetKey(KeyCode.A) ||
			Input.GetKey(KeyCode.S) ||
			Input.GetKey(KeyCode.D)
		) {
			
			sin_increment += 0.15f;

			player_cam.position = new Vector3 (
				player_cam.position.x,
				player_cam.position.y + Mathf.Sin(sin_increment ) / 40,
				player_cam.position.z
			);

		}

		_rb.velocity = new Vector3 (
			Mathf.Clamp(_rb.velocity.x, -max_speed, max_speed),
			Mathf.Clamp(_rb.velocity.y, -max_speed, max_speed),
			Mathf.Clamp(_rb.velocity.z, -max_speed, max_speed)
		); 

		//Object interaction
		RaycastHit hit;

		if (Physics.Raycast(player_cam.position, player_cam.forward, out hit, 100.0f)) {

			Ray ray = new Ray(player_cam.position, player_cam.forward);
			Debug.DrawLine(ray.origin, hit.point);

			if (hit.collider.tag == "Cube") {

				highlighted_cube = hit.collider;
				hit.collider.GetComponent<CubeBehaviour>().is_active = true;
					
			} else if (hit.collider.tag != "Cube"){

				highlighted_cube.GetComponent<CubeBehaviour>().is_active = false;

			}

		}
	}
}