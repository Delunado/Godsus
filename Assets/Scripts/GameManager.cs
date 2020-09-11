using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Player[] players;
    private AudioSource audioSource;
    public AudioSource audioSourceStart;
    public AudioClip audioClip;
    private bool startMusic = true;

    // Start is called before the first frame update
    void Start()
    {
        players = FindObjectsOfType<Player>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!audioSourceStart.isPlaying && startMusic)
        {
            startMusic = false;
            audioSource.Play();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
    }

    public void FinishGame()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].SetState(new FinishState(players[i]));
        }

        //Efecto camara

        StartCoroutine(RestarScreen());
    }

    IEnumerator RestarScreen()
    {
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene("Restart");
    }
}
