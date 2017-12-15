//using System;
//
//namespace AssemblyCSharp
//{
//	public class EmptyClass
//	{
//		public EmptyClass ()
//		{
//		}
//	}
//}

using System;
using System.Collections.Generic;
using UnityEngine;

public class Graph {

	private Dictionary<GameObject, GameObject[]> vertices = new Dictionary<GameObject, GameObject[]>();
	private List<GameObject> path = null;
//	private int currentVertexIndex = null;

	public void AddVertex(GameObject vertex, GameObject[] neighbors)
	{
		vertices[vertex] = neighbors;
	}

	public List<GameObject> shortest_path(GameObject start, GameObject finish) // Recebe o ROBO e o GOAL
	{
/*		var previous = new Dictionary<GameObject, GameObject>();
		var distances = new Dictionary<GameObject, float>();
		var nodes = new List<GameObject>();

		foreach (var vertex in vertices) {
			if (vertex == start) {
				distances[vertex] = 0; 
			} else {
				distances[vertex] = float.MaxValue; 
			}

			nodes.Add(vertex);
		}

		while (nodes.Count != 0) {
			nodes.Sort((x, y) => distances[x] - distances[y]);

			var smallest = nodes[0];
			nodes.Remove(smallest);

			if (smallest == finish) {
				path = new List<GameObject>();
				while (previous.ContainsKey(smallest)) {
					path.Add(smallest);
					smallest = previous[smallest];
				}

				break;
			}

			if (distances[smallest] == float.MaxValue) {
				break;
			}

			foreach (var neighbor in vertices[smallest]) {
				var alt = distances[smallest] + Vector3.Distance(neighbor.transform.postion, vertices[smallest].transform.postion);
				if (alt < distances[neighbor]) {
					distances[neighbor] = alt;
					previous[neighbor] = smallest;
				}
			}
		}

*/		return path;
	}

	public Vector3 nextVertexPosition ()
	{
		return Vector3.zero;
/*		if(currentVertexIndex == null) {
			currentVertexIndex = 0;
		} else {
			currentVertexIndex = currentVertexIndex + 1;
		}
		return path[currentVertexIndex].transform.position;
*/	}

}