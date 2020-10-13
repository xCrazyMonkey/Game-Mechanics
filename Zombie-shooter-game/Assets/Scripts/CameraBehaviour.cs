using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public GameObject target;
    public float xOffset;
    public float yOffset;
    public float zOffset = -10;
    public float speed = 10;
    [SerializeField] private bool LookAtPlayer;
    [SerializeField] private bool freezeRotation;
    [SerializeField] private Vector3 camPos;
    
    void Update()
    {
        camPos = target.transform.position;
        camPos.x += xOffset;
        camPos.y += yOffset;
        camPos.z += zOffset;
        if(transform.position != camPos)
            transform.position = Vector3.Lerp(transform.position, camPos, speed * Time.deltaTime);
        
        if(LookAtPlayer)transform.LookAt(target.transform);

        if (freezeRotation)
        {
            if(transform.rotation.y != 0)
            {
                Quaternion rot = transform.rotation;
                rot.y = 0;
                rot.z = 0;
                transform.rotation = rot;
            }
        }

    }
}
