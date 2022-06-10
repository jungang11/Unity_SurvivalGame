using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree1 : MonoBehaviour
{
    [SerializeField]
    private int hp; // 나무의 체력

    [SerializeField]
    private float destroyTime;// 파편 제거 시간

    [SerializeField]
    private CapsuleCollider parentCol; //  전체 캡슐 콜라이더
    [SerializeField]
    private CapsuleCollider childCol; //  위 나무 캡슐 콜라이더

    [SerializeField]
    private Rigidbody rig; // 물리


    //필요한 게임 오브젝트
    [SerializeField]
    private GameObject go_tree; // 일반 나무
    [SerializeField]
    private GameObject go_stump;// 밑동만 남은 나무
    //[SerializeField]
    //private GameObject go_effect_prefabs; //채굴 이펙트;
    [SerializeField]
    private GameObject go_tree_item_prefab; // 나무 아이템.
    [SerializeField]
    private float force;

    //돌멩이 생성 최소,최대개수
    [SerializeField]
    private int minCount;
    [SerializeField]
    private int maxCount;
    // Start is called before the first frame update
    public void Cutting()
    {
        //var clone = Instantiate(go_effect_prefabs, col.bounds.center, Quaternion.identity);
        //Destroy(clone, destroyTime);


        hp--;
        if (hp <= 0)

            Destruction();
    }
    private void Destruction()
    {
        parentCol.enabled = false;
        childCol.enabled = true;
        rig.useGravity = true;

        rig.AddForce(Random.Range(-force, force), 0f, Random.Range(-force, force));

        for (int i = 0; i < Mathf.Round(Random.Range(minCount, maxCount)); i++)
        {
            Instantiate(go_tree_item_prefab, go_tree.transform.position, Quaternion.identity);
        }


        Destroy(go_tree, destroyTime);
    }
}
