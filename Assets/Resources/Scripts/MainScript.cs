using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class MainScript : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private TextMeshProUGUI text_error;
    [SerializeField]
    private TMP_InputField charactersInputField;
    [SerializeField]
    private TMP_InputField timerInputField;

    private void Start()
    {
        text_error.gameObject.SetActive(false);
    }

    public void StartGameBlueTeam()
    {
        GlobalData.instance.teamColor = new Color(0f, 0.4f, 1f);
        StartGame();
    }

    public void StartGameOrangeTeam()
    {
        GlobalData.instance.teamColor = new Color(1f, 0.5f, 0f);
        StartGame();
    }

    private void StartGame()
    {
        bool correctFormat = IsCorrectFormat();

        if (correctFormat)
            SceneManager.LoadScene("GameScene");
    }

    private bool IsCorrectFormat()
    {
        if (timerInputField.text == "")
        {
            IncorrectTimerInputFormat();
            return false;
        }

        try
        {
            GlobalData.instance.timerTime = float.Parse(timerInputField.text);
        }
        catch (Exception)
        {
            IncorrectTimerInputFormat();
            return false;
        }

        if (charactersInputField.text == "")
        {
            IncorrectCharactersInputFormat();
            return false;
        }

        if (charactersInputField.text.EndsWith(","))
        {
            IncorrectCharactersInputFormat();
            return false;
        }

        try
        {
            GlobalData.instance.charactersArray = charactersInputField.text.Split(',');
        }
        catch (Exception)
        {
            IncorrectCharactersInputFormat();
            return false;
        }
        return true;
    }

    private void IncorrectCharactersInputFormat()
    {
        text_error.text = "ERROR: Incorrect characters input format.";
        text_error.gameObject.SetActive(true);
    }

    private void IncorrectTimerInputFormat()
    {
        text_error.text = "ERROR: Incorrect timer input format.";
        text_error.gameObject.SetActive(true);
    }
}
