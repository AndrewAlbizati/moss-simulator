using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CaliforniaPortal : MonoBehaviour
{
    public GameObject player;
    public string newGameScene;

    private bool isActive;
    // Start is called before the first frame update
    void Start()
    {
        isActive = true;
    }

    // Update is called once per frame
    void Update()
    {
        float playerX = player.transform.position.x;
        float playerZ = player.transform.position.z;
        float portalX = transform.position.x;
        float portalZ = transform.position.z;

        float distance = Mathf.Sqrt(Mathf.Pow(portalX - playerX, 2) + Mathf.Pow(portalZ - playerZ, 2));

        if (isActive && distance < 1)
        {
            SceneManager.LoadScene(newGameScene);
        }
    }
}
