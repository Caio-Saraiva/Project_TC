using UnityEngine;
using UnityEngine.UI;

public class SizeSelectionManager : MonoBehaviour
{
    public SizeButtons sizeButtons; // Refer�ncia ao script SizeButtons
    public Button addToCartButton; // Bot�o "Adicionar ao Carrinho"
    private string selectedSize = ""; // Tamanho selecionado ("P", "M" ou "G")

    private void Start()
    {
        ResetSelection(); // Reseta a sele��o ao iniciar
        addToCartButton.onClick.AddListener(OnAddToCartButtonClick); // Adiciona listener ao bot�o
    }

    // M�todo para atualizar o tamanho selecionado
    public void UpdateSelectedSize(string size)
    {
        selectedSize = size;
        Debug.Log("Tamanho selecionado: " + selectedSize);

        // Habilita o bot�o "Adicionar ao Carrinho" apenas se um tamanho for selecionado
        addToCartButton.interactable = !string.IsNullOrEmpty(selectedSize);
    }

    // M�todo para resetar a sele��o de tamanho
    public void ResetSelection()
    {
        selectedSize = ""; // Limpa o tamanho selecionado

        // Reseta a cor de todos os bot�es no SizeButtons
        sizeButtons.ResetButtonColors();

        // Desativa o bot�o "Adicionar ao Carrinho"
        addToCartButton.interactable = false;
    }

    // Listener do bot�o "Adicionar ao Carrinho"
    private void OnAddToCartButtonClick()
    {
        if (!string.IsNullOrEmpty(selectedSize))
        {
            // Executa a fun��o de adicionar ao carrinho
            Debug.Log("Item adicionado ao carrinho com o tamanho: " + selectedSize);
            // Chame aqui a fun��o de adicionar ao carrinho
        }
        else
        {
            Debug.LogWarning("Nenhum tamanho selecionado!");
        }
    }
}
