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
        if (Input.GetMouseButtonDown(0) && !c_IsRunning) Instantiate(bullet, bulletSpawn.transform.position, this.transform.rotation);//StartCoroutine(ShootTimer());
    }
    
    IEnumerator ShootTimer()
    {
        c_IsRunning = true;
        Instantiate(bullet, bulletSpawn.transform.position, this.transform.rotation);
        yield return new WaitForSeconds(shootCoolDown);
        c_IsRunning = false;
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
