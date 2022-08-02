using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BradleyBuck : MonoBehaviour
{
    public GameObject player;
    public GameObject gameController;

    private bool collected = false;

    private void OnEnable()
    {
        if (PlayerPrefs.HasKey("collected" + transform.position.x))
        {
            collected = PlayerPrefs.GetInt("collected" + transform.position.x) == 1;
        }
        else
        {
            collected = false;
        }
    }

    private void OnDisable()
    {
        PlayerPrefs.SetInt("collected" + transform.position.x, collected ? 1 : 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (collected)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {

            if (gameController.GetComponent<GameController>().GetTaskIndex() >= 1)
            {
                gameObject.GetComponent<MeshRenderer>().enabled = true;
            } else if (gameObject.GetComponent<MeshRenderer>().enabled)
            {
                gameObject.GetComponent<MeshRenderer>().enabled = false;
            }

            if (gameObject.GetComponent<MeshRenderer>().enabled)
            {
                float playerX = player.transform.position.x;
                float playerZ = player.transform.position.z;
                float bbuckX = transform.position.x;
                float bbuckZ = transform.position.z;

                float distance = Mathf.Sqrt(Mathf.Pow(bbuckX - playerX, 2) + Mathf.Pow(bbuckZ - playerZ, 2));

                if (distance < 2 && Mathf.Abs(player.transform.position.y - transform.position.y) < 4)
                {
                    collected = true;
                    gameObject.SetActive(false);
                    gameController.GetComponent<GameController>().AddMoney(1);
                    gameController.GetComponent<GameController>().PlayChaChing();
                }
            }
        }
    }
}
