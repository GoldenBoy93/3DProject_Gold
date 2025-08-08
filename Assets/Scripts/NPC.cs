using System.Collections;
using UnityEngine;
using UnityEngine.AI;

// AI ���¸� �����ϱ� ���� enum ����
public enum AIState
{
    Idle,
    Wandering,
    Attacking
}

public class NPC : MonoBehaviour, IDamagable
{
    [Header("Stats")]
    public int health;
    public float walkSpeed;
    public float runSpeed;
    public ItemData[] dropOnDeath;

    [Header("AI")]
    private NavMeshAgent agent;
    public float detectDistance;
    private AIState aiState;
    public bool aggro; // ��������

    // Wandering ���¿� �ʿ��� ����
    // min-max ������ ��� �ð����� min-max ������ �Ÿ��� �ִ� 
    // ������ ������ ���ƴٴϴ� ����� ���� �� �ʿ��� ������
    [Header("Wandering")]
    public float minWanderDistance;
    public float maxWanderDistance;
    public float minWanderWaitTime;
    public float maxWanderWaitTime;

    // Attacking ���¿� �ʿ��� ������
    [Header("Combat")]
    public int damage;
    public float attackRate;
    private float lastAttackTime;
    public float attackDistance;

    private float playerDistance;     // player���� �Ÿ��� ��� �� ����

    public float fieldOfView = 120f;

    private Animator animator;
    // NPC���� meshRenderer�� ��Ƶ� �迭 �� ���� ���� �� �� ���� ����
    private SkinnedMeshRenderer[] meshRenderers;

    private void Awake()
    {
        // �ʱ�ȭ
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    private void Start()
    {
        // ���� ���´� Wandering���� ����
        SetState(AIState.Wandering);
    }


    private void Update()
    {
        // player���� �Ÿ��� �� �����Ӹ��� ���
        playerDistance = Vector3.Distance(transform.position, GameManager.Instance.Player.transform.position);

        animator.SetBool("Moving", aiState != AIState.Idle);

        switch (aiState)
        {
            case AIState.Idle:
                PassiveUpdate();
                break;
            case AIState.Wandering:
                PassiveUpdate();
                break;
            case AIState.Attacking:
                AttackingUpdate();
                break;
        }
    }

    // ���¿� ���� agent�� �̵��ӵ�, �������θ� ����
    private void SetState(AIState state)
    {
        aiState = state;

        switch (aiState)
        {
            case AIState.Idle:
                agent.speed = walkSpeed;
                agent.isStopped = true;
                break;
            case AIState.Wandering:
                agent.speed = walkSpeed;
                agent.isStopped = false;
                break;
            case AIState.Attacking:
                agent.speed = runSpeed;
                agent.isStopped = false;
                break;
        }

        // �⺻ ��(walkSpeed)�� ���� ������ �缳��
        animator.speed = agent.speed / walkSpeed;
    }

    void PassiveUpdate()
    {
        // Wandering �����̰�, ��ǥ�� ������ ���� �� ���� ��
        if (aiState == AIState.Wandering && agent.remainingDistance < 0.1f)
        {
            SetState(AIState.Idle);
            Invoke("WanderToNewLocation", Random.Range(minWanderWaitTime, maxWanderWaitTime));
        }

        // �÷��̾���� �Ÿ��� ���� ���� �ȿ� ���� ��
        if (aggro == true && playerDistance < detectDistance)
        {
            SetState(AIState.Attacking);
        }
    }

    // ���ο� Wander ��ǥ���� ã��
    void WanderToNewLocation()
    {
        if (aiState != AIState.Idle) return;

        SetState(AIState.Wandering);
        agent.SetDestination(GetWanderLocation());
    }

    Vector3 GetWanderLocation()
    {
        NavMeshHit hit;

        // Unity ���Ĺ��� Ȯ���ؼ� �� �Ű������� ��� ��ȯ ������ ã�ƺ���
        // (���Ĺ��� ��ũ)
        NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)), out hit, maxWanderDistance, NavMesh.AllAreas);

        // ���ϴ� ���� �ȳ����� ��
        int i = 0;
        while (Vector3.Distance(transform.position, hit.position) < detectDistance)
        {
            NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)), out hit, maxWanderDistance, NavMesh.AllAreas);
            i++;
            if (i == 30) break;
        }

        return hit.position;
    }

    void AttackingUpdate()
    {
        // �÷��̾���� �Ÿ��� ���ݹ��� �ȿ� �ְ� �þ߰� �ȿ� ���� ��
        if (playerDistance < attackDistance && IsPlayerInFieldOfView())
        {
            agent.isStopped = true;
            if (Time.time - lastAttackTime > attackRate)
            {
                lastAttackTime = Time.time;
                GameManager.Instance.Player.controller.GetComponent<IDamagable>().TakePhysicalDamage(damage);
                animator.speed = 1;
                animator.SetTrigger("Attack");
            }
        }
        else
        {
            // ���ݹ��� �ȿ��� ������ �������� �ȿ��� ���� ��
            if (playerDistance < detectDistance)
            {
                agent.isStopped = false;
                NavMeshPath path = new NavMeshPath();
                if (agent.CalculatePath(GameManager.Instance.Player.transform.position, path))
                {
                    agent.SetDestination(GameManager.Instance.Player.transform.position);
                }
                else
                {
                    agent.SetDestination(transform.position);
                    agent.isStopped = true;
                    SetState(AIState.Wandering);
                }
            }
            // �������� ������ ������ ��
            else
            {
                agent.SetDestination(transform.position);
                agent.isStopped = true;
                SetState(AIState.Wandering);
            }
        }
    }

    // �÷��̾ NPC�� �þ߰� �ȿ� �ִ��� Ȯ���ϴ� �Լ�
    bool IsPlayerInFieldOfView()
    {
        // ���� ���ϱ� (Ÿ�� - �� ��ġ) -- ��
        Vector3 directionToPlayer = GameManager.Instance.Player.transform.position - transform.position;
        // �� ���� ����� �� ������ ���� ���ϱ�
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        // ������ �þ߰��� 1/2 ���� �۴ٸ� �þ߰� �ȿ� �ִ� ��.
        // �þ߰�(ex.120��) = �� ���� �������� �¿�� 60����
        return angle < fieldOfView * 0.5f;
    }

    public void TakePhysicalDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
            Die();

        StartCoroutine(DamageFlash());
    }

    void Die()
    {
        for (int i = 0; i < dropOnDeath.Length; i++)
        {
            Instantiate(dropOnDeath[i].dropPrefab, transform.position + Vector3.up * 2, Quaternion.identity);
        }

        Destroy(gameObject);
        Debug.Log($"{gameObject.name} has died.");
    }

    IEnumerator DamageFlash()
    {
        for (int i = 0; i < meshRenderers.Length; i++)
            meshRenderers[i].material.color = new Color(1.0f, 0.6f, 0.6f);

        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < meshRenderers.Length; i++)
            meshRenderers[i].material.color = Color.white;
    }
}
