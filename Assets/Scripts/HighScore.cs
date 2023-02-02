using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
	public static int Score = 0;

	private Text _textComponent;
	private const string HighScoreKey = "HighScore";

	// Start is called before the first frame update
	private void Start()
	{
		_textComponent = GetComponent<Text>();
		_textComponent.text = $"High Score: {Score}";
	}

	// Update is called once per frame
	private void Update()
	{
		_textComponent.text = $"High Score: {Score}";

		// Обновить HighScore в PlayerPrefs, если необходимо
		if (Score > PlayerPrefs.GetInt(HighScoreKey))
		{
			PlayerPrefs.SetInt(HighScoreKey, Score);
		}
	}

	private void Awake()
	{
		// Если значение HighScore уже существует в PlayerPrefs, прочитать его
		if (PlayerPrefs.HasKey(HighScoreKey))
		{
			Score = PlayerPrefs.GetInt(HighScoreKey);
		}

		// Сохранить высшее достижение HighScore в хранилище
		PlayerPrefs.SetInt("HighScore", Score);
	}
}
