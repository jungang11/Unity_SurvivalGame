using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigWild : StrongAnimal
{
    [SerializeField]
    private GameObject thePig;
    [SerializeField]
    private float destroyTime;


    protected override void Dead()
    {
        base.Dead();
        Destroy(thePig, destroyTime);
        //pigAlive = false;
    }

    protected override void Update()
    {
        base.Update();
        if (theViewAngle.View() && !isDead && !isAttacking)
        {
            StopAllCoroutines();
            StartCoroutine(ChaseTargetCoroutine());
        }
    }

}
