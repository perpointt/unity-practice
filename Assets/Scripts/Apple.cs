using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
	private const float BottomY = -20f;

	private ApplePicker _atScript;

	private void Start()
	{
		var mainCamera = Camera.main;
		// Получить ссылку на компонент ApplePicker главной камеры Main Camera
		_atScript = mainCamera!.GetComponent<ApplePicker>();
	}

	private void Update()
	{
		if (transform.position.y < BottomY)
		{
			Destroy(gameObject);
			
			// Вызвать общедоступный метод DestroyApples() из apScript
			_atScript.DestroyApples();
		}
	}
}
