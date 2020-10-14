using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    public ZombieScriptableObject ZombieValues;

    private GameObject player;

    [Header("Zombie Values")]
    [SerializeField] private float health;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float damage;
    [SerializeField] private string zombieType;
    [SerializeField] private float spawnDelay = 2f;
    [SerializeField] private bool trackPlayer;

    private bool c_AttackCoolDown;
    private bool lookAtPlayer;
    //Regular ability variables
    [Header("REGULAR ZOMBIE VARIABLES")]
    [SerializeField] private int zombiesInRange = 0;
    [SerializeField] private int MaxZombiesInRange = 3;
    [SerializeField] float maxDistance = 5;
    [SerializeField] List<GameObject> zombiesInRangeForBuff = new List<GameObject>();
    [SerializeField] float DamageBonus = 1.5f;
    //Fast ability variables

    //Fat ability variables

    //Multiple ability variables

    private NavMeshAgent agent;
    public bool DEBUG_FREEZE;
    private void Awake()
    {
        GetZombievalues();
    }
    void Start()
    {
        player = Player.Instance.gameObject;
        agent = GetComponent<NavMeshAgent>();
        StartCoroutine(SpawnDelay());
    }

    // Update is called once per frame
    void Update()
    {
        if (!DEBUG_FREEZE)
        {
            if (lookAtPlayer) transform.LookAt(player.transform);
            if (trackPlayer) agent.SetDestination(player.transform.position);
        }
        
    }

    private IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(spawnDelay);
        trackPlayer = true;
    }

    private void GetZombievalues()
    {
        this.gameObject.name = ZombieValues.zombieName;
        health = ZombieValues.health;
        moveSpeed = ZombieValues.moveSpeed;
        damage = ZombieValues.damage;
    }

    private void Attack()
    {
        float distance = Vector2.Distance(this.transform.position, player.transform.position);
        if (distance <= ZombieValues.attackRange)
        {
            if(!c_AttackCoolDown)StartCoroutine(InflictDamage());
            //perform attack animation
        }
    }

    IEnumerator InflictDamage()
    {
        c_AttackCoolDown = true;
        yield return new WaitForSeconds(ZombieValues.attackCoolDown);
        Player.Instance.TakeDamage(damage);
        print("zombie attacks player");
        c_AttackCoolDown = false;
    }


    public void TakeDamage(int dmg)
    {
        health -= dmg;
        if(health <= 0)
        {
            //zombie dead
            Destroy(this.gameObject);
            GameMaster.Instance.currentAmountOfZombies--;
        }
    }
    
//================================== Zombie abilities ========================================================

    private void RegularZombieAbility()
    {
        foreach(GameObject agent in GameMaster.Instance.ZombiesInScene)
        {
            if (agent != this.gameObject)
            {
                float distance = Vector3.Distance(this.transform.position, agent.transform.position);
                if (distance <= maxDistance)
                {
                    zombiesInRange++;
                    zombiesInRangeForBuff.Add(agent);
                }
            }
        }

        if(zombiesInRange >= MaxZombiesInRange)
        {
            //buff
            foreach(GameObject agent in zombiesInRangeForBuff)
            {
                agent.GetComponent<EnemyBehaviour>().ZombieValues.DamageBuff(DamageBonus);
            }
        }
    }
}
