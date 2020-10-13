using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public List<GameObject> zombieSpawns = new List<GameObject>();
    public GameObject zombieObject;
    public int currentAmountOfZombies;
    public int spawnTimer = 3;
    private bool timerIsAlive = false;

    private int zombiesSpawned;

    public List<GameObject> ZombiesInScene = new List<GameObject>();
    public bool SPAWN_ZOMBIES;

    [Header("UI objects")]
    [SerializeField] private Slider healthUI;
    
    private static GameMaster instance = null;
    public static GameMaster Instance
    {
        get { return instance; }
    }

    public bool isPlayerInLevel;

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
        SetHealthMax(Player.Instance.Health);
        SetHealthSlider(Player.Instance.Health);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!timerIsAlive && SPAWN_ZOMBIES) StartCoroutine(SpawnTimer());
        //healthText.text = "Health: " + Player.Instance.Health;
    }
    
    IEnumerator SpawnTimer()
    {
        timerIsAlive = true;
        yield return new WaitForSeconds(spawnTimer);
        SpawnZombies();
        timerIsAlive = false;
    }

    void SpawnZombies()
    {
        int rnd = Random.Range(0, zombieSpawns.Count);
        GameObject randomSpawn = zombieSpawns[rnd];
        Vector3 spawnPos = randomSpawn.transform.position;
        GameObject ZombieEnt = Instantiate(zombieObject, spawnPos, randomSpawn.transform.rotation);
        //ZombieEnt.transform.position = randomSpawn.transform.position;
        //print("dit is de spawn: "+randomSpawn.gameObject.name + "coords: "+randomSpawn.transform.position);
        ZombiesInScene.Add(ZombieEnt);
        currentAmountOfZombies++;
        zombiesSpawned++;
    }


    public void SetHealthSlider(float value)
    {
        healthUI.value = value;
    }
    private void SetHealthMax(float value)
    {
        healthUI.maxValue = value;
    }
}
