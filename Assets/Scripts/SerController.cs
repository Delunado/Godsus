using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerController : MonoBehaviour
{
    private StateBaseSer currentState;

    public RuntimeAnimatorController animPossesP1;
    public RuntimeAnimatorController animPossesP2;
    public RuntimeAnimatorController animUnposses;

    [HideInInspector] public AudioSource audioSource;
    public AudioClip audioClip;

    [SerializeField] private HealthControllerSer health;

    [SerializeField] public Ability ability1;
    [SerializeField] public Ability ability2;

    [HideInInspector] public SpriteRenderer abilitySprite1;
    [HideInInspector] public SpriteRenderer abilitySprite2;

    [HideInInspector] public SpriteRenderer spriteSer;
    [HideInInspector] public Animator animator;

    Player player;
    bool possesed = false;
    public bool Possesed { get => possesed; set => possesed = value; }
    
    [HideInInspector] public PlayerInput input;
    [HideInInspector] public Rigidbody2D rb;

    [HideInInspector] public Vector2 abilityDirection;
    public GameObject aimingArrow;

    [SerializeField] private Collider2D hitbox;
    public Collider2D Hitbox { get => hitbox; set => hitbox = value; }
    [SerializeField] private GameObject shield;
    public GameObject Shield { get => shield; set => shield = value; }

    private bool inGround = true;
    public bool InGround { get => inGround; set => inGround = value; }

    private int lastDirection;
    public int LastDirection { get => lastDirection; set => lastDirection = value; }

    //Features
    public float speed = 4f;

    public float delayUnderground = 2.0f; //Cuanto dura el efecto
    public float delayReuseUnderground = 5.0f; //Cuando puedes reutilizar el efecto

    public float delayReuseFireBall = 4.0f;
    [HideInInspector] public bool canUseFireBall = true;

    public float delayReuseRay = 1.0f;
    [HideInInspector] public bool canUseRay = true;

    public float delayReboteBall = 6.0f;
    [HideInInspector] public bool canUseReboteBall = true;

    [SerializeField] private bool inShield = true;
    [HideInInspector] public bool InShield { get => inShield; set => inShield = value; }
    public HealthControllerSer Health { get => health; set => health = value; }


    public float delayShield = 2.0f; //Cuanto dura el efecto
    public float delayReuseShield = 5.0f; //Cuando puedes reutilizar el efecto

    public void SetAbilities(Ability _ability1, Ability _ability2)
    {
        ability1 = _ability1;
        ability2 = _ability2;

        abilitySprite1.sprite = _ability1.Sprite;
        abilitySprite2.sprite = _ability2.Sprite;

        ability1.SetType();
        ability2.SetType();
    }

    public void SetState(StateBaseSer state)
    {
        //Para hacer build
        if (currentState != null)
            currentState.OnStateExit();

        //Debug.Log("Estado: " + state);
        currentState = state; //Aqui cambiamos al nuevo estado

        if (currentState != null)
            currentState.OnStateEnter();
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteSer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        ability1.SetType();
        ability2.SetType();
        //currentState; Aqui que vaya solo, IA
        possesed = false;
        lastDirection = Random.Range(0, 2);
        if (lastDirection == 0)
        {
            lastDirection = 1;
        } else
        {
            lastDirection = -1;
        }
    }

    public void StartPossesion(Player player)
    {
        rb.velocity = Vector2.zero;
        this.player = player;
        input = player.GetComponent<PlayerInput>();
        SetState(new MovementSerState(this));
        if (player.playerID == 1)
            animator.runtimeAnimatorController = animPossesP1;
        else if (player.playerID == 2)
            animator.runtimeAnimatorController = animPossesP2;
        Possesed = true;
    }

    public void FinishPossesion()
    {
        animator.runtimeAnimatorController = animUnposses;    
        rb.velocity = Vector2.zero;
        Possesed = false;
    }

    private void Update()
    {
        if (Possesed)
        {
            currentState.Tick();
        }
    }

    //--------------*COROUTINES*-------------------
    public void StartCorroutineUnderGround()
    {
        StartCoroutine(TimeUnderGround());
    }

    private IEnumerator TimeUnderGround()
    {
        yield return new WaitForSeconds(delayUnderground);
        Hitbox.enabled = true;
        this.GetComponent<SpriteRenderer>().color = Color.white;

        yield return new WaitForSeconds(delayReuseUnderground);
        InGround = true;
    }

    public void StartCorroutineShield()
    {
        StartCoroutine("TimeShield");
    }

    private IEnumerator TimeShield()
    {
        yield return new WaitForSeconds(delayShield);
        Shield.SetActive(false);

        yield return new WaitForSeconds(delayReuseShield);
        InShield = true;
    }


    public void StartCorroutineFireBall()
    {
        StartCoroutine(TimeFireBall());
    }

    private IEnumerator TimeFireBall()
    {
        canUseFireBall = false;
        yield return new WaitForSeconds(delayReuseFireBall);
        canUseFireBall = true;
    }


    public void StartCorroutineRay()
    {
        StartCoroutine(TimeRay());
    }

    private IEnumerator TimeRay()
    {
        canUseRay = false;
        yield return new WaitForSeconds(delayReuseRay);
        canUseRay = true;
    }


    public void StartCorroutineReboteBall()
    {
        StartCoroutine(TimeReboteBall());
    }

    private IEnumerator TimeReboteBall()
    {
        canUseReboteBall = false;
        yield return new WaitForSeconds(delayReboteBall);
        canUseReboteBall = true;
    }
}
