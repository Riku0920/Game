using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamageScript : MonoBehaviour
{
	// Start is called before the first frame update
	[SerializeField]
	private Image damageImage;
	//　フェードアウトするスピード
	private float fadeOutSpeed = 1f;
	//　移動値
	[SerializeField]
	private float moveSpeed = 0.4f;

	public void Start()
	{
		// 透明にして見えなくしておきます。
		damageImage.color = Color.clear;
	}

	void Update()
	{
		damageImage.color = Color.Lerp(damageImage.color, Color.clear, Time.deltaTime);
		if (damageImage.color.a <= 0.1f)
		{
			damageImage.color = Color.clear;
		}
	}
	public void Damage()
	{
		// *画面を赤塗りにする
		damageImage.color = new Color(0.5f, 0f, 0f, 0.5f);
	}
}