using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager<T> : MonoBehaviour, IManager
					where T : MonoBehaviour
{
	private static T instance;


	public static T Instance
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

	public virtual void UpdateManager(float deltaTime)
	{
		
	}


	public virtual void Initialize()
	{
		
	}
}
