using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AddToCart : MonoBehaviour
{
    [SerializeField] private GameObject cartItemPrefab; // Prefab que ser� instanciado no carrinho
    [SerializeField] private Transform cartContent; // Content do ScrollView do carrinho
    [SerializeField] private DynamicPanel dynamicPanel; // Refer�ncia ao painel din�mico
    [SerializeField] private UnityEvent onDestroyButtonClick; // Lista de eventos configur�veis para o bot�o "btn-destroy"

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
            // Obt�m a cor e o tamanho selecionados no painel din�mico
            string selectedColor = dynamicPanel.GetSelectedColor();
            string selectedSize = dynamicPanel.GetSelectedSize();

            // Busca o ID correto no painel din�mico com base na cor e tamanho selecionados
            int selectedId = dynamicPanel.GetIdByColorAndSize(selectedColor, selectedSize);

            // Instancia o prefab no ScrollView do carrinho
            GameObject cartItem = Instantiate(cartItemPrefab, cartContent);

            // Configura o ID no script PrefabItemId do prefab instanciado
            PrefabItemData prefabItemIdScript = cartItem.GetComponent<PrefabItemData>();
            if (prefabItemIdScript != null)
            {
                prefabItemIdScript.SetCodProduto(selectedId); // Passa o ID correto para o item instanciado
            }

            // Passa o ID e o tamanho selecionado para o script CartItem do prefab instanciado
            CartItem cartItemScript = cartItem.GetComponent<CartItem>();
            cartItemScript?.Initialize(selectedId, selectedSize, jsonLoader);

            // Acessa o bot�o "btn-destroy" que est� dentro do "BG"
            Transform bgTransform = cartItem.transform.Find("BG");
            if (bgTransform != null)
            {
                Button destroyButton = bgTransform.Find("btn-destroy")?.GetComponent<Button>();
                if (destroyButton != null)
                {
                    // Aqui mantemos os eventos que j� existem e adicionamos o evento do AddToCart no final
                    destroyButton.onClick.AddListener(() => {
                        // Outros eventos configurados pelo prefab ser�o executados antes
                        if (onDestroyButtonClick != null)
                        {
                            onDestroyButtonClick.Invoke();
                        }
                        // Este evento ser� o �ltimo a ser executado
                        Destroy(cartItem); // Exclui o item do carrinho
                    });
                }
                else
                {
                    Debug.LogError("Bot�o 'btn-destroy' n�o encontrado dentro do 'BG'.");
                }
            }
            else
            {
                Debug.LogError("'BG' n�o encontrado no prefab instanciado.");
            }
        }
        else
        {
            Debug.LogError("Prefab, Content do carrinho, Painel Din�mico n�o atribu�dos ou JSON n�o carregado.");
        }
    }
}
