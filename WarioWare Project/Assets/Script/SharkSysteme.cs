using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkSysteme : MonoBehaviour
{
    public static SharkSysteme Instance;


    public bool lockCanSpawn;
    public bool sharkIsHere;

    public float effectiveTime;
    public float timeBtwSpawn;
    public float minStartSpawn;

    public GameObject shark;
    public Transform sharkPoint;
    private GameObject actualShark;


    void Start()
    {
        ManagerInit();

        lockCanSpawn = false;
        sharkIsHere = false;

        
    }

    void Update()
    {
        if (sharkIsHere == false)
        {
            if (lockCanSpawn == false)
            {
                lockCanSpawn = true;
                StartCoroutine(SharkSpawnRandom());
            }
        }
    }


    // Permet d'avoir un obj de ce type
    void ManagerInit()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator SharkSpawnRandom()
    {
        float cooldown = Random.Range(1, 4);
        yield return new WaitForSeconds(cooldown);
        actualShark = Instantiate(shark, sharkPoint.transform.position, sharkPoint.transform.rotation);
        sharkIsHere = true;

        StartCoroutine(LifeTime());
    }

    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(effectiveTime);
        Destroy(actualShark);
        sharkIsHere = false;
        lockCanSpawn = false;
    }
}
