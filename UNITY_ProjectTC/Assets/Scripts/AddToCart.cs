using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class AddToCart : MonoBehaviour
{
    [SerializeField] private GameObject cartItemPrefab; // Prefab que será instanciado no carrinho
    [SerializeField] private Transform cartContent; // Content do ScrollView do carrinho
    [SerializeField] private DynamicPanel dynamicPanel; // Referência ao painel dinâmico
    [SerializeField] private UnityEvent onDestroyButtonClick; // Lista de eventos configuráveis para o botão "btn-destroy"

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
            // Obtém a cor e o tamanho selecionados no painel dinâmico
            string selectedColor = dynamicPanel.GetSelectedColor();
            string selectedSize = dynamicPanel.GetSelectedSize();

            // Busca o ID correto no painel dinâmico com base na cor e tamanho selecionados
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

            // Acessa o botão "btn-destroy" que está dentro do "BG"
            Transform bgTransform = cartItem.transform.Find("BG");
            if (bgTransform != null)
            {
                Button destroyButton = bgTransform.Find("btn-destroy")?.GetComponent<Button>();
                if (destroyButton != null)
                {
                    // Aqui mantemos os eventos que já existem e adicionamos o evento do AddToCart no final
                    destroyButton.onClick.AddListener(() => {
                        // Outros eventos configurados pelo prefab serão executados antes
                        if (onDestroyButtonClick != null)
                        {
                            onDestroyButtonClick.Invoke();
                        }
                        // Este evento será o último a ser executado
                        Destroy(cartItem); // Exclui o item do carrinho
                    });
                }
                else
                {
                    Debug.LogError("Botão 'btn-destroy' não encontrado dentro do 'BG'.");
                }
            }
            else
            {
                Debug.LogError("'BG' não encontrado no prefab instanciado.");
            }
        }
        else
        {
            Debug.LogError("Prefab, Content do carrinho, Painel Dinâmico não atribuídos ou JSON não carregado.");
        }
    }
}
