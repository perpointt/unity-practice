using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
	[Header("Set in Inspector: Spawning")]
	// Шаблон для создания яблок
	public GameObject applePrefab;

	/// Скорость движения яблони
	public float speed = 1f;

	/// Расстояние, на котором должно изменяться направление движения яблони
	public float leftAndRightEdge = 10f;

	/// Вероятность изменения направления движения
	public float chanceToChangeDirections = 0.1f;

	/// Частота создания экземпляров яблок
	public float secondsBetweenAppleDrops = 1f;
	
	private ApplePicker _apScript;


	private void Start()
	{
		var mainCamera = Camera.main;
		// Получить ссылку на компонент ApplePicker главной камеры Main Camera
		_apScript = mainCamera!.GetComponent<ApplePicker>();
		// Сбрасывать яблоки раз в секунду
		Invoke( nameof(DropApple), secondsBetweenAppleDrops );
	}

	private void DropApple()
	{
		var apple = Instantiate(applePrefab);
		apple.transform.position = transform.position;
		Invoke( nameof(DropApple), secondsBetweenAppleDrops );
		_apScript.AddApple(apple);
	}
	
	private void Update()
	{
		// Простое перемещение
		var pos = transform.position;
		pos.x += speed * Time.deltaTime;
		transform.position = pos;

		// Изменение направления
		if (pos.x < -leftAndRightEdge)
		{
			speed = Mathf.Abs(speed); // Движение вправо
		}
		else if (pos.x > leftAndRightEdge)
		{
			speed = -Mathf.Abs(speed); // Движение влево
		}
	}

	private void FixedUpdate()
	{
		if (Random.value < chanceToChangeDirections)
		{
			speed *= -1; // Поменять направлеие движения
		}
	}
}
