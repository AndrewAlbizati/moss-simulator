using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CreditsController : MonoBehaviour
{
    public GameObject canvas;
    public float speed = -10f;
    private GameObject textBox;


    private string[] productionRoles =
    {
        "Producer",
        "Senior Producer",
        "Junior Producer",
        "Supervisor",
        "Associate Producer",
        "Production Coordinator",
        "Project Management",
        "Game Director",
        "Development Director",
        "Project Leader",
        "Production Manager"
    };

    private string[] designRoles =
    {
        "Original Concept",
        "Created By",
        "Creative Director",
        "Design Director",
        "Research"
    };

    private string[] programmingRoles =
     {
        "Programmer",
        "Engineer",
        "Development",
        "Technical Director",
        "Scripter",
        "Software Architect",
        "Rendering",
        "Physics",
        "QA Programmer"
    };

    private string text;

    // Start is called before the first frame update
    void Start()
    {
        textBox = canvas.transform.GetChild(0).gameObject;

        text += "PRODUCTION\n";
        foreach (string s in productionRoles)
        {
            text += s + " - Andrew Albizati";
            text += "\n";
        }

        text += "\n\nDESIGN\n";
        foreach (string s in designRoles)
        {
            text += s + " - Andrew Albizati";
            text += "\n";
        }

        text += "\n\nPROGRAMMING\n";
        foreach (string s in programmingRoles)
        {
            text += s + " - Andrew Albizati";
            text += "\n";
        }

        textBox.GetComponent<TMP_Text>().text = text;
    }

    // Update is called once per frame
    void Update()
    {
        textBox.transform.Translate(new Vector3(0, speed * Time.deltaTime, 0));
        //textBox.transform.Translate(new Vector3(0, textBox.transform.position.y + speed, 0) * Time.deltaTime);
        //textBox.transform.position = new Vector3(textBox.transform.position.x, textBox.transform.position.y + speed * Time.deltaTime, textBox.transform.position.z);
    }
}
