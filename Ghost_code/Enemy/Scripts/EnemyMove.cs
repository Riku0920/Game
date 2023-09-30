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
    //�@�v���C���[Transform
    private Transform playerTransform;
    [SerializeField]
    private float TargetPoison = 5f;
    [SerializeField]
    private float AttackRange = 3f;//�U������鋗��

    [SerializeField]
    float time;//�o�ߎ���
    [SerializeField]
    float TimeLimit;//��������

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
        _agent = GetComponent<NavMeshAgent>(); // NavMeshAgent��ێ����Ă���
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

        
        

        // ���m�����I�u�W�F�N�g�ɁuPlayer�v�̃^�O�����Ă���΁A���̃I�u�W�F�N�g��ǂ�������
        if (collider.CompareTag("Player"))
        {
            var positionDiff = collider.transform.position - transform.position; // ���g�ƃv���C���[�̍��W�������v�Z
            var distance = positionDiff.magnitude; // �v���C���[�Ƃ̋������v�Z
            var direction = positionDiff.normalized; // �v���C���[�ւ̕���
            

            // _raycastHits�ɁA�q�b�g����Collider����W���Ȃǂ��i�[�����
            // RaycastAll��RaycastNonAlloc�͓����̋@�\�����ARaycastNonAlloc���ƃ������ɃS�~���c��Ȃ��̂ł�����𐄏�
            var hitCount = Physics.RaycastNonAlloc(transform.position, direction, _raycastHits, distance);
            playerTransform = collider.transform;
            SetDestination(playerTransform.position);
            //Debug.Log("hitCount: " + hitCount);
            if (Vector3.Distance(transform.position, GetDestination()) < AttackRange)//�@�U�����鋗����������U��
            {
                //Debug.Log("�U��");//���̓f�o�b�N��������Ă��Ȃ��B��ōU�����[�V����������
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
            if (Vector3.Distance(transform.position, GetDestination()) < TargetPoison)//�@�v���C���[�̋߂��ɓ����������ǂ����̔���
            {
                _agent.isStopped = true;
                _agent.destination = gameObject.transform.position;//�~�܂�B
                //Debug.Log("����");
            }
            else if (hitCount == 1)
            {
                // �{��̃v���C���[��CharacterController���g���Ă��āACollider�͎g���Ă��Ȃ��̂�Raycast�̓q�b�g���Ȃ�
                // �܂�A�q�b�g����0�ł���΃v���C���[�Ƃ̊Ԃɏ�Q���������Ƃ������ƂɂȂ�
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