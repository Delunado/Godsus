using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 direction;
    [SerializeField] float speed = 2f;
    [SerializeField] private float lifeTime = 0.5f;
    [SerializeField] private float damage = 10f;
    private SerController serController;
    private bool reflected = false;
    private bool canRebotar = false;

    public Vector2 Direction { get => direction; set => direction = value; }
    public float Damage { get => damage; set => damage = value; }
    public SerController SerController { get => serController; set => serController = value; }
    public bool Reflected { get => reflected; set => reflected = value; }
    public bool CanRebotar { get => canRebotar; set => canRebotar = value; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Shield"))
        {
            direction *= -1;
            Reflected = true;
        }

        if (collision.CompareTag("Wall") && CanRebotar)
        {
            direction *= -1;
        }
    }
}
