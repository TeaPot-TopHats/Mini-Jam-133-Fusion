using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayCharges : MonoBehaviour
{
    public PlayerData playerData;
    public TextMeshProUGUI fireChargeText;
    public TextMeshProUGUI IceChargeText;
    public TextMeshProUGUI ThunderChargeText;
    public TextMeshProUGUI chargeSelectedText;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        fireChargeText.text = playerData.FireCharge.ToString();
        IceChargeText.text = playerData.IceCharge.ToString();
        ThunderChargeText.text = playerData.ElectricCharge.ToString();
        chargeSelectedText.text = playerData.SelectedCharge.ToString(); 
    }
}
