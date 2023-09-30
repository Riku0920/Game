using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyStatus))]
public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private GameObject discover = null;

    private NavMeshAgent _agent;
    private RaycastHit[] _raycastHits = new RaycastHit[10];
    private EnemyStatus _status;

    private Vector3 destination;
    //　プレイヤーTransform
    private Transform playerTransform;
    [SerializeField]
    private float TargetPoison = 5f;
    [SerializeField]
    private float AttackRange = 3f;//攻撃される距離

    [SerializeField]
    float time;//経過時間
    [SerializeField]
    float TimeLimit;//制限時間

    [SerializeField]
    private GameObject gameOver;
    bool isCalledOnce = false;


    [SerializeField]
    private float staminum = 100f;

    public GameObject StaminumSlider;
    public Slider Slider;
    private void Start()
    {
        discover.SetActive(false);
        _agent = GetComponent<NavMeshAgent>(); // NavMeshAgentを保持しておく
        _status = GetComponent<EnemyStatus>();
        Slider = StaminumSlider.transform.Find("StaminumSlider").GetComponent<Slider>();
        Slider.value = staminum;
    }

    public void OnDetectObject(Collider collider)
    {

        if (!_status.IsMovable)
        {
            discover.SetActive(false);
            _agent.isStopped = true;
            return;
        }

        
        

        // 検知したオブジェクトに「Player」のタグがついていれば、そのオブジェクトを追いかける
        if (collider.CompareTag("Player"))
        {
            var positionDiff = collider.transform.position - transform.position; // 自身とプレイヤーの座標差分を計算
            var distance = positionDiff.magnitude; // プレイヤーとの距離を計算
            var direction = positionDiff.normalized; // プレイヤーへの方向
            

            // _raycastHitsに、ヒットしたColliderや座標情報などが格納される
            // RaycastAllとRaycastNonAllocは同等の機能だが、RaycastNonAllocだとメモリにゴミが残らないのでこちらを推奨
            var hitCount = Physics.RaycastNonAlloc(transform.position, direction, _raycastHits, distance);
            playerTransform = collider.transform;
            SetDestination(playerTransform.position);
            //Debug.Log("hitCount: " + hitCount);
            if (Vector3.Distance(transform.position, GetDestination()) < AttackRange)//　攻撃する距離だったら攻撃
            {
                //Debug.Log("攻撃");//今はデバックしか入れていない。後で攻撃モーションを入れる
                Slider.value -= 0.002f;
                if (Slider.value == 0)
                {
                    if (!isCalledOnce)
                    {
                        Instantiate(gameOver);
                        isCalledOnce = true;
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                    }
                    
                }
                
                    
            }
            if (Vector3.Distance(transform.position, GetDestination()) < TargetPoison)//　プレイヤーの近くに到着したかどうかの判定
            {
                _agent.isStopped = true;
                _agent.destination = gameObject.transform.position;//止まる。
                //Debug.Log("到着");
            }
            else if (hitCount == 1)
            {
                // 本作のプレイヤーはCharacterControllerを使っていて、Colliderは使っていないのでRaycastはヒットしない
                // つまり、ヒット数が0であればプレイヤーとの間に障害物が無いということになる
                discover.SetActive(true);
                _agent.isStopped = false;
                _agent.destination = collider.transform.position;


            }
            
        }
    }
    public void SetDestination(Vector3 position)
    {
        destination = position;
    }
    public Vector3 GetDestination()
    {
        return destination;
    }
}