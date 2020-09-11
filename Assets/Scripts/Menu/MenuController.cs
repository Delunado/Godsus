using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    MenuInput input;
    AudioSource audioSource;
    public GameObject instrucctions;

    public List<Button> menuTexts = new List<Button>(); //DEBE COINCIDIR CON EL NUMERO DE OPCIONES REAL. UNO POR CADA IMAGEN.
    public List<GameObject> menuCursor = new List<GameObject>(); //DEBE COINCIDIR CON EL NUMERO DE OPCIONES REAL. UNO POR CADA IMAGEN.
    int actualIndex;
    int maxIndex;

    public float delayCursorMovement = 0.05f;
    float timer = 0f;
    bool canMove;

    public AudioClip clipMover;
    public AudioClip clipAceptar;

    //Textos
    private Button jugar;
    private Button salir;
    private Button continuar;
    private Button opcionActual;

    private GameObject playCursor;
    private GameObject optionCursor;
    private GameObject exitCursor;
    private GameObject cursorActual;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        jugar = GameObject.Find("Play").GetComponent<Button>();
        continuar = GameObject.Find("Options").GetComponent<Button>();
        salir = GameObject.Find("Exit").GetComponent<Button>();

        playCursor = GameObject.Find("SelectorPlay");
        optionCursor = GameObject.Find("SelectorOptions");
        exitCursor = GameObject.Find("SelectorExit");
        optionCursor.SetActive(false);
        exitCursor.SetActive(false);

        opcionActual = jugar.GetComponent<Button>();
        cursorActual = playCursor;

        menuTexts.Add(jugar);
        menuTexts.Add(continuar);
        menuTexts.Add(salir);

        menuCursor.Add(playCursor);
        menuCursor.Add(optionCursor);
        menuCursor.Add(exitCursor);

        input = GetComponent<MenuInput>();
        canMove = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        actualIndex = 0;
        maxIndex = menuTexts.Count - 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove)
        {
            CheckCanMoveTimer();
        }
        else
        {
            MenuMovement();
        }

        if (input.pause)
        {
            instrucctions.SetActive(false);
            audioSource.PlayOneShot(clipAceptar);
        }
    }

    void EfectosTextos()
    {
        //Cambiamos el color anterior
        var colors = opcionActual.colors;
        colors.normalColor = Color.white;
        opcionActual.colors = colors;
        cursorActual.SetActive(false);

        opcionActual = menuTexts[actualIndex];
        cursorActual = menuCursor[actualIndex];

        //Cambiamos el color de la nueva opcion
        var colors2 = opcionActual.colors;
        colors2.normalColor = Color.yellow;
        opcionActual.colors = colors2;
        cursorActual.SetActive(true);
    }


    void CheckCanMoveTimer()
    {
        if (timer < delayCursorMovement)
        {
            timer += Time.deltaTime;
        }
        else
        {
            canMove = true;
            timer = 0f;
        }
    }

    void MenuMovement()
    {
        if (input.vertical < 0) //Si vas hacia arriba en el menu
        {
            audioSource.PlayOneShot(clipMover);
            int index = ++actualIndex;

            if (index > maxIndex)
            {
                actualIndex = 0;
            }
            EfectosTextos();
            canMove = false;

        }
        else if (input.vertical > 0)
        {
            audioSource.PlayOneShot(clipMover);
            int index = --actualIndex;
            if (index < 0)
            {
                actualIndex = maxIndex;
            }
            EfectosTextos();
            canMove = false;

        }
        else if (input.submit)
        {
           SelectOption(actualIndex);
        }
    }

    void SelectOption(int index)
    {
        switch (index)
        {
            case 0:
                Debug.Log("aaa");
                audioSource.PlayOneShot(clipAceptar);
                SceneManager.LoadScene("Main");
                break;

            case 1:
                instrucctions.SetActive(true);
                audioSource.PlayOneShot(clipAceptar);
                break;

            case 2:
                audioSource.PlayOneShot(clipAceptar);
                Application.Quit();
                break;
        }
    }
}