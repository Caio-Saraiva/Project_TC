using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CartTotalCalculator : MonoBehaviour
{
    [SerializeField] private Transform cartContent; // Content do ScrollView onde os itens são instanciados
    [SerializeField] private List<TextMeshProUGUI> totalTexts = new List<TextMeshProUGUI>(); // Lista de TextMeshProUGUI para mostrar o total

    private JsonLoader jsonLoader; // Referência ao JsonLoader para obter informações de preço

    private void Start()
    {
        jsonLoader = FindObjectOfType<JsonLoader>(); // Obtém referência ao JsonLoader na cena
        UpdateTotal(); // Calcula o total inicial
    }

    // Método para atualizar o valor total do carrinho
    public void UpdateTotal()
    {
        double total = 0.0;

        // Percorre cada item instanciado no content do ScrollView
        foreach (Transform item in cartContent)
        {
            PrefabItemData prefabItemId = item.GetComponent<PrefabItemData>();
            if (prefabItemId != null)
            {
                // Obtém o ID do item e busca as informações no JsonLoader
                int itemId = prefabItemId.itemId;
                ItemsShop itemData = jsonLoader.GetElementById(itemId);

                if (itemData != null)
                {
                    total += itemData.price; // Soma o preço do item ao total
                }
                else
                {
                    Debug.LogError("Item com ID " + itemId + " não encontrado.");
                }
            }
        }

        // Atualiza todos os campos TextMeshProUGUI com o valor total
        foreach (var totalText in totalTexts)
        {
            totalText.text = "Total: R$ " + total.ToString("F2");
        }

        Debug.Log("Valor total do carrinho: R$ " + total.ToString("F2"));
    }

    // Método para adicionar um novo campo TextMeshProUGUI à lista dinâmica
    public void AddTotalTextField(TextMeshProUGUI newTextField)
    {
        if (!totalTexts.Contains(newTextField))
        {
            totalTexts.Add(newTextField);
            UpdateTotal(); // Atualiza o total para o novo campo
        }
    }
}
