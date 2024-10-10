using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ItemSpawner : MonoBehaviour
{
    [Header("API Loader")]
    public JsonLoader jsonLoader;  // Refer�ncia ao JsonLoader para carregar os dados da API

    [Header("UI References")]
    public GameObject itemPrefab;  // Prefab que ser� instanciado
    public Transform scrollViewContent;  // Conte�do do ScrollView onde os prefabs ser�o instanciados
    public TextMeshProUGUI filterText;  // Campo de texto para exibir os filtros aplicados
    public DynamicPanel dynamicPanel;   // Refer�ncia ao painel din�mico

    [Header("Filter Settings")]
    public string selectedCategory = "Calca"; // Categoria, ex: Calca, Camiseta
    public string selectedGender = "F";      // G�nero, ex: M (masculino) ou F (feminino)

    [Header("Textos para Filtro")]
    public string categoryText; // Texto configur�vel no Inspector para a categoria
    public string genderText;   // Texto configur�vel no Inspector para o g�nero

    [Header("Scroll Settings")]
    public ScrollRect targetScrollRect; // Refer�ncia ao ScrollRect que ser� controlado

    [Header("Eventos Din�micos")]
    public UnityEvent itemButtonEvents;  // Lista de eventos configurados no Inspector

    [SerializeField] private Color hoverColorName; // Cor para "txt-name" ao passar o mouse
    [SerializeField] private Color hoverColorPrice; // Cor para "txt-price" ao passar o mouse
    [SerializeField] private Color defaultColorName; // Cor padr�o para "txt-name"
    [SerializeField] private Color defaultColorPrice; // Cor padr�o para "txt-price"

    void Start()
    {
        StartCoroutine(WaitForJsonToLoad());
    }

    IEnumerator WaitForJsonToLoad()
    {
        while (!jsonLoader.IsJsonLoaded())
        {
            yield return null; // Espera at� a pr�xima frame
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

        // Localiza o objeto "mask" e depois encontra o "img-Item" dentro dele
        Transform maskTransform = newItem.transform.Find("mask");
        if (maskTransform != null)
        {
            Image itemImage = maskTransform.Find("img-Item")?.GetComponent<Image>();
            if (itemImage != null)
            {
                // Usa o caminho com g�nero, categoria e nome
                Sprite itemSprite = Resources.Load<Sprite>("Images/" + groupedItems[0].genero + "/" + groupedItems[0].categoria + "/" + nome);
                if (itemSprite != null)
                {
                    itemImage.sprite = itemSprite;
                }
                else
                {
                    Debug.LogError("Imagem n�o encontrada em Resources/Images/" + groupedItems[0].genero + "/" + groupedItems[0].categoria + "/" + nome);
                }
            }
            else
            {
                Debug.LogError("Objeto 'img-Item' n�o encontrado dentro de 'mask'.");
            }
        }
        else
        {
            Debug.LogError("Objeto 'mask' n�o encontrado no prefab.");
        }

        TextMeshProUGUI itemTitle = newItem.transform.Find("txt-name").GetComponent<TextMeshProUGUI>();
        TextMeshProUGUI itemPrice = newItem.transform.Find("txt-price").GetComponent<TextMeshProUGUI>();

        if (itemTitle != null)
        {
            itemTitle.text = nome;
        }

        if (itemPrice != null)
        {
            itemPrice.text = $"R$ {groupedItems[0].valor_unidade:F2}";
        }

        // Adiciona os eventos de hover (mouse enter e mouse exit)
        AddHoverEvents(newItem, itemTitle, itemPrice);

        // Garante que o scroll funcione corretamente, mesmo ao passar o mouse sobre o prefab
        AddScrollHandler(newItem);

        Button itemButton = newItem.GetComponent<Button>();
        if (itemButton != null)
        {
            itemButton.onClick.AddListener(() =>
            {
                PassCodProdutoListToPanel(prefabItemData.GetCodProdutoList());
                SetupDynamicEvents();
            });
        }
    }

    // M�todo que adiciona os eventos de hover
    private void AddHoverEvents(GameObject prefab, TextMeshProUGUI itemTitle, TextMeshProUGUI itemPrice)
    {
        EventTrigger trigger = prefab.AddComponent<EventTrigger>();

        // Evento ao passar o mouse sobre o prefab
        EventTrigger.Entry entryEnter = new EventTrigger.Entry();
        entryEnter.eventID = EventTriggerType.PointerEnter;
        entryEnter.callback.AddListener((eventData) => { OnHoverEnter(itemTitle, itemPrice); });
        trigger.triggers.Add(entryEnter);

        // Evento ao sair com o mouse do prefab
        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((eventData) => { OnHoverExit(itemTitle, itemPrice); });
        trigger.triggers.Add(entryExit);
    }

    // M�todo chamado quando o mouse entra no prefab (hover)
    private void OnHoverEnter(TextMeshProUGUI itemTitle, TextMeshProUGUI itemPrice)
    {
        if (itemTitle != null)
        {
            itemTitle.color = hoverColorName; // Muda para a cor selecionada no Inspector
        }
        if (itemPrice != null)
        {
            itemPrice.color = hoverColorPrice; // Muda para a cor selecionada no Inspector
        }
    }

    // M�todo chamado quando o mouse sai do prefab
    private void OnHoverExit(TextMeshProUGUI itemTitle, TextMeshProUGUI itemPrice)
    {
        if (itemTitle != null)
        {
            itemTitle.color = defaultColorName; // Retorna � cor padr�o
        }
        if (itemPrice != null)
        {
            itemPrice.color = defaultColorPrice; // Retorna � cor padr�o
        }
    }

    // Adiciona o evento de scroll para garantir que o ScrollRect funcione corretamente
    private void AddScrollHandler(GameObject prefab)
    {
        EventTrigger trigger = prefab.GetComponent<EventTrigger>();

        if (trigger == null)
        {
            trigger = prefab.AddComponent<EventTrigger>();
        }

        // Adiciona o evento de scroll
        EventTrigger.Entry scrollEntry = new EventTrigger.Entry();
        scrollEntry.eventID = EventTriggerType.Scroll;
        scrollEntry.callback.AddListener((eventData) =>
        {
            // Verifica se o ScrollRect foi configurado no Inspector
            if (targetScrollRect != null)
            {
                // Passa o evento de scroll para o ScrollRect configurado no Inspector
                targetScrollRect.OnScroll((PointerEventData)eventData);
            }
        });

        trigger.triggers.Add(scrollEntry);
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
