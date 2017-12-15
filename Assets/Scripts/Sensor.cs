using System.Collections;
using System.Collections.Generic;
using BitStrap;
using UnityEngine;

public class Sensor : MonoBehaviour
{
	public LineRenderer connectionEffect;
	public float sensitivityRadius;
	public List<Sensor> connectedSensors = new List<Sensor>();
	public bool debug;

	public Color activeColor;
	public Renderer sensorRenderer;
	
	public LayerMask detectionLayerMask;
	public List<Transform> ignoreWhenDeleting;

	private Color defaultColor;
	public bool Visited { get; set; }

	void Start ()
	{
		if (sensorRenderer != null)
			defaultColor = sensorRenderer.material.color;
	}

	public void ActivateSensor()
	{
		if (sensorRenderer != null)
			sensorRenderer.material.color = activeColor;
	}
	
	public void Clear()
	{
		for (int i = 0; i < transform.childCount; i++)
		{
			var t = transform.GetChild(i);
			if (!ignoreWhenDeleting.Contains(t))
				Destroy(transform.GetChild(i).gameObject);
		}
		
		connectedSensors.Clear();
	}

	[Button]
	public void UpdateNeighbors()
	{
		Clear();
		var nearSensors = Physics.OverlapSphere(transform.position, sensitivityRadius, detectionLayerMask.value);
		
		foreach (var sensor in nearSensors)
		{
			bool blocked = false;
			
			RaycastHit hitInfo;
			var direction = (sensor.transform.position - transform.position);

			if (Mathf.Abs(direction.y) > 0.4f)
				continue;
			
			if (Physics.Raycast(transform.position, direction.normalized, out hitInfo, sensitivityRadius, detectionLayerMask.value))
			{
				if (hitInfo.collider != sensor)
					blocked = true;
			}

			if (debug) 
			{
				Debug.Log("blocked: "+Physics.Linecast(transform.position, sensor.transform.position));
				Debug.Log("Object tag: "+sensor.gameObject.tag);
				Debug.Log("Object name: "+sensor.gameObject.name);
			}
			
			if( blocked )
				continue;

			var s = sensor.gameObject.GetComponent<Sensor>();
			if (s == null || sensor.gameObject == gameObject)
				continue;

			connectedSensors.Add(s);
		}

		UpdateConnectionEffects();
	}

	private void UpdateConnectionEffects()
	{
		foreach (var connectedSensor in connectedSensors)
		{
			var effect = Create.Prefab(connectionEffect.gameObject, transform);
			var lr = effect.GetComponent<LineRenderer>();

			var t = connectedSensor.transform.position;
			
			lr.SetPosition(0, new Vector3(transform.position.x, 0.1f, transform.position.z));
			lr.SetPosition(1, new Vector3(t.x, 0.1f, t.z));
		}
	}
}
