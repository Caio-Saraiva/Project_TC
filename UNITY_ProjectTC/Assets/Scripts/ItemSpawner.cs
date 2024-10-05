using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;

public class ItemSpawner : MonoBehaviour
{
    [Header("API Loader")]
    public JsonLoader jsonLoader;  // Referência ao JsonLoader para carregar os dados da API

    [Header("UI References")]
    public GameObject itemPrefab;  // Prefab que será instanciado
    public Transform scrollViewContent;  // Conteúdo do ScrollView onde os prefabs serão instanciados
    public TextMeshProUGUI filterText;  // Campo de texto para exibir os filtros aplicados
    public DynamicPanel dynamicPanel;   // Referência ao painel dinâmico

    [Header("Filter Settings")]
    public string selectedCategory = "Calca"; // Categoria, ex: Calca, Camiseta
    public string selectedGender = "F";      // Gênero, ex: M (masculino) ou F (feminino)

    [Header("Textos para Filtro")]
    public string categoryText; // Texto configurável no Inspector para a categoria
    public string genderText;   // Texto configurável no Inspector para o gênero

    [Header("Eventos Dinâmicos")]
    public UnityEvent itemButtonEvents;  // Lista de eventos configurados no Inspector

    void Start()
    {
        StartCoroutine(WaitForJsonToLoad());
    }

    IEnumerator WaitForJsonToLoad()
    {
        while (!jsonLoader.IsJsonLoaded())
        {
            yield return null; // Espera até a próxima frame
        }

        UpdateFilterText();
        SpawnItems();
    }

    void UpdateFilterText()
    {
        filterText.text = $"{categoryText} - {genderText}";
    }

    void SpawnItems()
    {
        foreach (Transform child in scrollViewContent)
        {
            Destroy(child.gameObject);
        }

        var filteredItems = jsonLoader.itemsShopList.itemsShop
            .Where(item => IsItemValid(item))
            .GroupBy(item => item.nome)
            .ToList();

        foreach (var group in filteredItems)
        {
            CreateGroupedPrefab(group.Key, group.ToList());
        }
    }

    bool IsItemValid(ItemsShop item)
    {
        return item.categoria == selectedCategory && item.genero == selectedGender;
    }

    void CreateGroupedPrefab(string nome, List<ItemsShop> groupedItems)
    {
        GameObject newItem = Instantiate(itemPrefab, scrollViewContent);

        PrefabItemData prefabItemData = newItem.GetComponent<PrefabItemData>();
        if (prefabItemData != null)
        {
            prefabItemData.SetGroupedItems(groupedItems);
        }

        Image itemImage = newItem.transform.Find("img-Item").GetComponent<Image>();
        if (itemImage != null)
        {
            // Usa o caminho com gênero e nome
            Sprite itemSprite = Resources.Load<Sprite>("Images/" + groupedItems[0].genero + "/" + nome);
            if (itemSprite != null)
            {
                itemImage.sprite = itemSprite;
            }
            else
            {
                Debug.LogError("Imagem não encontrada em Resources/Images/" + groupedItems[0].genero + "/" + nome);
            }
        }

        TextMeshProUGUI itemTitle = newItem.transform.Find("txt-name").GetComponent<TextMeshProUGUI>();
        if (itemTitle != null)
        {
            itemTitle.text = nome;
        }

        TextMeshProUGUI itemPrice = newItem.transform.Find("txt-price").GetComponent<TextMeshProUGUI>();
        if (itemPrice != null)
        {
            itemPrice.text = $"R$ {groupedItems[0].valor_unidade:F2}";
        }

        Button itemButton = newItem.GetComponent<Button>();
        if (itemButton != null)
        {
            itemButton.onClick.AddListener(() => {
                PassCodProdutoListToPanel(prefabItemData.GetCodProdutoList());
                SetupDynamicEvents();
            });
        }
    }


    void PassCodProdutoListToPanel(List<int> codProdutoList)
    {
        dynamicPanel.UpdatePanelByCodProdutoList(codProdutoList);
    }

    void SetupDynamicEvents()
    {
        if (itemButtonEvents != null)
        {
            itemButtonEvents.Invoke();
        }
    }
}
