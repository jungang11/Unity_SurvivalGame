using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : CloseWeaponController
{
    //활성화 여부
    public static bool isActivate = false;

    // Update is called once per frame
    void Update()
    {
        if (isActivate && !GameManager.isWater)
        {
            TryAttack();
        }

    }
    protected override IEnumerator HitCoroutine()
    {
        while (isSwing)
        {
            if (CheckObject())
            {
                if (hitInfo.transform.tag == "WeakAnimal")
                {
                    hitInfo.transform.GetComponent<WeakAnimal>().Damage(currentCloseWeapon.damage, transform.position);
                }
                //충돌했음
                isSwing = false;
                Debug.Log(hitInfo.transform.name);

            }
            yield return null;
        }
    }

    public override void CloseWeaponChange(CloseWeapon _closeWeapon)
    {
        base.CloseWeaponChange(_closeWeapon);
        isActivate = true;
    }
}
