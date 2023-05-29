using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayHealth : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI scoreText;
    public PlayerData playerData;

    // Update is called once per frame
    void Update()
    {
        healthText.text = playerData.Health.ToString();
        scoreText.text = playerData.Score.ToString();
    }
}
