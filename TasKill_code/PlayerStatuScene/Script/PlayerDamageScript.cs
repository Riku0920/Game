using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamageScript : MonoBehaviour
{
	// Start is called before the first frame update
	[SerializeField]
	private Image damageImage;
	//�@�t�F�[�h�A�E�g����X�s�[�h
	private float fadeOutSpeed = 1f;
	//�@�ړ��l
	[SerializeField]
	private float moveSpeed = 0.4f;

	public void Start()
	{
		// �����ɂ��Č����Ȃ����Ă����܂��B
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
		// *��ʂ�ԓh��ɂ���
		damageImage.color = new Color(0.5f, 0f, 0f, 0.5f);
	}
}