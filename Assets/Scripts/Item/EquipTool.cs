using System.Collections;
using System.Collections.Generic;
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
    public bool doesDealDamager;
    public int damage;

    private Animator animator;
    private Camera _camera;
    private Vector3 screenPoint;
    private LayerMask canHitLayers;

    void Start()
    {
        animator = GetComponent<Animator>();
        _camera = Camera.main;
        
        screenPoint = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        
        canHitLayers = LayerMask.GetMask("CanHit");
    }

    public override void OnAttackInput()
    {
        if (!attacking)
        {
            if (CharacterManager.Instance.Player.condition.UseStamina(useStamina))
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

    public void OnHit()
    {
        // TODO : 공격 및 자원채집 대상에 CanHit 레이어 닷것
        Ray ray = _camera.ScreenPointToRay(screenPoint);
        if (Physics.Raycast(ray, out RaycastHit hit, attackDistance, canHitLayers))
        {
            if (doesGatherResources && hit.collider.TryGetComponent(out Resource resource))
            {
                resource.Gather(hit.point, hit.normal);
            }

            if (doesDealDamager && hit.collider.TryGetComponent(out IDamagable damagable))
            {
                damagable.TakePhysicalDamage(damage);
            }
        }
    }
}
