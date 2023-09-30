using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class PlayerStatus : MonoBehaviourPun
{
    public int hp = 100;
    public int score = 0;
    private DataBase DB;
    [SerializeField] Text Bullet;
    [SerializeField] Text DeathText;
    private GameObject OnlineObj;
    private OnlineScripts OnlineScript;
    [SerializeField] Camera DeathCamera;
    private ExitGames.Client.Photon.Hashtable properties;
    [SerializeField]
    private CapsuleCollider cap;
    [SerializeField]
    private Rigidbody rb;
    [SerializeField]
    private MeshRenderer ms;
    [SerializeField]
    private Text ScoreText;
    public bool DeathCheck;
    private string Myname;
    public string Netname;
    public bool Invisible;
    private void Start()
    {
        GetDataBase(GameObject.FindGameObjectWithTag("DataBase").GetComponent<DataBase>());
        OnlineObj = GameObject.FindGameObjectWithTag("Online");
        OnlineScript = OnlineObj.GetComponent<OnlineScripts>();
        //properties = this.gameObject.GetComponent<PhotonView>().Owner.CustomProperties;
        properties = PhotonNetwork.LocalPlayer.CustomProperties;
        if(properties.Count != 0)
        {
            properties["ID"] = (int)PhotonNetwork.CurrentRoom.PlayerCount;
            properties["Score"] = 0;

        }
        else
        {
            properties.Add("Score", 0);
            properties.Add("ID",(int)PhotonNetwork.CurrentRoom.PlayerCount);
        }

        PhotonNetwork.LocalPlayer.SetCustomProperties(properties);
        CountDownTime = 5.0f;
        KillText.text = "";
        ScoreText.text = "Score\n" + 0;
        DeathCheck = false;
        KillCountDown = 3.0f;
        Invisible = false;
    }
    public void ChangeHp(int num)
    {
        //properties = this.gameObject.GetComponent<PhotonView>().Owner.CustomProperties;
        //int n = (int)PhotonNetwork.LocalPlayer.CustomProperties["Hp"];
        //PhotonNetwork.LocalPlayer.CustomProperties["Hp"] = n - num;
        //properties["Hp"] = (int)properties["Hp"] - num;
        //PhotonNetwork.LocalPlayer.SetCustomProperties(properties);
        hp -= num;
    }
    public void GetDataBase(DataBase db)
    {
        DB = db;
        Myname = DB.NameSetting;
        this.gameObject.GetComponent<PhotonView>().RPC("NameSet", RpcTarget.AllBuffered,Myname);
    }
    public void GetScore(int Getscore)
    {
        int point = (int)properties["Score"];
        properties["Score"] = point + Getscore;
        PhotonNetwork.LocalPlayer.SetCustomProperties(properties);
        score = (int)properties["Score"];
        ScoreText.text = "Score\n"+score;

    }
    [PunRPC]
    private void NameSet(string name)
    {
        Netname = name;
    } 

    [PunRPC]
    void RPCSetInvisible(bool invisible)
    {
        DeathCheck = invisible;
        if (invisible)
        {
            ms.material.color = new Color32(0, 0, 0, 0);
            cap.enabled = false;
            rb.constraints = RigidbodyConstraints.FreezePosition;
            rb.freezeRotation = true;
        }
        else
        {
            cap.enabled = true;
            Invisible = true;
            StartCoroutine("InvisibleTime");
            rb.constraints = RigidbodyConstraints.None;
            rb.freezeRotation = true;
        }
    }
    IEnumerator InvisibleTime()
    {
        ms.material.color = new Color32(255, 255, 0, 255);
        yield return new WaitForSeconds(3.0f);
        this.gameObject.GetComponent<PhotonView>().RPC("InvisibleStop", RpcTarget.AllBuffered);
        ms.material.color = new Color32(255, 0, 0, 255);
    }
    [PunRPC]
    public void InvisibleStop()
    {
        Invisible = false;
    }
    [PunRPC]
    public void EnemyName(string name)
    {
        DeathText.text = name + "‚É“|‚³‚ê‚½";
        Debug.Log(name + "“|‚³‚ê‚½");
    }
    [PunRPC]
    public void SetHp(int num)
    {
        hp = num;
    }
    float CountDownTime;
    private void Update()
    {
        if(this.gameObject.transform.position.y <= -50.0f)
        {
            this.gameObject.transform.position = new Vector3(0,0,0);
            ChangeHp(99999);
            DeathText.text = "—Ž‰º";
        }
        try
        {
            //Bullet.text = properties["Hp"].ToString();
            Bullet.text =  "HP:"+hp.ToString();
            if (hp <= 0)
            {
                photonView.RPC("RPCSetInvisible", RpcTarget.All, true);
                photonView.RPC("SetHp", RpcTarget.AllBuffered, 100);
                //properties["Hp"] = hp;
                //PhotonNetwork.LocalPlayer.SetCustomProperties(properties);
                DB.DeathSetting++;
                //Instantiate(DeathCamera,Camera.main.transform.position,Camera.main.transform.rotation);
            }
            if (!cap.enabled)
            {
                CountDownTime -= Time.deltaTime;
                if (CountDownTime <= 0.0f)
                {
                    CountDownTime = 5.0f;
                    hp = 100;
                    OnlineScript.DestroyObj();
                    DeathText.text = "";
                    photonView.RPC("RPCSetInvisible", RpcTarget.All, false);
                }
            }
            if(KillText.text != "")
            {
                KillCountDown -= Time.deltaTime;
                if(KillCountDown <= 0.0f)
                {
                    KillText.text = "";
                }
            }
        }
        catch
        {

        }
    }
    private float KillCountDown;
    [SerializeField]
    private Text KillText;
    public void Kill(string name)
    {
        KillCountDown = 3.0f;
        KillText.text = name + "‚ð“|‚µ‚½";
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            hp -= 5;
        }
    }
    */
}
