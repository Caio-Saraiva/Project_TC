using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class DynamicPanel : MonoBehaviour
{
    public Image panelImage; // Imagem do produto
    public TextMeshProUGUI titleText; // Texto do nome do produto
    public TextMeshProUGUI priceText; // Texto do valor do produto
    public TMP_Dropdown colorDropdown; // Dropdown para selecionar a cor
    public TMP_Dropdown sizeDropdown; // Dropdown para selecionar o tamanho

    private List<ItemsShop> storedItems = new List<ItemsShop>(); // Armazena todos os itens passados
    private ItemsShop currentItem; // Item atual exibido no painel

    public JsonLoader jsonLoader; // Referência ao JsonLoader para buscar itens pelo ID

    // Atualiza o painel com base em uma lista de IDs de produtos
    public void UpdatePanelByCodProdutoList(List<int> codProdutoList)
    {
        storedItems.Clear(); // Limpa os itens armazenados
        foreach (int codProduto in codProdutoList)
        {
            ItemsShop item = jsonLoader.GetElementByCodProduto(codProduto);
            if (item != null)
            {
                storedItems.Add(item); // Armazena o item encontrado
            }
        }

        if (storedItems.Count > 0)
        {
            UpdatePanel(storedItems[0]); // Exibe o primeiro item por padrão
        }
        else
        {
            Debug.LogError("Nenhum item encontrado com os IDs fornecidos.");
        }

        UpdateDropdownOptions(); // Atualiza os Dropdowns de cor e tamanho
    }

    // Atualiza o painel com base em um único item
    private void UpdatePanel(ItemsShop item)
    {
        currentItem = item; // Armazena o item atual

        titleText.text = item.nome;
        priceText.text = "R$ " + item.valor_unidade.ToString("F2");

        Sprite sprite = Resources.Load<Sprite>("Images/" + item.nome);
        if (sprite != null)
        {
            panelImage.sprite = sprite;
        }
        else
        {
            Debug.LogError("Imagem não encontrada em Resources/Images/" + item.nome);
        }

        SetDropdownSelections(item);
    }

    // Atualiza os Dropdowns com base nos itens armazenados
    private void UpdateDropdownOptions()
    {
        List<string> colorOptions = new List<string>();
        foreach (var item in storedItems)
        {
            if (!colorOptions.Contains(item.cor))
            {
                colorOptions.Add(item.cor);
            }
        }
        colorDropdown.ClearOptions();
        colorDropdown.AddOptions(colorOptions);

        List<string> sizeOptions = new List<string>();
        foreach (var item in storedItems)
        {
            if (!sizeOptions.Contains(item.tamanho))
            {
                sizeOptions.Add(item.tamanho);
            }
        }
        sizeDropdown.ClearOptions();
        sizeDropdown.AddOptions(sizeOptions);
    }

    // Método para definir as seleções iniciais nos dropdowns com base no item atual
    private void SetDropdownSelections(ItemsShop item)
    {
        colorDropdown.value = colorDropdown.options.FindIndex(option => option.text == item.cor);
        sizeDropdown.value = sizeDropdown.options.FindIndex(option => option.text == item.tamanho);
    }

    // Método para retornar a cor selecionada
    public string GetSelectedColor()
    {
        return colorDropdown.options[colorDropdown.value].text;
    }

    // Método para retornar o tamanho selecionado
    public string GetSelectedSize()
    {
        return sizeDropdown.options[sizeDropdown.value].text;
    }

    // Método para buscar o ID com base na cor e tamanho selecionados
    public int GetIdByColorAndSize(string selectedColor, string selectedSize)
    {
        ItemsShop selectedItem = storedItems.Find(item => item.cor == selectedColor && item.tamanho == selectedSize);
        if (selectedItem != null)
        {
            return selectedItem.cod_produto;
        }
        else
        {
            Debug.LogError("Nenhum item encontrado para a cor e o tamanho selecionados.");
            return -1; // Retorna -1 se não encontrar um item correspondente
        }
    }
}
