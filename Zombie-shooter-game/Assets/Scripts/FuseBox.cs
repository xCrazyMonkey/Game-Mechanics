using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuseBox : MonoBehaviour
{
    [SerializeField] private GameObject interactText;
    // Start is called before the first frame update
    private GameObject player;

    void Start()
    {
        player = Player.Instance.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerNearButton();
    }

    private void PlayerNearButton()
    {
        if (Vector3.Distance(player.transform.position, this.transform.position) < 3)
        {
            Debug.Log("press button");
            interactText.SetActive(true);
        }
        else
        {
            interactText.SetActive(false);
        }
    }
}
