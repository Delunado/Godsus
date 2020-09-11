using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthControllerSer : MonoBehaviour
{
    [SerializeField] Slider slider;
    private SerController serController;
    private SeresManager seresManager;

    public float initialHealth;
    private float actualHealth;

    private bool isDead = false;
    public bool IsDead { get => isDead; set => isDead = value; }

    private float timer = 0f;
    private float delayToDamage = 2.0f;


    private void Awake()
    {
        seresManager = FindObjectOfType<SeresManager>();
        serController = GetComponentInParent<SerController>();
    }

    private void Start()
    {
        actualHealth = initialHealth;
        slider.value = (actualHealth / initialHealth) * 100f;
    }

    private void Update()
    {
        //if (serController.Possesed && !IsDead)
        //{
        //    if (timer >= delayToDamage)
        //    {
        //        AddHealth(-5.0f);
        //        timer = 0f;
        //    }
        //    else
        //    {
        //        timer += Time.deltaTime;
        //    }
        //}

        if (actualHealth <= 0f && !IsDead)
        {
            Death();
        }

    }

    public void AddHealth(float value)
    {
        actualHealth += value;
        slider.value = (actualHealth / initialHealth) * 100f;
        serController.audioSource.PlayOneShot(serController.audioClip);
        serController.spriteSer.color = Color.red;
        StartCoroutine("Damage");
    }

    private void Death()
    {
        IsDead = true;
        //transform.parent.transform.position = new Vector3(1000, 1000, 1000);
        Destroy(serController.GetComponent<SpriteRenderer>());
        Destroy(serController.GetComponent<Collider2D>());
        Destroy(serController.GetComponentInParent<Collider2D>());
        Destroy(serController.abilitySprite1);
        Destroy(serController.abilitySprite2);
        slider.transform.position = Vector3.left * 1000f;
        Destroy(serController.aimingArrow);
        Destroy(serController.gameObject, 2f);
        seresManager.SeresNumber--;
    }

    private IEnumerator Damage()
    {
        yield return new WaitForSeconds(0.2f);
        serController.spriteSer.color = Color.white;
    }
}
