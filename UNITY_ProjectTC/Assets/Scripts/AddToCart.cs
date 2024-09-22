using UnityEngine;

public class AddToCart : MonoBehaviour
{
    [SerializeField] private GameObject cartItemPrefab; // Prefab que ser� instanciado no carrinho
    [SerializeField] private Transform cartContent; // Content do ScrollView do carrinho
    [SerializeField] private DynamicPanel dynamicPanel; // Refer�ncia ao painel din�mico

    private JsonLoader jsonLoader; // Refer�ncia ao JsonLoader

    private void Start()
    {
        jsonLoader = FindObjectOfType<JsonLoader>(); // Obt�m refer�ncia ao JsonLoader na cena
    }

    // M�todo chamado pelo bot�o para adicionar ao carrinho
    public void AddItemToCart()
    {
        if (cartItemPrefab != null && cartContent != null && jsonLoader.IsJsonLoaded() && dynamicPanel != null)
        {
            int currentItemID = dynamicPanel.GetCurrentItemId(); // Obt�m o ID do item atual do painel din�mico
            string selectedSize = dynamicPanel.GetSelectedSize(); // Obt�m o tamanho selecionado do painel din�mico

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
            Debug.LogError("Prefab, Content do carrinho, Painel Din�mico n�o atribu�dos ou JSON n�o carregado.");
        }
    }
}
