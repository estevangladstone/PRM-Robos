using System.Collections.Generic;
using System.Linq;
using BitStrap;
using UnityEngine;
using UnityEngine.UI;

public class SensorManager : Singleton<SensorManager>
{
	public RobotController robot;
	public Sensor exitSensor;
	
	public Sensor sensorPrefab;
	
	[Header("Spawn Configurations")]
	public int numberOfSensorsToSpawn = 50;
	public float levelSizeX;
	public float levelSizeZ;

	public Text uiText;
	
	private List<Sensor> sensors = new List<Sensor>();

	public void SetNumberOfSensors(float v)
	{
		numberOfSensorsToSpawn = (int) v;
		uiText.text = v.ToString();
	}
	
	public void ClearSensors()
	{
		foreach (var sensor in sensors)
			Destroy(sensor.gameObject);
		sensors.Clear();
		
		robot.GetComponent<Sensor>().Clear();
		exitSensor.Clear();
	}

	public void SpawnSensors()
	{
		ClearSensors();
		for (int i = 0; i < numberOfSensorsToSpawn; i++)
		{
			var s = Create.Behaviour(sensorPrefab);

			var x = Random.Range(-levelSizeX, levelSizeX);
			var z = Random.Range(-levelSizeZ, levelSizeZ);

			s.transform.position = new Vector3(x, 10, z);
			
			sensors.Add(s);
		}
	}

	public void UpdateNeighbors()
	{
		foreach (var sensor in sensors)
			sensor.UpdateNeighbors();
		robot.GetComponent<Sensor>().UpdateNeighbors();
		exitSensor.UpdateNeighbors();
	}

	public List<Transform> GetPathToExit()
	{
		var robotSensor = robot.GetComponent<Sensor>();
		var allSensors = new List<Sensor>(sensors);
		allSensors.Add(robotSensor);
		allSensors.Add(exitSensor);

		foreach (var sensor in allSensors)
			sensor.Visited = false;

		var path = new List<Sensor>();
		var pathMap = new Dictionary<Sensor, Sensor>();
		var container = new Queue<Sensor>();

		container.Enqueue(robotSensor);
		robotSensor.Visited = true;
		while (container.Count > 0) 
		{
			var vertex = container.Dequeue();
			foreach(var sensor in vertex.connectedSensors)
			{
				if (!sensor.Visited)
				{
					pathMap[sensor] = vertex;
					container.Enqueue(sensor);
					sensor.Visited = true;
				}
			}
		}

		var s = exitSensor;
		while (s != null)
		{
			path.Add(s);
			if (pathMap.ContainsKey(s))
				s = pathMap[s];
			else
				break;
		}

		if (path.Last() != robotSensor)
		{
			return new List<Transform>();
		}

		path.Reverse();
		foreach (var sensor in path)
			sensor.ActivateSensor();
		
		return path.Select(t => t.transform).ToList();
	}

	private bool FindPath(Sensor pivot, Sensor target, ref Stack<Sensor> path)
	{
		path.Push(pivot);
		pivot.Visited = true;

		if (pivot == target)
		{
			return true;
		}
		
		foreach (var sensor in pivot.connectedSensors)
		{
			if( sensor.Visited )
				continue;
			
			if (FindPath(sensor, target, ref path))
				return true;
		}

		path.Pop();
		return false;
	}
}
