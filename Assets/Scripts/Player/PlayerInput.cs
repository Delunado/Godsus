using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)] //Esto sirve para que este script se ejecute antes que los demás.
public class PlayerInput : MonoBehaviour
{
    private int playerID;

    [HideInInspector] public float horizontal;
    [HideInInspector] public float vertical;
    [HideInInspector] public bool ability1X; //IsPressed
    [HideInInspector] public bool ability1XIsUp;
    [HideInInspector] public bool ability1XIsDown;
    [HideInInspector] public bool ability2A;
    [HideInInspector] public bool ability2AIsDown;
    [HideInInspector] public bool ability2AIsUp;
    [HideInInspector] public bool ability3B;
    [HideInInspector] public bool ability3BIsDown;
    [HideInInspector] public bool ability4Y;
    [HideInInspector] public bool ability4YIsDown;

    bool readyToClear; //Esta variable indicará si el input debe limpiarse o no en el siguiente Update().

    [Header("Offset mínimo del stick")]
    [Tooltip("Sirve para que el stick solo funcione cuando llegue a una posicion mínima.")]
    public float minStickOffset = 0.2f;

    private void Start()
    {
        playerID = GetComponent<Player>().playerID;
    }

    // Update is called once per frame
    void Update()
    {
        ClearInput();
        ProcessInput();

        //Clampeamos los ejes x e y
        horizontal = Mathf.Clamp(horizontal, -1f, 1f);
        vertical = Mathf.Clamp(vertical, -1f, 1f);
    }

    private void FixedUpdate()
    {
        readyToClear = true; //Este flag permite que el input se limpie durante
                             //el Update y asegura que no se va a perder ningún input.
    }

    void ClearInput()
    {
        if (!readyToClear)
        {
            return;
        }

        //Limpiamos las variables
        horizontal = 0f;
        vertical = 0f;
        ability1X = false;
        ability1XIsUp = false;
        ability1XIsDown = false;
        ability2A = false;
        ability2AIsDown = false;
        ability2AIsUp = false;
        ability3B = false;
        ability3BIsDown = false;
        ability4Y = false;
        ability4YIsDown = false;

        readyToClear = false;
    }

    void ProcessInput()
    {
        if (playerID == 1)
        {
            //Acumulamos horizontal y vertical
            if (Input.GetAxis("HorizontalP1") > minStickOffset || Input.GetAxis("HorizontalP1") < -minStickOffset)
            {
                horizontal += Input.GetAxis("HorizontalP1");
            }

            if (Input.GetAxis("VerticalP1") > minStickOffset || Input.GetAxis("VerticalP1") < -minStickOffset)
            {
                vertical += Input.GetAxis("VerticalP1");
            }

            //Acumulamos inputs de los botones
            ability1X = ability1X || Input.GetButton("Ability1(X)P1");
            ability1XIsUp = ability1XIsUp || Input.GetButtonUp("Ability1(X)P1");
            ability1XIsDown = ability1XIsDown || Input.GetButtonDown("Ability1(X)P1");

            ability2A = ability2A || Input.GetButton("Ability2(A)P1");
            ability2AIsDown = ability2AIsDown || Input.GetButtonDown("Ability2(A)P1");
            ability2AIsUp = ability2AIsUp || Input.GetButtonUp("Ability2(A)P1");

            ability3B = ability3B || Input.GetButton("Ability3(B)P1");
            ability3BIsDown = ability3BIsDown || Input.GetButtonDown("Ability3(B)P1");

            ability4Y = ability4Y || Input.GetButton("Ability4(Y)P1");
            ability4YIsDown = ability4YIsDown || Input.GetButtonDown("Ability4(Y)P1");
        } else
        {
            //Acumulamos horizontal y vertical
            if (Input.GetAxis("HorizontalP2") > minStickOffset || Input.GetAxis("HorizontalP2") < -minStickOffset)
            {
                horizontal += Input.GetAxis("HorizontalP2");
            }

            if (Input.GetAxis("VerticalP2") > minStickOffset || Input.GetAxis("VerticalP2") < -minStickOffset)
            {
                vertical += Input.GetAxis("VerticalP2");
            }

            //Acumulamos inputs de los botones
            ability1X = ability1X || Input.GetButton("Ability1(X)P2");
            ability1XIsUp = ability1XIsUp || Input.GetButtonUp("Ability1(X)P2");
            ability1XIsDown = ability1XIsDown || Input.GetButtonDown("Ability1(X)P2");

            ability2A = ability2A || Input.GetButton("Ability2(A)P2");
            ability2AIsDown = ability2AIsDown || Input.GetButtonDown("Ability2(A)P2");
            ability2AIsUp = ability2AIsUp || Input.GetButtonUp("Ability2(A)P2");

            ability3B = ability3B || Input.GetButton("Ability3(B)P2");
            ability3BIsDown = ability3BIsDown || Input.GetButtonDown("Ability3(B)P2");

            ability4Y = ability4Y || Input.GetButton("Ability4(Y)P2");
            ability4YIsDown = ability4YIsDown || Input.GetButtonDown("Ability4(Y)P2");
        }
        
    }
}
