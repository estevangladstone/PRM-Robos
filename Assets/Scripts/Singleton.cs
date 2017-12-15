using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Object
{
	private static T instance;

	/// <summary>
	/// The class's single global instance.
	/// </summary>
	public static T Instance
	{
		get
		{
			if (instance == null)
				instance = Object.FindObjectOfType<T>();

			return instance;
		}
	}
}