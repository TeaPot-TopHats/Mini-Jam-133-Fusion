using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChangeDimension : MonoBehaviour
{

    public void DimensionShift(ChargeType type)
    {
        

            switch (type)
        {
            case ChargeType.FIRE:
                
                this.gameObject.GetComponent<Tilemap>().color = new Color(255, 0, 0, 255);
                break;
            case ChargeType.ICE:
                
                this.gameObject.GetComponent<Tilemap>().color = new Color(0, 163, 255, 255);
                break;
            case ChargeType.ELECTRIC:
                
                this.gameObject.GetComponent<Tilemap>().color = new Color(255, 211, 0, 255);
                break;
            default:
                break;
        }

    }

}

