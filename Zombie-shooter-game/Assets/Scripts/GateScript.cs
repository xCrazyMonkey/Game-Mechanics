using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour
{
    [SerializeField] private GameObject gate;
    [SerializeField] private GameObject button;

    [SerializeField] private GameObject interactText;

    private GameObject player;
    void Start()
    {
        gate.SetActive(false);
        player = Player.Instance.gameObject;
    }
    
    void Update()
    {
        PlayerNearButton();
        if (!gate.activeSelf) PlayerEntersLevel();
    }

    private void PlayerNearButton()
    {
        if (Vector3.Distance(player.transform.position, button.transform.position) < 3)
        {
            Debug.Log("press button");
            interactText.SetActive(true);
        }
        else
        {
            interactText.SetActive(false);
        }
    }

    private void PlayerEntersLevel()
    {
        if (GameMaster.Instance.isPlayerInLevel) gate.SetActive(true);
        //gate slammed dicht sfx
    }
}
