using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Animal : MonoBehaviour
{
    protected StatusController thePlayerStatus;

    [SerializeField]
    protected string animalName; // ������ �̸�
    [SerializeField]
    protected int hp; // ������ ü��
    [SerializeField]
    protected int inithp; 

    [SerializeField]
    protected float walkSpeed; // �ȱ� ���ǵ�
    [SerializeField]
    protected float runSpeed;// �ٱ� ���ǵ�

    protected Vector3 destination; // ������

    //���º���
    protected bool isAction;// �ൿ ������ �ƴ��� �Ǻ�
    protected bool isWalking; // �ȴ��� �� �ȴ��� �Ǻ�.
    protected bool isRunning; // �ٴ��� �Ǻ�.
    protected bool isChasing; // �߰������� �Ǻ�
    protected bool isAttacking; // ���������� �Ǻ�.
    [HideInInspector]
    public bool isDead; // �׾����� �Ǻ�
    // FieldOfViewAngle���� ����ϱ� ���� public ���

    [SerializeField]
    protected float walkTime; // �ȱ� �ð�
    [SerializeField]
    protected float waitTime; // ��� �ð�
    [SerializeField]
    protected float runTime; // �ٱ� �ð�

    protected float currentTime;

    public GameObject hpBarPrefab;
    public Vector3 hpBarOffset = new Vector3(0, 4f, 0);
    private Canvas uiCanvas;
    private Image hpBarImage;

    // �ʿ��� ������Ʈ
    [SerializeField]
    protected Animator anim;
    [SerializeField]
    protected Rigidbody rigid;
    [SerializeField]
    protected BoxCollider boxCol;
    protected AudioSource theAudio;
    protected NavMeshAgent nav;
    protected FieldOfViewAngle theViewAngle;

    [SerializeField]
    protected AudioClip[] sound_Normal;
    [SerializeField]
    protected AudioClip sound_Hurt;
    [SerializeField]
    protected AudioClip sound_Dead;
    void Start()
    {
        thePlayerStatus = FindObjectOfType<StatusController>();
        theViewAngle = GetComponent<FieldOfViewAngle>();
        nav = GetComponent<NavMeshAgent>();
        theAudio = GetComponent<AudioSource>();
        currentTime = waitTime;
        isAction = true;
        SetHpBar();
    }


    // Update is called once per frame
    protected virtual void Update()
    {
        if (!isDead)
        {
            Move();
            //Rotation();
            ElapseTime();
        }
    }

    void SetHpBar()
    {
        uiCanvas = GameObject.Find("HpBar Canvas").GetComponent<Canvas>();
        GameObject hpBar = Instantiate<GameObject>(hpBarPrefab, uiCanvas.transform);
        hpBarImage = hpBar.GetComponentsInChildren<Image>()[1];
        hpBarImage.GetComponentsInParent<Image>()[0].color = Color.clear;
        hpBarImage.GetComponentsInParent<Image>()[1].color = Color.clear;
        var _hpbar = hpBar.GetComponent<EnemyHpBar>();
        _hpbar.targetTr = this.gameObject.transform;
        _hpbar.offset = hpBarOffset;
    }

    protected void Move()
    {
        if (isWalking || isRunning)
            //rigid.MovePosition(transform.position + (transform.forward * applySpeed * Time.deltaTime));
            nav.SetDestination(transform.position + destination * 5f);
    }

    protected void ElapseTime()
    {
        if (isAction)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0 && !isChasing && !isAttacking)
                ReSet();
        }
    }

    protected virtual void ReSet()
    {
        isWalking = false;
        isRunning = false;
        isAction = true;
        nav.speed = walkSpeed;
        nav.ResetPath();
        anim.SetBool("Walking", isWalking);
        anim.SetBool("Running", isRunning);
        destination.Set(Random.Range(-0.2f, 0.2f), 0f, Random.Range(0.5f, 1f));
        //RandomAction();
    }

    protected void TryWalk()
    {
        isWalking = true;
        anim.SetBool("Walking", isWalking);
        currentTime = walkTime;
        nav.speed = walkSpeed;
        Debug.Log("�ȱ�");
    }

    public virtual void Damage(int _dmg, Vector3 _targetPos)
    {
        if (!isDead)
        {
            hp -= _dmg;
            hpBarImage.fillAmount = (float)hp/inithp;
            hpBarImage.GetComponentsInParent<Image>()[0].color = Color.red;
            hpBarImage.GetComponentsInParent<Image>()[1].color = Color.white;
            if (hp <= 0)
            {
                Dead();
                
                return;
            }

            PlaySE(sound_Hurt);
            anim.SetTrigger("Hurt");
        }
    }

    //������ ���� �������
    protected virtual void Dead()
    {
        PlaySE(sound_Dead);
        isWalking = false;
        isRunning = false;
        isDead = true;
        nav.speed = 0; // �� �ڸ��� ����
        anim.SetTrigger("Dead");
        hpBarImage.GetComponentsInParent<Image>()[1].color = Color.clear;
    }

    protected void RandomSound()
    {
        int _random = Random.Range(0, 3); // �ϻ� ���� 3��.
        PlaySE(sound_Normal[_random]);
    }

    protected void PlaySE(AudioClip _clip)
    {
        theAudio.clip = _clip;
        theAudio.Play();
    }
}
