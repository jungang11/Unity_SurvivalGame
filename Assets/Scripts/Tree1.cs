using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree1 : MonoBehaviour
{
    [SerializeField]
    private int hp; // ������ ü��

    [SerializeField]
    private float destroyTime;// ���� ���� �ð�

    [SerializeField]
    private CapsuleCollider parentCol; //  ��ü ĸ�� �ݶ��̴�
    [SerializeField]
    private CapsuleCollider childCol; //  �� ���� ĸ�� �ݶ��̴�

    [SerializeField]
    private Rigidbody rig; // ����


    //�ʿ��� ���� ������Ʈ
    [SerializeField]
    private GameObject go_tree; // �Ϲ� ����
    [SerializeField]
    private GameObject go_stump;// �ص��� ���� ����
    //[SerializeField]
    //private GameObject go_effect_prefabs; //ä�� ����Ʈ;
    [SerializeField]
    private GameObject go_tree_item_prefab; // ���� ������.
    [SerializeField]
    private float force;

    //������ ���� �ּ�,�ִ밳��
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
