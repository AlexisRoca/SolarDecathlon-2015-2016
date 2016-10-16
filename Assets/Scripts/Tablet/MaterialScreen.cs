using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MaterialScreen : TabletScreen
{
    public Text NameText;
    public Text DescriptionText;
    public Text AdvantagesText;
    public Image MaterialImage;
    public Material Material { get; set; }
    public MaterialInfo MaterialInfo { get; set; }

    public override void Show()
    {
        base.Show();

        if (Material != null && MaterialInfo != null)
        {
            MaterialImage.material = Material;

            NameText.text = MaterialInfo.Name;
            DescriptionText.text = MaterialInfo.Description;

            AdvantagesText.text = null;
            foreach (string advantage in MaterialInfo.Advantages)
                AdvantagesText.text += "+ " + advantage + "\n";
        }
        else
        {
            NameText.text = "";
            DescriptionText.text = "";
            AdvantagesText.text = "";
            MaterialImage.material = null;
        }
    }
}
