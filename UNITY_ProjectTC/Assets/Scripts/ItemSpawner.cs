using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events; // Para usar UnityEvent
using System.Collections;

[System.Serializable]
public class ItemEvent : UnityEvent<int> { }

public class ItemSpawner : MonoBehaviour
{
    [Header("Configuration")]
    public JsonLoader jsonLoader; // Referência ao script JsonLoader
    public GameObject itemPrefab; // Prefab que será instanciado
    public Transform scrollViewContent; // Ponto de referência do ScrollView onde os prefabs serão instanciados
    public DynamicPanel dynamicPanel; // Referência ao painel dinâmico que será atualizado

    [Header("Event")]
    public ItemEvent onItemButtonClicked; // Evento para chamar métodos de outros GameObjects

    [Header("Filter Settings")]
    public string selectedType = "female"; // Opções: "female", "male"
    public string selectedSeason = "summer"; // Opções: "summer", "winter"

    [Header("UI Element")]
    public TextMeshProUGUI filterText; // O GameObject TextMeshProUGUI que será atualizado

    void Start()
    {
        StartCoroutine(WaitForJsonToLoad());
    }

    IEnumerator WaitForJsonToLoad()
    {
        while (!jsonLoader.IsJsonLoaded())
        {
            yield return null;
        }

        // Define o texto com base no filtro selecionado
        UpdateFilterText();

        Debug.Log("JSON carregado com sucesso. Agora os itens podem ser instanciados.");
        SpawnItems();
    }

    void UpdateFilterText()
    {
        // Verifica a combinação de filtro (type e season) e atribui o valor correspondente ao texto
        if (selectedType == "male" && selectedSeason == "winter")
        {
            filterText.text = "Inverno Masculino";
        }
        else if (selectedType == "male" && selectedSeason == "summer")
        {
            filterText.text = "Verão Masculino";
        }
        else if (selectedType == "female" && selectedSeason == "winter")
        {
            filterText.text = "Inverno Feminino";
        }
        else if (selectedType == "female" && selectedSeason == "summer")
        {
            filterText.text = "Verão Feminino";
        }
        else
        {
            filterText.text = "Filtro Desconhecido";
        }
    }

    void SpawnItems()
    {
        // Limpa os itens anteriores no ScrollView, se houver
        foreach (Transform child in scrollViewContent)
        {
            Destroy(child.gameObject);
        }

        // Carrega os itens filtrados e instancia os prefabs
        foreach (var item in jsonLoader.itemsShopList.itemsShop)
        {
            if (IsItemValid(item))
            {
                InstantiateItem(item);
            }
        }
    }

    bool IsItemValid(ItemsShop item)
    {
        return item.type == selectedType && item.season == selectedSeason;
    }

    void InstantiateItem(ItemsShop item)
    {
        // Instancia o prefab no ScrollView
        GameObject newItem = Instantiate(itemPrefab, scrollViewContent);

        PrefabItemData prefabItemData = newItem.GetComponent<PrefabItemData>();
        if (prefabItemData != null)
        {
            prefabItemData.itemId = item.id;
        }

        // Configura a imagem do prefab
        Image itemImage = newItem.transform.Find("img-Item").GetComponent<Image>();
        if (itemImage != null)
        {
            Sprite itemSprite = Resources.Load<Sprite>(item.image);
            if (itemSprite != null)
            {
                itemImage.sprite = itemSprite;
            }
        }

        // Configura o título (TextMeshProUGUI)
        TextMeshProUGUI itemTitle = newItem.transform.Find("txt-name").GetComponent<TextMeshProUGUI>();
        if (itemTitle != null)
        {
            itemTitle.text = item.title;
        }

        // Configura o preço (TextMeshProUGUI)
        TextMeshProUGUI itemPrice = newItem.transform.Find("txt-price").GetComponent<TextMeshProUGUI>();
        if (itemPrice != null)
        {
            itemPrice.text = $"R$ {item.price:F2}";
        }

        // Configura o botão do prefab para atualizar o painel dinâmico e acionar eventos
        Button itemButton = newItem.GetComponent<Button>();
        if (itemButton != null)
        {
            // Listener para atualizar o painel dinâmico
            itemButton.onClick.AddListener(() => UpdateDynamicPanel(item.id));

            // Listener para chamar o evento UnityEvent
            itemButton.onClick.AddListener(() => onItemButtonClicked.Invoke(item.id));
        }
    }

    void UpdateDynamicPanel(int itemId)
    {
        // Busca o item pelo ID no JsonLoader
        ItemsShop item = jsonLoader.GetElementById(itemId);
        if (item != null)
        {
            dynamicPanel.UpdatePanel(item); // Atualiza o painel com os dados do item
        }
        else
        {
            Debug.LogError("Item com ID " + itemId + " não encontrado.");
        }
    }
}
