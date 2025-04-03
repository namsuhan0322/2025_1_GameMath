using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DiceRoll : MonoBehaviour
{
    public InputField sideInput;
    public Text diceNumText;
    public int side = 6;

    public void RollingDice()
    {
        if (int.TryParse(sideInput.text, out side) && side > 0)
        {
            int rollResult = Random.Range(1, side + 1);
            diceNumText.text = $"¡÷ªÁ¿ß : {rollResult.ToString()}";
        }
    } 
}
