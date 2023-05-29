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

    private Button selectedButton;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerData.SelectedCharge = ChargeType.FIRE;
            SelectButton(button);
            Debug.Log("The selected charge is: " + playerData.SelectedCharge);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerData.SelectedCharge = ChargeType.ICE;
            SelectButton(button2);
            Debug.Log("The selected charge is: " + playerData.SelectedCharge);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerData.SelectedCharge = ChargeType.ELECTRIC;
            SelectButton(button3);
            Debug.Log("The selected charge is: " + playerData.SelectedCharge);
        }

    }

    private void SelectButton(Button buttonToSelect)
    {
        if(selectedButton != null)
        {
            if (selectedButton == buttonToSelect)
            {
                return;
            }
           
        }
        selectedButton = buttonToSelect;
        selectedButton.Select();         
    }
}
