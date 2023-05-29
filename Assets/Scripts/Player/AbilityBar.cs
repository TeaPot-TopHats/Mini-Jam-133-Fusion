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
        if(playerData.SelectedCharge == ChargeType.FIRE)
        {
            // playerData.SelectedCharge = ChargeType.FIRE;
            button.Select();
            Debug.Log("The selected charge is: " + playerData.SelectedCharge);
        }
        else if (playerData.SelectedCharge == ChargeType.ICE)
        {
            //playerData.SelectedCharge = ChargeType.ICE;
            button2.Select();
            Debug.Log("The selected charge is: " + playerData.SelectedCharge);
        }
        else if (playerData.SelectedCharge == ChargeType.ELECTRIC)
        {
            //playerData.SelectedCharge = ChargeType.ELECTRIC;
            button3.Select();
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
