using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-99)] //Esto sirve para que este script se ejecute antes que los demás.
public class MenuInput : MonoBehaviour
{
    [HideInInspector] public float horizontal;
    [HideInInspector] public float vertical;
    [HideInInspector] public bool submit;
    [HideInInspector] public bool pause;

    bool readyToClear; //Esta variable indicará si el input debe limpiarse o no en el siguiente Update().

    [Header("Offset mínimo del stick")]
    [Tooltip("Sirve para que el stick solo funcione cuando llegue a una posicion mínima.")]
    public float minStickOffset = 0.2f;

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
        submit = false;
        pause = false;

        readyToClear = false;
    }

    void ProcessInput()
    {
        //Acumulamos horizontal y vertical
        if (Input.GetAxis("Horizontal") > minStickOffset || Input.GetAxis("Horizontal") < -minStickOffset)
        {
            horizontal += Input.GetAxis("Horizontal");
        }

        if (Input.GetAxis("Vertical") > minStickOffset || Input.GetAxis("Vertical") < -minStickOffset)
        {
            vertical += Input.GetAxis("Vertical");
        }

        //Acumulamos inputs de los botones
        submit = submit || Input.GetButtonDown("Ability2(A)P1");
        pause = pause || Input.GetButtonDown("Ability3(B)P1");
    }
}
