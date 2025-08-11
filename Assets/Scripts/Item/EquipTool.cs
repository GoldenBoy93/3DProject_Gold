using UnityEngine;

public class EquipTool : Equip
{
    public float attackRate;
    private bool attacking;
    public float attackDistance;
    public float useStamina;

    [Header("Resource Gathering")]
    public bool doesGatherResources;

    [Header("Combat")]
    public bool doesDealDamage;
    public int damage;

    private Animator animator;
    private Camera weqponCamera;

    void Start()
    {
        animator = GetComponent<Animator>();
        weqponCamera = Camera.main;
    }

    public override void OnAttackInput()
    {
        if (!attacking)
        {
            // �÷��̾� ���¹̳ʰ� �Һ�� �� �ִ��� Ȯ��(useStamina�� 0���� ū��)
            if (GameManager.Instance.Player.condition.UseStamina(useStamina))
            {
                attacking = true;
                animator.SetTrigger("Attack");
                Invoke("OnCanAttack", attackRate);
            }
        }
    }

    void OnCanAttack()
    {
        attacking = false;
    }

    // �ִϸ��̼� �̺�Ʈ ���� ������ �ν����� Ȯ���ϸ� EquipTool.OnHit()�� �����س��� ��.
    public void OnHit()
    {
        Ray ray = weqponCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, attackDistance))
        {
            if (doesGatherResources && hit.collider.TryGetComponent(out Resource resource))
            {
                resource.Gather(hit.point, hit.normal);
            }

            if (doesDealDamage && hit.collider.TryGetComponent(out NPC npc))
            {
                npc.TakePhysicalDamage(damage);
            }
        }
    }
}