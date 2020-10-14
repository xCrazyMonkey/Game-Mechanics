using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public GameObject bullet;
    public GameObject bulletSpawn;

    [Header("bullet variables")]
    public float bulletVelocity = 10;
    public float aliveTimer = 5;
    public int bulletDamage = 5;

    [Header("player variables")]
    public float moveSpeed;
    public float sprintValue = 20;
    public float walkValue = 10;
    public float shootCoolDown = 1;
    private float health = 20;
    [SerializeField] private float gravity = 50f;

    private CharacterController charController;

    private GameObject enemyTarget;
    RaycastHit hit;

    public float Health
    {
        get { return health; }
        set
        {
            health = value;
        }
    }

    private bool c_IsRunning;

    private static Player instance = null;
    public static Player Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
        charController = GetComponent<CharacterController>();
    }

    void Start()
    {
        Health = health; 
    }

    // Update is called once per frame
    void Update()
    {
        MouseRotation();
        Controls();
        Movement();
    }

    private void FixedUpdate()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;
        
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit: "+hit.collider.gameObject.name);
        }
        else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            Debug.Log("Did Hit: " + hit.collider.gameObject.name);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }
    void OnDrawGizmosSelected()
    {
        /*if (target != null)
        {
            // Draws a blue line from this transform to the target
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(bulletSpawn.transform.position, target.position);
        }*/
    }

    private void MouseRotation()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            Vector3 target = ray.GetPoint(distance);
            Vector3 direction = target - transform.position;
            float rotation = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, rotation, 0);
        }
    }

    private void Controls()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject newBullet = Instantiate(bullet, bulletSpawn.transform.position, this.transform.rotation);
            newBullet.GetComponent<Bullet>().target = hit.collider.gameObject;
        }
    }

    private void Movement()
    {
        moveSpeed = Input.GetKey(KeyCode.LeftShift) ? sprintValue : walkValue;
        
        var input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 velocity = input.normalized * moveSpeed;
        if(!charController.isGrounded)velocity.y -= gravity * Time.deltaTime;
        
        charController.Move(velocity * Time.deltaTime);
    }
    public void TakeDamage(float dmg)
    {
        Health -= dmg;
        GameMaster.Instance.SetHealthSlider(Health);
        if (Health <= 0)
        {
            //gameOver
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Gate")
        {
        }
    }
}
