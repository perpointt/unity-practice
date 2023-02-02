using UnityEngine;
using UnityEngine.UI;

public class Basket : MonoBehaviour
{
	[Header("Set Dynamically")] public int scorePerApple = 100;

	public float leftAndRightEdge = 10f;

	private Text _scoreGt;
	private Camera _mainCamera;

	private ApplePicker _apScript;

	// Start is called before the first frame update
	private void Start()
	{
		_mainCamera = Camera.main;

		// Получить ссылку на компонент ApplePicker главной камеры Main Camera
		_apScript = _mainCamera!.GetComponent<ApplePicker>();


		// Получить ссылку на игровой объект ScoreCounter
		var scoreGo = GameObject.Find("ScoreCounter");

		// Получить компонент Text этого игрового объекта
		_scoreGt = scoreGo.GetComponent<Text>();

		// Установить начальное число очков равным 0
		_scoreGt.text = "0";
	}


	// Update is called once per frame
	private void Update()
	{
		// Получить текущие координаты указателя мыши на экране из Input
		var mousePos2D = Input.mousePosition;

		// Координата Z камеры определяет, как далеко в трехмерном пространстве находится указатель мыши
		mousePos2D.z = -_mainCamera.transform.position.z;

		// Преобразовать точку на двумерной плоскости экрана в трехмерные координаты игры
		var mousePos3D = _mainCamera.ScreenToWorldPoint(mousePos2D);

		// Переместить корзину вдоль оси X в координату X указателя мыши
		var pos = transform.position;


		if (mousePos3D.x > -leftAndRightEdge && mousePos3D.x < leftAndRightEdge)
		{
			pos.x = mousePos3D.x;
			transform.position = pos;
		}
	}

	private void OnCollisionEnter(Collision coll)
	{
		// Отыскать яблоко, попавшее в эту корзину
		var collidedWith = coll.gameObject;

		if (collidedWith.CompareTag("Apple"))
		{
			_apScript.DestroyAnApple(collidedWith.name);

			// Преобразовать текст в scoreGT в целое число
			int score = int.Parse(_scoreGt.text);

			// Добавить очки за пойманное яблоко
			score += scorePerApple;

			// Преобразовать число очков обратно в строку и вывести ее на экран
			_scoreGt.text = score.ToString();

			// Запомнить высшее достижение
			if (score > HighScore.Score)
			{
				HighScore.Score = score;
			}
		}
	}
}
