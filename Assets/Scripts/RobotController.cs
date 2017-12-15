using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour
{
	public float movementSpeed;
	private Rigidbody rb;

	private Vector3 initialPosition;
	
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		initialPosition = transform.position;
	}

	public void ResetRobot()
	{
		StopAllCoroutines();
		transform.position = initialPosition;
	}
	
	public void GetPathAndMoveToExit()
	{
		var path = SensorManager.Instance.GetPathToExit();

		StartCoroutine(WalkPath(path));
	}

	IEnumerator WalkPath(List<Transform> path)
	{
		foreach (var t in path)
		{
			var dir = t.position - transform.position;
			transform.forward = dir;

			while (Vector3.Distance(t.position, transform.position) > 0.1f)
			{
				rb.MovePosition(Vector3.MoveTowards(transform.position, t.position, movementSpeed * Time.deltaTime));
				yield return null;
			}
		}
	}
}