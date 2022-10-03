using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class OptionsController : MonoBehaviour
{
    public GameObject sensitivityObject;
    public GameObject keybindObject;

    private Slider sensitivitySlider;
    private TMP_Text sensitivityText;

    private GameObject keybindButton;
    private TMP_Text keybindLabel;

    private KeyCode actionKeybind;
    private bool awaitingKeybind = false;

    private void OnEnable()
    {
        sensitivitySlider = sensitivityObject.transform.GetChild(1).GetComponent<Slider>();
        sensitivityText = sensitivityObject.transform.GetChild(2).GetComponent<TMP_Text>();

        keybindButton = keybindObject.transform.GetChild(0).gameObject;
        keybindLabel = keybindObject.transform.GetChild(1).GetComponent<TMP_Text>();

        float sensitivity;
        if (PlayerPrefs.HasKey("sensitivity"))
        {
            sensitivity = PlayerPrefs.GetFloat("sensitivity");
        }
        else
        {
            sensitivity = 200f;
        }

        sensitivitySlider.value = sensitivity;

        if (PlayerPrefs.HasKey("actionKeybind"))
        {
            actionKeybind = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("actionKeybind"));
        }
        else
        {
            actionKeybind = KeyCode.E;
        }
    }

    // Update is called once per frame
    void Update()
    {
        sensitivityText.SetText(Mathf.FloorToInt(sensitivitySlider.value).ToString());
        keybindLabel.SetText(actionKeybind.ToString());

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (awaitingKeybind)
            {
                awaitingKeybind = false;
            }
            else
            {
                OnBackClick();
            }
        }

        if (awaitingKeybind)
        {
            keybindButton.GetComponent<Button>().interactable = false;
            keybindButton.transform.GetChild(0).GetComponent<TMP_Text>().SetText("Press any key...");

            foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(kcode))
                {
                    if (kcode.ToString().Contains("Mouse"))
                    {
                        continue;
                    }

                    actionKeybind = kcode;
                    awaitingKeybind = false;
                    break;
                }
            }
        }
        else
        {
            keybindButton.GetComponent<Button>().interactable = true;
            keybindButton.transform.GetChild(0).GetComponent<TMP_Text>().SetText("Change Action Keybind");
        }
    }

    public void OnBackClick()
    {
        PlayerPrefs.SetFloat("sensitivity", sensitivitySlider.value);
        PlayerPrefs.SetString("actionKeybind", actionKeybind.ToString());
        SceneManager.LoadScene(PlayerPrefs.GetString("previousScene"));
    }

    public void OnKeybindClick()
    {
        awaitingKeybind = true;
    }
}
