using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour {

	public float speed;

	private Rigidbody rb;

	private Graph graph;
	private Vector3 target;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.AddForce (movement * speed);
	}

	void WalkPath ()
	{
		if(transform.position == target) {
			target = graph.nextVertexPosition();
		}
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, target, step);
	}

	void createGraph ()
	{
		graph = new Graph();
//		foreach (GameObject sensor in sensors){}
	}

}
