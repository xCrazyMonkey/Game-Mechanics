using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject target;

    void Start()
    {
        Destroy(this.gameObject, Player.Instance.aliveTimer);
        if(target.tag == "Enemy" && target != null)
            transform.LookAt(target.transform);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Player.Instance.bulletVelocity * Time.deltaTime;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Zombie")
        {
            print("hit");
            collision.gameObject.GetComponent<EnemyBehaviour>().TakeDamage(Player.Instance.bulletDamage);
            //Destroy(this.gameObject);
        }
        Destroy(this.gameObject);
    }
    
}
