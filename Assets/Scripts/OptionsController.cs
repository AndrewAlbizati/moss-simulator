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
    public GameObject volumeObject;
    public GameObject keybindObject;

    private Slider sensitivitySlider;
    private TMP_Text sensitivityText;

    private Slider volumeSlider;
    private TMP_Text volumeText;

    private GameObject keybindButton;
    private TMP_Text keybindLabel;

    private KeyCode actionKeybind;
    private bool awaitingKeybind = false;

    private void OnEnable()
    {
        sensitivitySlider = sensitivityObject.transform.GetChild(1).GetComponent<Slider>();
        sensitivityText = sensitivityObject.transform.GetChild(2).GetComponent<TMP_Text>();

        volumeSlider = volumeObject.transform.GetChild(1).GetComponent<Slider>();
        volumeText = volumeObject.transform.GetChild(2).GetComponent<TMP_Text>();

        keybindButton = keybindObject.transform.GetChild(0).gameObject;
        keybindLabel = keybindObject.transform.GetChild(1).GetComponent<TMP_Text>();

        float sensitivity;
        float volume;
        if (PlayerPrefs.HasKey("sensitivity") && PlayerPrefs.HasKey("volume"))
        {
            sensitivity = PlayerPrefs.GetFloat("sensitivity");
            volume = PlayerPrefs.GetFloat("volume");
        }
        else
        {
            sensitivity = 200f;
            volume = 75;
        }

        sensitivitySlider.value = sensitivity;
        volumeSlider.value = volume;

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
        volumeText.SetText(Mathf.FloorToInt(volumeSlider.value).ToString());
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
            keybindButton.transform.GetChild(0).GetComponent<TMP_Text>().SetText("Press any button");

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
        PlayerPrefs.SetFloat("volume", volumeSlider.value);
        PlayerPrefs.SetString("actionKeybind", actionKeybind.ToString());
        SceneManager.LoadScene(PlayerPrefs.GetString("previousScene"));
    }

    public void OnKeybindClick()
    {
        awaitingKeybind = true;
    }
}
