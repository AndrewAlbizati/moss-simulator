using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BradleyBuck : MonoBehaviour
{
    public GameObject player;
    public GameObject gameControllerObject;
    public int minimumTaskIndex;

    private bool collected = false;
    private GameController gameController;

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
        gameController = gameControllerObject.GetComponent<GameController>();
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

            if (gameController.GetTaskIndex() >= minimumTaskIndex)
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
                    gameController.AddMoney(1);
                    gameController.PlayChaChing();
                }
            }
        }
    }
}
