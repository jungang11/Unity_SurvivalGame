using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Animal : MonoBehaviour
{
    protected StatusController thePlayerStatus;

    [SerializeField]
    protected string animalName; // 동물의 이름
    [SerializeField]
    protected int hp; // 동물의 체력
    [SerializeField]
    protected int inithp; 

    [SerializeField]
    protected float walkSpeed; // 걷기 스피드
    [SerializeField]
    protected float runSpeed;// 뛰기 스피드

    protected Vector3 destination; // 목적지

    //상태변수
    protected bool isAction;// 행동 중인지 아닌지 판별
    protected bool isWalking; // 걷는지 안 걷는지 판별.
    protected bool isRunning; // 뛰는지 판별.
    protected bool isChasing; // 추격중인지 판별
    protected bool isAttacking; // 공격중인지 판별.
    [HideInInspector]
    public bool isDead; // 죽었는지 판별
    // FieldOfViewAngle에서 사용하기 위해 public 사용

    [SerializeField]
    protected float walkTime; // 걷기 시간
    [SerializeField]
    protected float waitTime; // 대기 시간
    [SerializeField]
    protected float runTime; // 뛰기 시간

    protected float currentTime;

    public GameObject hpBarPrefab;
    public Vector3 hpBarOffset = new Vector3(0, 4f, 0);
    private Canvas uiCanvas;
    private Image hpBarImage;

    // 필요한 컴포넌트
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
        Debug.Log("걷기");
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

    //죽으면 돼지 사라지게
    protected virtual void Dead()
    {
        PlaySE(sound_Dead);
        isWalking = false;
        isRunning = false;
        isDead = true;
        nav.speed = 0; // 그 자리에 멈춤
        anim.SetTrigger("Dead");
        hpBarImage.GetComponentsInParent<Image>()[1].color = Color.clear;
    }

    protected void RandomSound()
    {
        int _random = Random.Range(0, 3); // 일상 사운드 3개.
        PlaySE(sound_Normal[_random]);
    }

    protected void PlaySE(AudioClip _clip)
    {
        theAudio.clip = _clip;
        theAudio.Play();
    }
}
