using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : WeakAnimal
{
    [SerializeField]
    private GameObject thePig;
    [SerializeField]
    private float destroyTime;

    //public bool pigAlive = true; // µÅÁö Á×¾ú´ÂÁö ÆÇ´Ü

    protected override void ReSet()
    {
        base.ReSet();
        RandomAction();
    }

    // µÅÁö »ç¸Á ½Ã ½ÃÃ¼ »ç¶óÁü
    protected override void Dead() 
    {
        base.Dead();
        Destroy(thePig, destroyTime);
        //pigAlive = false;
    }

    private void RandomAction()
    {
        RandomSound();
        int _random = Random.Range(0, 4); // ´ë±â, Ç®¶â±â, µÎ¸®¹ø, °È±â.

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
        Debug.Log("´ë±â");
    }
    private void Eat()
    {
        currentTime = waitTime;
        anim.SetTrigger("Eat");
        Debug.Log("Ç®¶â±â");
    }
    private void Peek()
    {
        currentTime = waitTime;
        anim.SetTrigger("Peek");
        Debug.Log("µÎ¸®¹ø");
    }
}
