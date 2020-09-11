using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    private MenuInput menuInput;
    
    // Start is called before the first frame update
    void Start()
    {
        menuInput = FindObjectOfType<MenuInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if (menuInput.submit)
        {
            SceneManager.LoadScene("Main");
        }
        else if(menuInput.pause)
        {
            Debug.Log("aa");
            SceneManager.LoadScene("Menu");
        }
    }
}
