using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DynamicPanel : MonoBehaviour
{
    public Image panelImage;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI priceText;

    public void UpdatePanel(ItemsShop itemsShop)
    {
        titleText.text = itemsShop.title;
        priceText.text = "R$ " + itemsShop.price.ToString("F2");

        // Carregar a imagem a partir do caminho sem a extensão
        Sprite sprite = Resources.Load<Sprite>(itemsShop.image);
        if (sprite != null)
        {
            panelImage.sprite = sprite;
        }
        else
        {
            Debug.LogError("Image not found at Resources/" + itemsShop.image);
        }
    }
}
