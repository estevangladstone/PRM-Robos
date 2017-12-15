using System.Collections.Generic;
using BitStrap;
using UnityEngine;
using UnityEngine.UI;

public sealed class ObstacleManager : MonoBehaviour
{
	public Obstacle obstaclePrefab;
	public int obstacleCount;
	
	public int numberOfObstaclesToSpawn = 50;
	public float levelSizeX;
	public float levelSizeZ;
	
	public Text uiText;
	
	private List<Obstacle> obstacleList = new List<Obstacle>();

	public void SetNumberOfObstacles(float v)
	{
		numberOfObstaclesToSpawn = (int) v;
		uiText.text = v.ToString();
	}

	
	public void SpawnObstacles()
	{
		SensorManager.Instance.ClearSensors();
		foreach (var obstacle in obstacleList)
			Destroy(obstacle.gameObject);
		obstacleList.Clear();
		
		for (int i = 0; i < numberOfObstaclesToSpawn; i++)
		{
			var obs = Create.Behaviour(obstaclePrefab, transform);

			var sizeX = Random.Range(1.0f, 5.0f);
			var sizeZ = Random.Range(1.0f, 5.0f);

			var posX = Random.Range(-levelSizeX, levelSizeX);
			var posZ = Random.Range(-levelSizeZ, levelSizeZ);

			obs.transform.position = new Vector3(posX, 1, posZ);
			obs.transform.localScale = new Vector3(sizeX, obstaclePrefab.transform.localScale.y, sizeZ);
			
			obstacleList.Add(obs);
		}
	}
}
