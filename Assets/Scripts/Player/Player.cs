using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private StateBase currentState;
    [HideInInspector] public AudioSource audioSource;
    public AudioClip audioClip;
    public SpriteRenderer spriteRenderer;

    public Transform winningPoint;

    public float speed;
    public int playerID;
    public Collider2D hitBoxCollider;
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public PlayerInput input;

    //Possesion
    [HideInInspector] public SerController selectedSer;
    [HideInInspector] public SerController possesedSer;
    [HideInInspector] public bool inPossesion;
    [HideInInspector] public bool canPosses;

    public TextMeshPro textCombination;
    [HideInInspector] public int combinationButtonsNumber = 2;

    //Abilities
    public GameObject aimingArrow;
    [HideInInspector] public Vector2 abilityDirection;
    //public Ability pushAbility;
    public Ability dashAbility;

    private bool canDash = true;
    public bool CanDash { get => canDash; set => canDash = value; }

    [HideInInspector] public Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = Vector2.zero;
        canPosses = true;
        SetState(new StartState(this));
        //pushAbility.SetType();
        dashAbility.SetType();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentState.Tick(); 
    }

    public void SetState(StateBase state)
    {
        //Para hacer build
        if (currentState != null)
            currentState.OnStateExit();

        //Debug.Log("Estado: " + state);
        currentState = state; //Aqui cambiamos al nuevo estado

        if (currentState != null)
            currentState.OnStateEnter();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<SerController>())
        {
            selectedSer = collision.GetComponent<SerController>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<SerController>())
        {
            selectedSer = null;
        }
    }

    /*CORUTINAS DELAY*/
    public void DelayPossesion()
    {
        StartCoroutine(DelayPossesionCoroutine());
    }

    private IEnumerator DelayPossesionCoroutine()
    {
        canPosses = false;

        yield return new WaitForSeconds(0.5f);

        canPosses = true;
    }


    public void DelayDash()
    {
        StartCoroutine(DelayDashCoroutine());
    }

    private IEnumerator DelayDashCoroutine()
    {
        Debug.Log("Entrando en corutina");
        yield return new WaitForSeconds(1.5f);

        Debug.Log("Se quita habilidad");
        speed -= 3.0f;

        yield return new WaitForSeconds(2f);

        Debug.Log("Se reactiva habilidad");
        canDash = true;
    }

}
