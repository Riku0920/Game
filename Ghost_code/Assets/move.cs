//
// Mecanimのアニメーションデータが、原点で移動しない場合の Rigidbody付きコントローラ
// サンプル
// 2014/03/13 N.Kobyasahi
//
using UnityEngine;
using System.Collections;

	// 必要なコンポーネントの列記
	[RequireComponent(typeof(Animator))]
	[RequireComponent(typeof(CapsuleCollider))]
	[RequireComponent(typeof(Rigidbody))]

	public class move : MonoBehaviour
	{

		public float animSpeed = 1.5f;              // アニメーション再生速度設定
		public float lookSmoother = 3.0f;           // a smoothing setting for camera motion
		public bool useCurves = true;               // Mecanimでカーブ調整を使うか設定する
													// このスイッチが入っていないとカーブは使われない
		public float useCurvesHeight = 0.5f;        // カーブ補正の有効高さ（地面をすり抜けやすい時には大きくする）

		// 以下キャラクターコントローラ用パラメタ
		// 前進速度
		public float forwardSpeed = 7.0f;
		// 後退速度
		public float backwardSpeed = 7.0f;
		// 旋回速度
		public float rotateSpeed = 2.0f;
		// キャラクターコントローラ（カプセルコライダ）の参照
		private CapsuleCollider col;
		private Rigidbody rb;
		// キャラクターコントローラ（カプセルコライダ）の移動量
		private Vector3 zvelocity;
		private Vector3 xvelocity;
		private Animator anim;                          // キャラにアタッチされるアニメーターへの参照
		private AnimatorStateInfo currentBaseState;         // base layerで使われる、アニメーターの現在の状態の参照
															// CapsuleColliderで設定されているコライダのHeiht、Centerの初期値を収める変数
		private float orgColHight;
		private Vector3 orgVectColCenter;

		private GameObject cameraObject;    // メインカメラへの参照

		// アニメーター各ステートへの参照
		static int idleState = Animator.StringToHash("Base Layer.Idle");
		static int locoState = Animator.StringToHash("Base Layer.Locomotion");
		static int jumpState = Animator.StringToHash("Base Layer.Jump");
		static int restState = Animator.StringToHash("Base Layer.Rest");


		// 初期化
		void Start()
		{
			// Animatorコンポーネントを取得する
			anim = GetComponent<Animator>();
			//CapsuleColliderコンポーネントを取得する（カプセル型コリジョン）
			col = GetComponent<CapsuleCollider>();
			rb = GetComponent<Rigidbody>();
			//メインカメラを取得する
			cameraObject = GameObject.FindWithTag("MainCamera");
			// CapsuleColliderコンポーネントのHeight、Centerの初期値を保存する
			orgColHight = col.height;
			orgVectColCenter = col.center;
		}


		// 以下、メイン処理.リジッドボディと絡めるので、FixedUpdate内で処理を行う.
		void FixedUpdate()
		{
			float h = Input.GetAxis("Horizontal");              // 入力デバイスの水平軸をhで定義
			float v = Input.GetAxis("Vertical");                // 入力デバイスの垂直軸をvで定義
			anim.SetFloat("Speed", v);                          // Animator側で設定している"Speed"パラメタにvを渡す
			anim.SetFloat("Direction", h);                      // Animator側で設定している"Direction"パラメタにhを渡す
			anim.speed = animSpeed;                             // Animatorのモーション再生速度に animSpeedを設定する

			// 以下、キャラクターの移動処理
			zvelocity = new Vector3(0, 0, v);        // 上下のキー入力からZ軸方向の移動量を取得
													// キャラクターのローカル空間での方向に変換
			zvelocity = transform.TransformDirection(zvelocity);
			//以下のvの閾値は、Mecanim側のトランジションと一緒に調整する
			if (v > 0.1)
			{
				zvelocity *= forwardSpeed;       // 移動速度を掛ける
			}
			else if (v < -0.1)
			{
				zvelocity *= backwardSpeed;  // 移動速度を掛ける
			}

			// 以下、キャラクターの移動処理
			xvelocity = new Vector3(h, 0, 0);        // 上下のキー入力からZ軸方向の移動量を取得
													 // キャラクターのローカル空間での方向に変換
			xvelocity = transform.TransformDirection(xvelocity);
			//以下のvの閾値は、Mecanim側のトランジションと一緒に調整する
			if (h > 0.1)
			{
				xvelocity *= forwardSpeed;       // 移動速度を掛ける
			}
			else if (h < -0.1)
			{
				xvelocity *= backwardSpeed;  // 移動速度を掛ける
			}



			// 上下のキー入力でキャラクターを移動させる
			transform.localPosition += zvelocity * Time.fixedDeltaTime;

			transform.localPosition += xvelocity * Time.fixedDeltaTime;
			// 左右のキー入力でキャラクタをY軸で旋回させる
			//transform.Rotate(0, h * rotateSpeed, 0);


			// 以下、Animatorの各ステート中での処理
			// Locomotion中
			// 現在のベースレイヤーがlocoStateの時
			if (currentBaseState.nameHash == locoState)
			{
				//カーブでコライダ調整をしている時は、念のためにリセットする
				if (useCurves)
				{
					resetCollider();
				}
			}
			// IDLE中の処理
			// 現在のベースレイヤーがidleStateの時
			else if (currentBaseState.nameHash == idleState)
			{
				//カーブでコライダ調整をしている時は、念のためにリセットする
				if (useCurves)
				{
					resetCollider();
				}
				// スペースキーを入力したらRest状態になる
				if (Input.GetButtonDown("Jump"))
				{
					anim.SetBool("Rest", true);
				}
			}
			// REST中の処理
			// 現在のベースレイヤーがrestStateの時
			else if (currentBaseState.nameHash == restState)
			{
				//cameraObject.SendMessage("setCameraPositionFrontView");		// カメラを正面に切り替える
				// ステートが遷移中でない場合、Rest bool値をリセットする（ループしないようにする）
				if (!anim.IsInTransition(0))
				{
					anim.SetBool("Rest", false);
				}
			}
		}

		void OnGUI()
		{
			GUI.Box(new Rect(Screen.width - 260, 10, 250, 150), "Interaction");
			GUI.Label(new Rect(Screen.width - 245, 30, 250, 30), "Up/Down Arrow : Go Forwald/Go Back");
			GUI.Label(new Rect(Screen.width - 245, 50, 250, 30), "Left/Right Arrow : Turn Left/Turn Right");
			GUI.Label(new Rect(Screen.width - 245, 70, 250, 30), "Hit Space key while Running : Jump");
			GUI.Label(new Rect(Screen.width - 245, 90, 250, 30), "Hit Spase key while Stopping : Rest");
			GUI.Label(new Rect(Screen.width - 245, 110, 250, 30), "Left Control : Front Camera");
			GUI.Label(new Rect(Screen.width - 245, 130, 250, 30), "Alt : LookAt Camera");
		}


		// キャラクターのコライダーサイズのリセット関数
		void resetCollider()
		{
			// コンポーネントのHeight、Centerの初期値を戻す
			col.height = orgColHight;
			col.center = orgVectColCenter;
		}
	}