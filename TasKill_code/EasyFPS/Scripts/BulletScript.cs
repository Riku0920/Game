using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;

public class BulletScript : MonoBehaviourPunCallbacks
{

	[Tooltip("Furthest distance bullet will look for target")]
	public float maxDistance = 1000000;
	RaycastHit hit;
	[Tooltip("Prefab of wall damange hit. The object needs 'LevelPart' tag to create decal on it.")]
	public GameObject decalHitWall;
	[Tooltip("Decal will need to be sligtly infront of the wall so it doesnt cause rendeing problems so for best feel put from 0.01-0.1.")]
	public float floatInfrontOfWall;
	[Tooltip("Blood prefab particle this bullet will create upoon hitting enemy")]
	public GameObject bloodEffect;
	[Tooltip("Put Weapon layer and Player layer to ignore bullet raycast.")]
	public LayerMask ignoreLayer;
	private int Atk;
	private Rigidbody rb;
	private GameObject OnlineObj;
	private OnlineScripts OnlineScript;
	public bool Death;
	[SerializeField]
	private GameObject par;
	/*
	* Uppon bullet creation with this script attatched,
	* bullet creates a raycast which searches for corresponding tags.
	* If raycast finds somethig it will create a decal of corresponding tag.
	*/
	void Update () {
		
		if(Physics.Raycast(transform.position, transform.forward,out hit, maxDistance, ~ignoreLayer)){
			if(decalHitWall){
				if(hit.transform.tag == "LevelPart"){
					PhotonNetwork.Instantiate("bulletHole", hit.point + hit.normal * floatInfrontOfWall, Quaternion.LookRotation(hit.normal));
					Destroy(gameObject);
				}
				if (hit.transform.tag == "target")
				{
                    if (this.gameObject.GetComponent<PhotonView>().IsMine)
                    {
						hit.transform.gameObject.GetComponent<TaskMato>().DamageMato(this.gameObject.GetComponent<PhotonView>().IsMine);
						PhotonNetwork.Instantiate("bulletHole", hit.point + hit.normal * floatInfrontOfWall, Quaternion.LookRotation(hit.normal));
						Destroy(gameObject);
					}
				}
				if (hit.transform.tag == "Dummie" || (hit.transform.tag == "Player" && ! hit.collider.gameObject.GetComponent<PhotonView>().IsMine)){
					PlayerStatus ps = hit.collider.gameObject.GetComponent<PlayerStatus>();
					if (ps.Invisible)
                    {
						return;
                    }
					Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
					Instantiate(par, hit.point, Quaternion.LookRotation(hit.normal));
					Vector3 Distance = hit.transform.position;
					hit.collider.gameObject.GetComponent<PhotonView>().RPC("Damage", RpcTarget.AllBuffered,Atk);
					hit.collider.gameObject.GetComponent<PhotonView>().RPC("DamagePosition", RpcTarget.Others);
					if (photonView.IsMine)
					{
						//Player owner = hit.collider.gameObject.GetComponent<PhotonView>().Owner;
						if (ps.hp <= 0 && hit.collider.gameObject.GetComponent<CapsuleCollider>().enabled )
						{
							ps.hp = 100;
							Debug.Log(ps.Netname+"を倒した");//敵の名前取得
							OnlineObj = GameObject.FindGameObjectWithTag("Online");
							OnlineScript = OnlineObj.GetComponent<OnlineScripts>();
							OnlineScript.Player.GetComponent<PlayerStatus>().Kill(ps.Netname);
							OnlineScript.Player.GetComponent<PlayerStatus>().GetScore(5);
							OnlineScript.TaskClear(5);
							hit.collider.gameObject.GetComponent<PhotonView>().RPC("EnemyName", RpcTarget.Others,OnlineScript.GetMyName());
							OnlineScript.KillEnemy();
						}
						//hit.gameObject.GetComponent<TakeDamage>().Damage(hit.collider, Atk);
					}
					Destroy(gameObject);
				}
			}		
			//Destroy(gameObject);
		}
		Destroy(gameObject, 0.3f);
	}
    private void Start()
    {
		rb = this.gameObject.GetComponent<Rigidbody>();
		Death = true;
	}
    public void SetAtk(int atk)
	{
		Atk = atk;
	}
	/*
    private void OnTriggerEnter(Collider other)
    {
		Vector3 hitPos = other.ClosestPointOnBounds(this.transform.position);
		if (other.transform.tag == "LevelPart")
		{
			PhotonNetwork.Instantiate("bulletHole", hitPos, Quaternion.LookRotation(hitPos));
			Destroy(gameObject);
		}
		if (other.transform.tag == "Dummie" || other.transform.tag == "Player")
		{
			Instantiate(bloodEffect, hitPos, Quaternion.LookRotation(hitPos));
			other.transform.gameObject.GetComponent<TakeDamage>().Damage(other.transform.gameObject.GetComponent<Collider>(), Atk);
			Destroy(gameObject);
		}
		Destroy(this.gameObject);
	}
	*/
}
