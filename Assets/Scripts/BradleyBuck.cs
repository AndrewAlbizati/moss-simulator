using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BradleyBuck : MonoBehaviour
{
    public GameObject player;
    public GameObject gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            float playerX = player.transform.position.x;
            float playerZ = player.transform.position.z;
            float bbuckX = transform.position.x;
            float bbuckZ = transform.position.z;

            float distance = Mathf.Sqrt(Mathf.Pow(bbuckX - playerX, 2) + Mathf.Pow(bbuckZ - playerZ, 2));

            if (distance < 2 && Mathf.Abs(player.transform.position.y - transform.position.y) < 4)
            {
                gameObject.SetActive(false);
                gameController.GetComponent<GameController>().AddMoney(1);
                gameController.GetComponent<GameController>().PlayChaChing();
            }
        }
    }
}
