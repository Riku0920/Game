using System.Collections;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// �G�̏�ԊǗ��X�N���v�g
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyStatus : MobStatus
{
    private NavMeshAgent _agent;

    protected override void Start()
    {
        base.Start();

        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // NavMeshAgent��velocity�ňړ����x�̃x�N�g�����擾�ł���
        _animator.SetFloat("Speed", _agent.velocity.magnitude);
    }

    protected override void OnDie()
    {
        base.OnDie();
        StartCoroutine(DestroyCoroutine());
    }

    /// <summary>
    /// �|���ꂽ���̏��ŃR���[�`���ł��B
    /// </summary>
    /// <returns></returns>
    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
}