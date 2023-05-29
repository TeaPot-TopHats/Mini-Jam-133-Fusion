using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AbilityBar : MonoBehaviour
{
    public Button button;
    public Button button2;
    public Button button3;
    public PlayerData playerData;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerData.SelectedCharge = ChargeType.FIRE;
            button.Select();
            Debug.Log("The selected charge is: " + playerData.SelectedCharge);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerData.SelectedCharge = ChargeType.ICE;
            button2.Select();
            Debug.Log("The selected charge is: " + playerData.SelectedCharge);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerData.SelectedCharge = ChargeType.ELECTRIC;
            button3.Select();
            Debug.Log("The selected charge is: " + playerData.SelectedCharge);
        }

    }
}
