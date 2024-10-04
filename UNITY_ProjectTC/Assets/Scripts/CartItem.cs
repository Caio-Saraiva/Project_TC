using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CartItem : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemPrice;
    [SerializeField] private TextMeshProUGUI itemSize;

    // M�todo chamado para inicializar o item do carrinho com base no ID e tamanho
    public void Initialize(int id, string size, JsonLoader jsonLoader)
    {
        LoadItemData(id, jsonLoader);
        SetItemSize(size); // Define o tamanho selecionado
    }

    // M�todo que carrega os dados do item com base no ID
    private void LoadItemData(int id, JsonLoader jsonLoader)
    {
        ItemsShop itemData = jsonLoader.GetElementByCodProduto(id);

        if (itemData != null)
        {
            itemImage.sprite = Resources.Load<Sprite>("Images/" + itemData.nome);
            itemName.text = itemData.nome;
            itemPrice.text = "R$ " + itemData.valor_unidade.ToString("F2"); // Formata��o de pre�o
        }
        else
        {
            Debug.LogError("Item com ID " + id + " n�o encontrado!");
        }
    }

    // M�todo para definir o tamanho do item no carrinho
    private void SetItemSize(string size)
    {
        itemSize.text = "Tamanho " + size; // Define o texto com o tamanho selecionado
    }
}
