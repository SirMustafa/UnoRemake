using UnityEngine.UI;

public class NumberActionCard : Cards
{
    public override void Interract()
    {
        // Implement interaction logic here
    }

    public void SetMySkin()
    {
        this.GetComponent<Image>().sprite = Skin;
    }
}