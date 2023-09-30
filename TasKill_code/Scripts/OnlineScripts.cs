using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// MonoBehaviourPunCallbacksを継承して、PUNのコールバックを受け取れるようにする
public class OnlineScripts : MonoBehaviourPunCallbacks
{
    bool CheckCG;
    public GameObject Player;
    private TakeDamage TakeDamage;
    private GameObject DataBase;
    private DataBase DB;
    private ExitGames.Client.Photon.Hashtable properties;
    private bool GameStart = false;
    private float gameCountDown = 10.0f;
    [SerializeField]
    private Text StartTimer;
    private float GameTimer = 180.0f;
    public bool endgame = false;
    private GameObject[] SpawnPoint;
    private void Start()
    {
        // PhotonServerSettingsの設定内容を使ってマスターサーバーへ接続する
        PhotonNetwork.ConnectUsingSettings();
        SpawnPoint = GameObject.FindGameObjectsWithTag("Spawn");
    }
    public ExitGames.Client.Photon.Hashtable roomHash;
    // マスターサーバーへの接続が成功した時に呼ばれるコールバック
    public override void OnConnectedToMaster()
    {
        // "Room"という名前のルームに参加する（ルームが存在しなければ作成して参加する）
        var roomOptions = new RoomOptions();
        PhotonNetwork.JoinOrCreateRoom("Room", roomOptions, TypedLobby.Default);
        StartTimer.text = "マッチング中...";
    }
    public bool GameTime = false;
    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        object value = null;
        if (propertiesThatChanged.TryGetValue("GamePlay", out value))
        {
            GameTime = (bool)value;
        }
        object valuecount = null;
        if (propertiesThatChanged.TryGetValue("StartCount", out valuecount))
        {
            gameCountDown = (float)valuecount;
        }
        object valuestart = null;
        if (propertiesThatChanged.TryGetValue("GameStart", out valuestart))
        {
            GameStart = (bool)valuestart;
        }
        object valuetime = null;
        if (propertiesThatChanged.TryGetValue("GameTime", out valuetime))
        {
            GameTimer = (float)valuetime;
        }
    }
    // ゲームサーバーへの接続が成功した時に呼ばれるコールバック

    private void StartInit()
    {
        roomHash["StartCount"] = 10.0f;
        roomHash["GameStart"] = true;
        PhotonNetwork.CurrentRoom.SetCustomProperties(roomHash);
    }
    private bool TaskStart = true;
    private void Update()
    {
        if (GameStart && !endgame && !GameTime)
        {
            roomHash["GameStart"] = true;
            float time = (float)roomHash["StartCount"];
            roomHash["StartCount"] = time - Time.deltaTime;
            PhotonNetwork.CurrentRoom.SetCustomProperties(roomHash);
            StartTimer.text = "開始まで\n"+gameCountDown.ToString("N2");
            if (PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                endgame = true;
                bool win = true;
                GameTimer = 180.0f;
                int score = Player.GetComponent<PlayerStatus>().score;
                roomHash["GamePlay"] = false;
                PhotonNetwork.CurrentRoom.SetCustomProperties(roomHash);
                DB.WinSetting++;
                StartTimer.text = "終了";
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                GameObject UI = Instantiate(EndGameUI);
                UI.GetComponent<EndGameScript>().SetWin(win, false);
                UI.GetComponent<EndGameScript>().SetScore(score);
            }

        }
        if(gameCountDown <= 0.0f && !endgame)
        {
            properties = PhotonNetwork.LocalPlayer.CustomProperties;
            int PointNum = (int)properties["ID"];
            Debug.Log(PointNum);
            // ランダムな座標に自身のアバター（ネットワークオブジェクト）を生成する
            var position = SpawnPoint[PointNum-1].transform.position;
            Player.transform.position = position;
            StartTimer.text = "";
            gameCountDown = 3.0f;
            roomHash["GameStart"] = false;
            roomHash["GamePlay"] = true;
            roomHash["StartCount"] = 3.0f;
            PhotonNetwork.CurrentRoom.SetCustomProperties(roomHash);
        }
        if (GameTime && !endgame)
        {
            if (TaskStart)
            {
                TaskStart = false;
                Player.GetComponent<Taskmanagement>().StartGame();
            }
            if(PhotonNetwork.CurrentRoom.PlayerCount == 1)
            {
                endgame = true;
                bool win = true;
                GameTimer = 180.0f;
                int score = Player.GetComponent<PlayerStatus>().score;
                roomHash["GamePlay"] = false;
                PhotonNetwork.CurrentRoom.SetCustomProperties(roomHash);
                DB.WinSetting++;
                StartTimer.text = "終了";
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                GameObject UI = Instantiate(EndGameUI);
                UI.GetComponent<EndGameScript>().SetWin(win,false);
                UI.GetComponent<EndGameScript>().SetScore(score);
            }
            float time = (float)roomHash["GameTime"];
            roomHash["GameTime"] = time - Time.deltaTime;
            PhotonNetwork.CurrentRoom.SetCustomProperties(roomHash);
            StartTimer.text = "残り時間\n" + GameTimer.ToString("N2");
        }
        if(GameTimer <= 0.0f && !endgame)
        {
            endgame = true;
            bool win = true;
            GameTimer = 180.0f;
            int score = Player.GetComponent<PlayerStatus>().score;
            roomHash["GamePlay"] = false;
            PhotonNetwork.CurrentRoom.SetCustomProperties(roomHash);
            Player[] others = PhotonNetwork.PlayerListOthers;
            foreach (Player enemy in others)
            {
                int enemyscore = (int)enemy.CustomProperties["Score"];
                if (enemyscore > score)
                {
                    win = false;
                    break;
                }
            }
            if (win)
            {
                DB.WinSetting++;
            }
            else
            {
                DB.LoseSetting++;
            }
            StartTimer.text = "終了";
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            GameObject UI = Instantiate(EndGameUI);
            UI.GetComponent<EndGameScript>().SetWin(win,true);
            UI.GetComponent<EndGameScript>().SetScore(score);
            /*
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene("selectScene");
            */
        }
    }
    [SerializeField]
    private GameObject EndGameUI;
    [SerializeField]
    private GameObject MapCamera;
    public GameObject Sound;
    private int MaxPlayer = 3;
    public override void OnJoinedRoom()
    {
        roomHash = PhotonNetwork.CurrentRoom.CustomProperties;
        if(roomHash.Count == 0)
        {
            roomHash = new ExitGames.Client.Photon.Hashtable();
            roomHash.Add("GamePlay", false);
            roomHash.Add("GameStart", false);
            roomHash.Add("StartCount", 10.0f);
            roomHash.Add("GameTime", 180.0f);
            PhotonNetwork.CurrentRoom.SetCustomProperties(roomHash);
        }
        GameTime = (bool)roomHash["GamePlay"];
        if (PhotonNetwork.CurrentRoom.PlayerCount >= (MaxPlayer + 1) || GameTime)
        {
            roomHash = null;
            endgame = true;
            PhotonNetwork.Disconnect();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("NoMatchScene");
            //SceneManager.LoadScene("selectScene");
        }
        if (PhotonNetwork.CurrentRoom.PlayerCount == MaxPlayer)
        {
            //試合スタートのカウントダウン
            StartInit();
        }
        int PointNum = Random.Range(0,SpawnPoint.Length - 1);
        // ランダムな座標に自身のアバター（ネットワークオブジェクト）を生成する
        var position = SpawnPoint[PointNum].transform.position;
        CheckCG = true;
        PhotonNetwork.Instantiate("Player", position, Quaternion.identity);//フォルダにあるResourcesからPlayerという名前のオブジェクトを出す
        if (CheckCG == true)//念のためifで動かす
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            //これ↓入れないと動きが同期されるから入れてる
            Player = GameObject.FindWithTag("Player");
            MapCamera.transform.parent = Player.transform;
            Vector3 PlayerPos = Player.transform.position;
            PlayerPos.y += 23.0f;
            MapCamera.transform.position = PlayerPos;
            
            /*
            ExitGames.Client.Photon.Hashtable properties = new ExitGames.Client.Photon.Hashtable();
            properties.Add("Hp", Player.GetComponent<PlayerStatus>().hp);
            PhotonNetwork.LocalPlayer.SetCustomProperties(properties);
            */
            GameObject mainCamera = GameObject.FindWithTag("MainCamera");
            GameObject secondCamera = GameObject.FindWithTag("SecondCamera");
            DataBase = GameObject.FindWithTag("DataBase");
            DB = DataBase.GetComponent<DataBase>();
            Player.GetComponent<PlayerMovementScript>().enabled = true;
            Player.GetComponent<MouseLookScript>().enabled = true;
            Player.GetComponent<GunInventory>().enabled = true;
            Player.GetComponent<PlayerStatus>().enabled = true;
            Player.GetComponent<Taskmanagement>().enabled = true;
            Sound = Player.transform.Find("sound").gameObject;
            Sound.tag = "Setting";
            Sound.SetActive(true);
            Sound.GetComponent<SettingScript>().enabled = true;
            Sound.GetComponent<Mousesensitivity>().enabled = true;
            GameObject StatusUI = Player.transform.Find("StatusUI").gameObject;
            StatusUI.SetActive(true);
            GameObject Damage = Player.transform.Find("Damage").gameObject;
            Damage.SetActive(true);
            TakeDamage = Player.GetComponent<TakeDamage>();
            mainCamera.GetComponent<Camera>().enabled = true;
            secondCamera.GetComponent<Camera>().enabled = true;
            this.gameObject.layer = LayerMask.NameToLayer("Default");
            GameObject mappoint = Player.transform.Find("MapPoint").gameObject;
            mappoint.layer = LayerMask.NameToLayer("MapUI");
        }
    }
    public void DestroyObj()
    {
        int PointNum = Random.Range(0, SpawnPoint.Length - 1);
        // ランダムな座標に自身のアバター（ネットワークオブジェクト）を生成する
        var position = SpawnPoint[PointNum].transform.position;
        Player.transform.position = position;
        //PhotonNetwork.Destroy(Player);
    }
    public void KillEnemy()
    {
        DB.KillSetting++;
    }
    public string GetMyName()
    {
        return DB.NameSetting;
    }
    public void ReturnLose()
    {
        if (GameTime || GameStart)
        {
            DB.LoseSetting++;
        }
    }
    public void TaskClear(int num)
    {
        DB.TaskPointSetting = DB.TaskPointSetting + num;
    }
    public  void TaskNum()
    {
        DB.TaskSetting++;
    }
}