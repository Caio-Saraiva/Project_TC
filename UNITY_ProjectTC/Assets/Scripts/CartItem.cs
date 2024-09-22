using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CartItem : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemPrice;
    [SerializeField] private TextMeshProUGUI itemSize;

    // Método chamado para inicializar o item do carrinho com base no ID e tamanho
    public void Initialize(int id, string size, JsonLoader jsonLoader)
    {
        LoadItemData(id, jsonLoader);
        SetItemSize(size); // Define o tamanho selecionado
    }

    // Método que carrega os dados do item com base no ID
    private void LoadItemData(int id, JsonLoader jsonLoader)
    {
        ItemsShop itemData = jsonLoader.GetElementById(id);

        if (itemData != null)
        {
            // Supondo que você tenha algum método para carregar sprites baseado no nome da imagem
            itemImage.sprite = Resources.Load<Sprite>(itemData.image);
            itemName.text = itemData.title;
            itemPrice.text = "R$ " + itemData.price.ToString("F2"); // Formatação de preço
        }
        else
        {
            Debug.LogError("Item com ID " + id + " não encontrado!");
        }
    }

    // Método para definir o tamanho do item no carrinho
    private void SetItemSize(string size)
    {
        itemSize.text = size; // Define o texto com o tamanho selecionado ("P", "M", ou "G")
    }
}
