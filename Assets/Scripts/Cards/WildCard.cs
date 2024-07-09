using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WildCard : Cards
{
    public override void Interract()
    {
        // Implement interaction logic here
    }

    public void SetMySkin()
    {
        this.GetComponent<Image>().sprite = MySkin;
    }
}
