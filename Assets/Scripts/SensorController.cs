using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorController : MonoBehaviour {

	public float sensitivityRadius;
	public List<GameObject> neighborhood;
	public bool debug;

	void Start ()
	{
		sensitivityRadius = 3.0f;
		neighborhood = new List<GameObject>();
		// TODO: O ponto de destino também será um sensor? Ele deve ter o nome diferente para ser identificado
		debug = false;
	}
	
	void FixedUpdate ()
	{
		UpdateNeighborhood();
	}

	void UpdateNeighborhood ()
	{
		Collider[] nearSensors = Physics.OverlapSphere(transform.position, sensitivityRadius);
		neighborhood.Clear();
		foreach (Collider c in nearSensors) {
			
			if (debug) {
				Debug.Log("blocked: "+Physics.Linecast(transform.position, c.transform.position));
				Debug.Log("Object tag: "+c.gameObject.tag);
				Debug.Log("Object name: "+c.gameObject.name);
			}

			// TODO: Criar tag para obstáculo e excluir aqui?
			if (!Physics.Linecast(transform.position, c.transform.position) && (c.gameObject.GetInstanceID() != this.gameObject.GetInstanceID()) && (c.gameObject.CompareTag("Sensor") || c.gameObject.CompareTag("Goal") || c.gameObject.CompareTag("Robot"))) {
				neighborhood.Add(c.gameObject);
			}
		}
	}

}
