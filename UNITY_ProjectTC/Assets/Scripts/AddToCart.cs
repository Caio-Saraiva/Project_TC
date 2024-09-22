using UnityEngine;

public class AddToCart : MonoBehaviour
{
    [SerializeField] private GameObject cartItemPrefab; // Prefab que será instanciado no carrinho
    [SerializeField] private Transform cartContent; // Content do ScrollView do carrinho
    [SerializeField] private DynamicPanel dynamicPanel; // Referência ao painel dinâmico

    private JsonLoader jsonLoader; // Referência ao JsonLoader

    private void Start()
    {
        jsonLoader = FindObjectOfType<JsonLoader>(); // Obtém referência ao JsonLoader na cena
    }

    // Método chamado pelo botão para adicionar ao carrinho
    public void AddItemToCart()
    {
        if (cartItemPrefab != null && cartContent != null && jsonLoader.IsJsonLoaded() && dynamicPanel != null)
        {
            int currentItemID = dynamicPanel.GetCurrentItemId(); // Obtém o ID do item atual do painel dinâmico
            string selectedSize = dynamicPanel.GetSelectedSize(); // Obtém o tamanho selecionado do painel dinâmico

            // Instancia o prefab no ScrollView do carrinho
            GameObject cartItem = Instantiate(cartItemPrefab, cartContent);

            // Configura o ID no script PrefabItemId do prefab instanciado
            PrefabItemData prefabItemIdScript = cartItem.GetComponent<PrefabItemData>();
            if (prefabItemIdScript != null)
            {
                prefabItemIdScript.SetItemId(currentItemID);
                Debug.Log("ID Setado no PrefabItemId: " + prefabItemIdScript.itemId);
            }

            // Passa o ID e o tamanho selecionado para o script CartItem do prefab instanciado
            CartItem cartItemScript = cartItem.GetComponent<CartItem>();
            cartItemScript?.Initialize(currentItemID, selectedSize, jsonLoader);
        }
        else
        {
            Debug.LogError("Prefab, Content do carrinho, Painel Dinâmico não atribuídos ou JSON não carregado.");
        }
    }
}
