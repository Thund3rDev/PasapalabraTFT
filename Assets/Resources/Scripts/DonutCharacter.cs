using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DonutCharacter : MonoBehaviour
{
    [Header("Objects")]
    [SerializeField]
    private Image image_color;
    [SerializeField]
    private TextMeshProUGUI text_character;
    [SerializeField]
    private Animator characterAnimator;

    [Header("Colors")]
    [SerializeField]
    private Color waitingColor = new Color(0f, 0f, 0f);
    [SerializeField]
    private Color successColor = new Color(0f, 0f, 0f);
    [SerializeField]
    private Color errorColor = new Color(0f, 0f, 0f);

    private void Start()
    {
        image_color.color = waitingColor;
    }

    public void SetCharacter(string character)
    {
        text_character.text = character;
    }

    public void StartThisCharacter()
    {
        characterAnimator.SetBool("idle",true);
    }

    public void StopThisCharacter()
    {
        characterAnimator.SetBool("idle", false);
    }

    public void HitCharacter()
    {
        image_color.color = successColor;
        StopThisCharacter();
    }

    public void FailCharacter()
    {
        image_color.color = errorColor;
        StopThisCharacter();
    }
}
