using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkSysteme : MonoBehaviour
{
    #region Variable
    public static SharkSysteme Instance;

    [Header ("Temps que le shark reste")]
    public float effectiveTime;

    [Header ("Temps que le requin spawn après l'alerte")]
    public float timeForSpawn;

    [Header ("Attente min entre chaque alerte")]
    public float timeBtwSpawn;

    [Header ("Temps avant lancement premier spawn")]
    public float minStartSpawn;

    [Header ("Bool qui permet ou non l'apparition du shark")]
    public bool lockCanSpawn;

    [Header ("Bool qui detecte la presence du shark")]
    public bool sharkIsHere;

    [Space]
    public GameObject shark;
    public GameObject stopSign;
    public Transform sharkPoint;

    private GameObject actualShark;
    private Animator signAnim;
    #endregion

    void Start()
    {
        ManagerInit();
        signAnim = stopSign.GetComponent<Animator>();

        lockCanSpawn = true;
        sharkIsHere = false;

        stopSign.SetActive(false);

        StartCoroutine(StartTime());
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


    // Coroutine qui fait spawn le shark de manière aléatoire
    IEnumerator SharkSpawnRandom()
    {
        float cooldown = Random.Range(1, 3);
        yield return new WaitForSeconds(cooldown);

        stopSign.SetActive(true);
        yield return new WaitForSeconds(timeForSpawn);
        signAnim.SetBool("switch", true);
        actualShark = Instantiate(shark, sharkPoint.transform.position, sharkPoint.transform.rotation);

        sharkIsHere = true;

        StartCoroutine(LifeTime());
    }

    // Coroutine qui detruit le shark après un certain temps
    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(effectiveTime);
        Destroy(actualShark);

        stopSign.SetActive(false);

        sharkIsHere = false;

        StartCoroutine(Cooldown());
    }

    // Cooldown entre chaque spawn
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(timeBtwSpawn);
        lockCanSpawn = false;
    }

    // Cooldown du start
    IEnumerator StartTime()
    {
        yield return new WaitForSeconds(minStartSpawn);
        lockCanSpawn = false;
    }
}
