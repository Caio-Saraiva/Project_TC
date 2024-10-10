using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CartTotalCalculator : MonoBehaviour
{
    [SerializeField] private Transform cartContent; // Content do ScrollView onde os itens s�o instanciados
    [SerializeField] private List<TextMeshProUGUI> totalTexts = new List<TextMeshProUGUI>(); // Lista de TextMeshProUGUI para mostrar o total
    [SerializeField] private TextMeshProUGUI itemCountText; // Campo para exibir a quantidade de itens no carrinho

    private JsonLoader jsonLoader; // Refer�ncia ao JsonLoader para obter informa��es de pre�o

    private void Start()
    {
        jsonLoader = FindObjectOfType<JsonLoader>(); // Obt�m refer�ncia ao JsonLoader na cena
        UpdateTotal(); // Calcula o total inicial
    }

    // M�todo para atualizar o valor total do carrinho e a quantidade de itens
    public void UpdateTotal()
    {
        double total = 0.0;
        int itemCount = 0; // Contador de itens

        // Percorre cada item instanciado no content do ScrollView
        foreach (Transform item in cartContent)
        {
            PrefabItemData prefabItemId = item.GetComponent<PrefabItemData>();
            if (prefabItemId != null)
            {
                // Obt�m o ID do item e busca as informa��es no JsonLoader
                int itemId = prefabItemId.codProduto;

                ItemsShop itemData = jsonLoader.GetElementByCodProduto(itemId);

                if (itemData != null)
                {
                    total += itemData.valor_unidade; // Soma o pre�o do item ao total
                    itemCount++; // Incrementa o contador de itens
                }
                else
                {
                    Debug.LogError("Item com ID " + itemId + " n�o encontrado.");
                }
            }
        }

        // Atualiza todos os campos TextMeshProUGUI com o valor total
        foreach (var totalText in totalTexts)
        {
            totalText.text = "R$: " + total.ToString("F2");
        }

        // Atualiza o campo que exibe a quantidade de itens no carrinho
        if (itemCountText != null)
        {
            itemCountText.text = "Itens no carrinho: " + itemCount;
        }

        Debug.Log("Valor total do carrinho: R$ " + total.ToString("F2"));
        Debug.Log("Total de itens no carrinho: " + itemCount);
    }

    // M�todo para adicionar um novo campo TextMeshProUGUI � lista din�mica
    public void AddTotalTextField(TextMeshProUGUI newTextField)
    {
        if (!totalTexts.Contains(newTextField))
        {
            totalTexts.Add(newTextField);
            UpdateTotal(); // Atualiza o total para o novo campo
        }
    }
}
