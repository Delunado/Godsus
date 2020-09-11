using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthControllerPlayer : MonoBehaviour
{
    [SerializeField] Slider slider;
    private Player player;

    GameManager gameManager;

    public float initialHealth;
    private float actualHealth;

    private bool isDead = false;
    public bool IsDead { get => isDead; set => isDead = value; }

    private float timer = 0f;
    private float delayToDamage = 1.0f;


    private void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        player = GetComponentInParent<Player>();
    }

    private void Start()
    {
        actualHealth = initialHealth;
        slider.value = (actualHealth / initialHealth) * 100f;
    }

    private void Update()
    {
        if (actualHealth <= 0f && !IsDead)
        {
            Death();
        }

    }

    public void AddHealth(float value)
    {
        actualHealth += value;
        slider.value = (actualHealth / initialHealth) * 100f;
        player.audioSource.PlayOneShot(player.audioClip);
        player.spriteRenderer.color = Color.red;
        StartCoroutine("Damage");
    }

    private void Death()
    {
        IsDead = true;
        Destroy(transform.parent.GetComponent<SpriteRenderer>());
        gameManager.FinishGame();
    }

    private IEnumerator Damage()
    {
        yield return new WaitForSeconds(0.2f);
        player.spriteRenderer.color = Color.white;
    }
}
