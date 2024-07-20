using TMPro;
using UnityEngine.UI;

public class NumberCard : Cards
{
    public override void Interract()
    {
        // Implement interaction logic here
    }

    public void SetMySkin(int i)
    {
        this.GetComponent<Image>().sprite = Skin;
        setMyNumbers(i);
    }
    void setMyNumbers(int myNumber)
    {
        this.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = myNumber.ToString();
        this.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = myNumber.ToString();
        this.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = myNumber.ToString();
    }
}
