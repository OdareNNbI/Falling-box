using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager<T> : MonoBehaviour
					where T : MonoBehaviour
{
	private static T instance;


	public T Instance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<T>();
				if (instance == null)
				{
					GameObject obj = new GameObject(typeof(T).ToString() + " Auto Singleton");
					instance = obj.AddComponent<T>();
				}
			}

			return instance;
		}
	}
}
