using UnityEngine;

public class GlobalData : MonoBehaviour
{
    public static GlobalData instance;
    public string[] charactersArray;

    public float timerTime;
    public Color teamColor;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        DontDestroyOnLoad(instance);
    }
}
