using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CartItem : MonoBehaviour
{
    public Image itemImage; // A imagem do item
    public TextMeshProUGUI itemName; // O nome do item
    public TextMeshProUGUI itemPrice; // O preço do item
    public TextMeshProUGUI itemSize; // O tamanho do item
    public TextMeshProUGUI itemColor; // A cor do item

    private JsonLoader jsonLoader;

    // Método para inicializar o item no carrinho
    public void Initialize(int id, string selectedSize, JsonLoader jsonLoader)
    {
        ItemsShop itemData = jsonLoader.GetElementByCodProduto(id);

        if (itemData != null)
        {
            // Define a imagem do item
            itemImage.sprite = Resources.Load<Sprite>("Images/" + itemData.genero + "/" + itemData.nome);

            // Define o nome do item
            itemName.text = itemData.nome;

            // Define o preço do item
            itemPrice.text = "R$ " + itemData.valor_unidade.ToString("F2");

            // Define o tamanho selecionado do item
            itemSize.text = "Tamanho: " + selectedSize;

            // Define a cor do item
            itemColor.text = "Cor: " + itemData.cor;
        }
        else
        {
            Debug.LogError("Item com ID " + id + " não encontrado!");
        }
    }
}
