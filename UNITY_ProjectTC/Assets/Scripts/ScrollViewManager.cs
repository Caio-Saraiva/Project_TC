using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScrollViewManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject prefab; // O prefab que será instanciado
    public Transform content; // O Content do ScrollView

    // Supondo que você já tenha a lista de itens carregada de outro script
    public List<ItemsShop> itemsFromJson; // Lista dos itens que veio de outro script

    private void Start()
    {
        // Chama a função para instanciar os itens já carregados
        LoadItems();
    }

    void LoadItems()
    {
        foreach (ItemsShop item in itemsFromJson)
        {
            // Verifica se o item obedece às condições
            if (item.season.Equals("summer", System.StringComparison.OrdinalIgnoreCase) &&
                item.type.Equals("female", System.StringComparison.OrdinalIgnoreCase))
            {
                // Instancia o prefab e o inicializa com os dados do item
                InstantiateItem(item);
            }
        }
    }

    void InstantiateItem(ItemsShop item)
    {
        // Instancia o prefab no ScrollView
        GameObject newItem = Instantiate(prefab, content);

        // Atribui o ID ao script PrefabItemData
        PrefabItemData prefabData = newItem.GetComponent<PrefabItemData>();
        if (prefabData != null)
        {
            prefabData.itemId = item.id;
        }

        // Associa o evento do botão ao OnClick
        Button button = newItem.GetComponentInChildren<Button>(); // Supondo que o botão esteja no prefab
        if (button != null)
        {
            button.onClick.AddListener(() => OnItemClick(prefabData.itemId));
        }

        // Preenche os dados no prefab (como título, preço e imagem)
        Text titleText = newItem.transform.Find("Title").GetComponent<Text>();
        Text priceText = newItem.transform.Find("Price").GetComponent<Text>();
        Image itemImage = newItem.transform.Find("ItemImage").GetComponent<Image>();

        titleText.text = item.title;
        priceText.text = $"${item.price:F2}";

        // Carrega a imagem (certifique-se de que as imagens estão na pasta Resources)
        Sprite sprite = Resources.Load<Sprite>(item.image);
        if (sprite != null)
        {
            itemImage.sprite = sprite;
        }
        else
        {
            Debug.LogWarning("Image not found for item: " + item.title);
        }
    }

    // Função callback a ser executada quando o botão for clicado
    void OnItemClick(int itemId)
    {
        Debug.Log("Item clicado com ID: " + itemId);
        // Aqui você pode adicionar a lógica que precisa ser executada ao clicar no botão
    }
}
