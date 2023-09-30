using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCManager : MonoBehaviour
{
    public enum State
    {
        Wait,
        Walk,
        Talk
    }

    //�@�ړI�n
    private Vector3 destination;
    //�@���񂷂�ʒu�̐e
    [SerializeField]
    private Transform patrolPointsParent = null;
    //�@���񂷂�ʒu
    private Transform[] patrolPositions;
    //�@���ɏ��񂷂�ʒu
    private int nowPatrolPosition = 0;
    //�@�G�[�W�F���g
    private NavMeshAgent navMeshAgent;
    //�@�A�j���[�^�[
    private Animator animator;
    //�@���l�̏��
    private State state;
    //�@�ҋ@��������
    private float elapsedTime;
    //�@�ҋ@���鎞��
    [SerializeField]
    private float waitTime = 5f;

    void OnEnable()
    {

    }

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        //�@����n�_��ݒ�
        patrolPositions = new Transform[patrolPointsParent.transform.childCount];
        for (int i = 0; i < patrolPointsParent.transform.childCount; i++)
        {
            patrolPositions[i] = patrolPointsParent.transform.GetChild(i);
        }
        SetState(State.Wait);
    }

    void Update()
    {
        //�@�����
        if (state == State.Walk)
        {
            //�@�G�[�W�F���g�̐��ݓI�ȑ�����ݒ�
            animator.SetFloat("Speed", navMeshAgent.desiredVelocity.magnitude);

            //�@�ړI�n�ɓ����������ǂ����̔���
            if (navMeshAgent.remainingDistance < 0.1f)
            {
                SetState(State.Wait);
            }
            //�@�������Ă������莞�ԑ҂�
        }
        else if (state == State.Wait)
        {
            elapsedTime += Time.deltaTime;

            //�@�҂����Ԃ��z�����玟�̖ړI�n��ݒ�
            if (elapsedTime > waitTime)
            {
                SetState(State.Walk);
            }
        }
    }

    //�@���l�̏�ԕύX
    public void SetState(State state, Transform conversationPartnerTransform = null)
    {
        this.state = state;
        if (state == State.Wait)
        {
            elapsedTime = 0f;
            animator.SetFloat("Speed", 0f);
        }
        else if (state == State.Walk)
        {
            SetNextPosition();
            //            navMeshAgent.SetDestination(GetDestination());
            navMeshAgent.isStopped = false;
            navMeshAgent.destination = destination;
           
        }
    }

    //�@����n�_�����Ɏ���
    public void SetNextPosition()
    {
        SetDestination(patrolPositions[nowPatrolPosition].position);
        nowPatrolPosition++;
        if (nowPatrolPosition == patrolPositions.Length)
        {
            nowPatrolPosition = 0;
        }
    }
    //�@�ړI�n��ݒ肷��
    public void SetDestination(Vector3 position)
    {
        destination = position;
    }

    //�@�ړI�n���擾����
    public Vector3 GetDestination()
    {
        return destination;
    }
}
