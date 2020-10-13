using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject, Player.Instance.aliveTimer);
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
        if (collision.gameObject.tag == "Environment")
        {

        }
        Destroy(this.gameObject);
    }
}
