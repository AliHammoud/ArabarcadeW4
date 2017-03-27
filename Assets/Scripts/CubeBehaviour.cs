using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehaviour : MonoBehaviour {

	public bool is_active = false;
	public Material idle_mat;
	public Material activated_mat;

	void Start () {
		
	}

	void Update () {

		if (is_active) {

			GetComponent<MeshRenderer>().material = activated_mat;

		} else {

			GetComponent<MeshRenderer>().material = idle_mat;

		}

	}
}
