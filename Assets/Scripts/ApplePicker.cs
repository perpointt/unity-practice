using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ApplePicker : MonoBehaviour
{
	public GameObject basketPrefab;
	public int numBaskets = 3;
	public float basketBottomY = -14f;
	public float basketSpacingY = 2f;
	private List<GameObject> _apples = new();
	private List<GameObject> _basketList = new();

	private void Start()
	{
		for (int i = 0; i < numBaskets; i++)
		{
			var tBasketGo = Instantiate(basketPrefab);
			var pos = Vector3.zero;
			pos.y = basketBottomY + (basketSpacingY * i);
			tBasketGo.transform.position = pos;
			_basketList.Add(tBasketGo);
		}
	}

	public void AddApple(GameObject item)
	{
		_apples.Add(item);
	}

	public void DestroyApples()
	{
		// Удалить все упавшие яблоки
		foreach (var apple in _apples)
		{
			Destroy(apple);
		}

		_apples.Clear();

		// Удалить одну корзину
		// Получить индекс последней корзины в basketList
		int basketIndex = _basketList.Count - 1;
		// Получить ссылку на этот игровой объект Basket
		var tBasketGo = _basketList[basketIndex];
		// Исключить корзину из списка и удалить сам игровой объект
		_basketList.RemoveAt(basketIndex);
		Destroy(tBasketGo);

		// Если корзин не осталось^ перезапустить игру
		if (_basketList.Count == 0)
		{
			SceneManager.LoadScene("_Scene_0");
		}
	}

	public void DestroyAnApple(string appleName)
	{
		var apple = _apples.Find(x => x.name == appleName);
		if (apple != null)
		{
			Destroy(apple);
			_apples.Remove(apple);
		}
	}
}
