using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : WeakAnimal
{
    [SerializeField]
    private GameObject thePig;
    [SerializeField]
    private float destroyTime;

    //public bool pigAlive = true; // 돼지 죽었는지 판단

    [SerializeField]
    private GameObject go_meat_item_prefab; // 고기 아이템.

    //고기 생성 최소,최대개수
    [SerializeField]
    private int minCount;
    [SerializeField]
    private int maxCount;

    protected override void ReSet()
    {
        base.ReSet();
        RandomAction();
    }

    // 돼지 사망 시 시체 사라짐
    protected override void Dead() 
    {
        for (int i = 0; i < Mathf.Round(Random.Range(minCount, maxCount)); i++)
        {
            Instantiate(go_meat_item_prefab, thePig.transform.position, Quaternion.identity);
        }
        base.Dead();
        Destroy(thePig, destroyTime);
        //pigAlive = false;
    }

    private void RandomAction()
    {
        RandomSound();
        int _random = Random.Range(0, 4); // 대기, 풀뜯기, 두리번, 걷기.

        if (_random == 0)
            Wait();
        else if (_random == 1)
            Eat();
        else if (_random == 2)
            Peek();
        else if (_random == 3)
            TryWalk();
    }

    private void Wait()
    {
        currentTime = waitTime;
        Debug.Log("대기");
    }
    private void Eat()
    {
        currentTime = waitTime;
        anim.SetTrigger("Eat");
        Debug.Log("풀뜯기");
    }
    private void Peek()
    {
        currentTime = waitTime;
        anim.SetTrigger("Peek");
        Debug.Log("두리번");
    }
}
