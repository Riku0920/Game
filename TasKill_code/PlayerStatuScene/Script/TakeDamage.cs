using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;

public class TakeDamage : MonoBehaviour
{	//　DamageUIプレハブ
	[SerializeField]
	private GameObject damageUI;
	[SerializeField]
	private GameObject DamagePosUI;
	private CapsuleCollider col;
	private Vector3 pos;

    private void Awake()
    {
		pos.y = 1.0f; 
		col = this.gameObject.GetComponent<CapsuleCollider>();

	}
	[PunRPC]
	public void Damage(int DamageNum)
	{
		//　DamageUIをインスタンス化。登場位置は接触したコライダの中心からカメラの方向に少し寄せた位置
		var obj = Instantiate<GameObject>(damageUI, col.bounds.center - Camera.main.transform.forward + pos, Quaternion.identity);
		obj.GetComponentInChildren<Text>().text = DamageNum.ToString();
		obj.transform.SetParent(col.transform);
		col.transform.gameObject.GetComponent<PlayerStatus>().ChangeHp(DamageNum);
	}
	[PunRPC]
	public void DamagePosition()
	{
		this.gameObject.GetComponentInChildren<PlayerDamageScript>().Damage();
	}

}