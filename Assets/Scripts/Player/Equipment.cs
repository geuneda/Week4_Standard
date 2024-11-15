using System.Collections;
using System.Collections.Generic;
using Item;
using UnityEngine;
using UnityEngine.InputSystem;

public class Equipment : MonoBehaviour
{
    public Equip curEquip;
    public Transform equipParent;

    private IWeaponStrategy strategy;
    private PlayerController controller;
    private PlayerCondition condition;

    private void Start()
    {
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
    }

    // 무기변경시 호출하여 현재 무기 적용
    public void SetWeapon(IWeaponStrategy newStrategy)
    {
        strategy = newStrategy;
    }
    
    // 장비 변경 예제
    public void SetWeapon()
    {
        SetWeapon(new BowStrategy());
        Attack(); // 화살 발사
        SetWeapon(new GunStrategy());
        Attack(); // 총알 발사
        SetWeapon(new MagicStrategy());
        Attack(); // 마법 공격
    }

    public void EquipNew(ItemData data)
    {
        UnEquip();
        curEquip = Instantiate(data.equipPrefab, equipParent).GetComponent<Equip>();
    }

    public void UnEquip()
    {
        if (!curEquip) return;
        Destroy(curEquip.gameObject);
        curEquip = null;
    }
    
    // 전략 패턴을 적용한 새로운 공격 시스템
    public void Attack()
    {
        if (strategy != null)
        {
            strategy.UseWeapon();
        }
        else
        {
            Debug.Log("장착된 장비가 없음.");
        }
    }
    
    public void OnAttackInput(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Performed && curEquip != null && controller.canLook)
        {
            curEquip.OnAttackInput();
        }
    }
}
