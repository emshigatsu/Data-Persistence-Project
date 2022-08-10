using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SceneData : MonoBehaviour
{
    public static SceneData Instance;
    public string playerName;

    public TMP_InputField inputField;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    public void UpdateName()
    {
        playerName = inputField.GetComponent<TMP_InputField>().text;
        MainManager.Instance.playerName2 = playerName;
    }
}
