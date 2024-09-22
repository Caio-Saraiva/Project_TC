using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DynamicPanel : MonoBehaviour
{
    public Image panelImage;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI priceText;
    private int currentItemId; // Campo para armazenar o ID do item atual
    private string selectedSize = "P"; // Tamanho selecionado, padr�o "P"

    public void UpdatePanel(ItemsShop itemsShop)
    {
        currentItemId = itemsShop.id; // Armazena o ID do item atual

        titleText.text = itemsShop.title;
        priceText.text = "R$ " + itemsShop.price.ToString("F2");

        // Carregar a imagem a partir do caminho sem a extens�o
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

    // M�todo para definir o tamanho selecionado (pode ser chamado por bot�es no UI)
    public void SetSelectedSize(string size)
    {
        selectedSize = size;
    }

    // M�todo para retornar o tamanho selecionado
    public string GetSelectedSize()
    {
        return selectedSize;
    }

    // M�todo para retornar o ID do item atual
    public int GetCurrentItemId()
    {
        return currentItemId;
    }
}
