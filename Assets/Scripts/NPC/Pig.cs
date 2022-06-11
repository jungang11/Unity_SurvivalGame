using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : WeakAnimal
{
    [SerializeField]
    private GameObject thePig;
    [SerializeField]
    private float destroyTime;

    //public bool pigAlive = true; // ���� �׾����� �Ǵ�

    [SerializeField]
    private GameObject go_meat_item_prefab; // ���� ������.

    //���� ���� �ּ�,�ִ밳��
    [SerializeField]
    private int minCount;
    [SerializeField]
    private int maxCount;

    protected override void ReSet()
    {
        base.ReSet();
        RandomAction();
    }

    // ���� ��� �� ��ü �����
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
        int _random = Random.Range(0, 4); // ���, Ǯ���, �θ���, �ȱ�.

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
        Debug.Log("���");
    }
    private void Eat()
    {
        currentTime = waitTime;
        anim.SetTrigger("Eat");
        Debug.Log("Ǯ���");
    }
    private void Peek()
    {
        currentTime = waitTime;
        anim.SetTrigger("Peek");
        Debug.Log("�θ���");
    }
}
