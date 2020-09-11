using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeresManager : MonoBehaviour
{
    int seresNumber;
    public int SeresNumber { get => seresNumber; set => seresNumber = value; }
    int spawnPivot;

    [SerializeField] List<Ability> abilitiesList;
    [SerializeField] List<Transform> spawnPoints;
    [SerializeField] Transform firstSpawnPoint;
    [SerializeField] GameObject ser;

    float timer = 0f;
    float delaySpawn;
    [SerializeField] float initialDelaySpawn;

    private void Start()
    {
        ResetSpawnList();
        delaySpawn = initialDelaySpawn;
        spawnPivot = 0;
        SeresNumber = 0;
        CreateFirstSer();
    }

    // Update is called once per frame
    void Update()
    {
        if (seresNumber < 6)
        {
            if (timer >= delaySpawn)
            {
                CreateSer();
                timer = 0f;
                delaySpawn = (initialDelaySpawn * SeresNumber) / 2.0f;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
    }

    private void CreateSer()
    {
        SeresNumber++;

        Vector3 spawnPoint = spawnPoints[spawnPivot++].position;
        if (spawnPivot >= spawnPoints.Count)
        {
            ResetSpawnList();
            spawnPivot = 0;
        }

        SerController serController = Instantiate(ser, spawnPoint, Quaternion.identity).GetComponent<SerController>();

        SetSerAbilities(serController);
    }

    private void CreateFirstSer()
    {
        SeresNumber++;

        Vector3 spawnPoint = firstSpawnPoint.position;

        SerController serController = Instantiate(ser, spawnPoint, Quaternion.identity).GetComponent<SerController>();

        SetSerAbilities(serController);
    }

    private void SetSerAbilities(SerController serController)
    {
        int indexAbility1 = Random.Range(0, abilitiesList.Count);
        int indexAbility2 = Random.Range(0, abilitiesList.Count);

        if (indexAbility1 == indexAbility2)
        {
            if ((indexAbility2 + 1) == abilitiesList.Count)
            {
                indexAbility2 = Random.Range(1, indexAbility1);
            } else
            {
                indexAbility2 += Random.Range(1, abilitiesList.Count - indexAbility2);
            }
        }

        serController.SetAbilities(abilitiesList[indexAbility1], abilitiesList[indexAbility2]);
    }

    private void ResetSpawnList()
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            Transform temp = spawnPoints[i];
            int randomIndex = Random.Range(i, spawnPoints.Count);
            spawnPoints[i] = spawnPoints[randomIndex];
            spawnPoints[randomIndex] = temp;
        }
    }

    
}
