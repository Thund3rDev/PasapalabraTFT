using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    [SerializeField]
    private float DISTANCE_TO_CENTER = 230f;

    [Header("Characters")]
    [SerializeField]
    private GameObject donutCharacter_prefab;
    [SerializeField]
    private GameObject donut;

    public List<DonutCharacter> donutCharacters = new List<DonutCharacter>();
    private DonutCharacter currentCharacter;
    public int characterIndex = 0;
    private int numberOfCharacters;

    [Header("Timer")]
    [SerializeField]
    private TextMeshProUGUI text_timer;
    [SerializeField]
    private Image image_timerColor;

    [Header("Score")]
    [SerializeField]
    private TextMeshProUGUI text_score;

    // Time
    private bool isTimeRunning = false;
    private float timer;

    private int score = 0;

    private void Start()
    {
        CreateDonut();
        currentCharacter = donutCharacters[characterIndex];
        timer = GlobalData.instance.timerTime;
        text_timer.text = "" + Mathf.RoundToInt(timer);
        image_timerColor.color = GlobalData.instance.teamColor;
    }

    private void Update()
    {
        if (isTimeRunning)
        {
            if (timer > 0.0f) {
                timer -= Time.deltaTime;
                text_timer.text = "" + Mathf.RoundToInt(timer);
            }
        }
    }

    private void CreateDonut()
    {
        numberOfCharacters = GlobalData.instance.charactersArray.Length;
        float angleBetweenCharacters = 360 / (float)numberOfCharacters;

        for (int i = 0; i < numberOfCharacters; i++)
        {
            CreateCharacter(i * angleBetweenCharacters, GlobalData.instance.charactersArray[i].ToUpper());
        }
    }

    private void CreateCharacter(float angle, string character)
    {
        Quaternion rotation = Quaternion.AngleAxis(angle, -Vector3.forward);
        Vector3 direction = rotation * Vector3.up;
        Vector3 position = donut.transform.position + (direction * DISTANCE_TO_CENTER);

        GameObject thisDonutCharacterGameObject = Instantiate(donutCharacter_prefab, position, Quaternion.Euler(0,0,0), donut.transform);
        DonutCharacter thisDonutCharacter = thisDonutCharacterGameObject.GetComponent<DonutCharacter>();

        thisDonutCharacter.SetCharacter(character);
        donutCharacters.Add(thisDonutCharacter);
    }

    public void StartButton()
    {
        currentCharacter.StartThisCharacter();
        isTimeRunning = true;
    }

    public void PasapalabraButton()
    {
        if (isTimeRunning)
        {
            currentCharacter.StopThisCharacter();
            characterIndex = (characterIndex + 1) % donutCharacters.Count;
            ChangeToNextCharacter();
            isTimeRunning = false;
        }
    }

    public void HitButton()
    {
        if (isTimeRunning)
        {
            currentCharacter.HitCharacter();
            donutCharacters.Remove(currentCharacter);

            if (donutCharacters.Count == 0)
            {
                isTimeRunning = false;
            }
            else
            {
                characterIndex = (characterIndex) % donutCharacters.Count;
                ChangeToNextCharacter();
                currentCharacter.StartThisCharacter();
            }

            score++;
            text_score.text = "" + score;
            AudioManager.instance.PlaySound("Hit");
        }
    }

    public void FailButton()
    {
        if (isTimeRunning)
        {
            currentCharacter.FailCharacter();
            donutCharacters.Remove(currentCharacter);

            if (donutCharacters.Count != 0)
            {
                characterIndex = (characterIndex) % donutCharacters.Count;
                ChangeToNextCharacter();
            }
            isTimeRunning = false;
            AudioManager.instance.PlaySound("Fail");
        }
    }

    public void ChangeToNextCharacter()
    {
        currentCharacter = donutCharacters[characterIndex];
    }
}
