using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityType
{
    AIMING,
    NO_AIMING
}

public abstract class Ability : ScriptableObject
{
    [SerializeField] Sprite sprite;
    [SerializeField] protected Vector2 direction;
    [SerializeField] protected Vector3 position;
    protected AbilityType abilityType;
    protected Player player;
    protected SerController serController;
    [SerializeField] protected AudioClip audioClip;

    public Sprite Sprite { get => sprite; set => sprite = value; }
    public Vector2 Direction { get => direction; set => direction = value; }
    public AbilityType AbilityType { get => abilityType; set => abilityType = value; }
    public Player Player { get => player; set => player = value; }
    public SerController SerController { get => serController; set => serController = value; }
    public AudioClip AudioClip { get => audioClip; set => audioClip = value; }

    public abstract void UseAbility();

    public abstract void SetDirection(Vector2 _direction);
    public abstract void SetPosition(Vector3 _position);

    public abstract void SetType();
}
