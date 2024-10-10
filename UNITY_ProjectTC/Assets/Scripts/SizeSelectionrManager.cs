using UnityEngine;
using UnityEngine.UI;

public class SizeSelectionManager : MonoBehaviour
{
    public SizeButtons sizeButtons; // Referência ao script SizeButtons
    public Button addToCartButton; // Botão "Adicionar ao Carrinho"
    private string selectedSize = ""; // Tamanho selecionado ("P", "M" ou "G")

    private void Start()
    {
        ResetSelection(); // Reseta a seleção ao iniciar
        addToCartButton.onClick.AddListener(OnAddToCartButtonClick); // Adiciona listener ao botão
    }

    // Método para atualizar o tamanho selecionado
    public void UpdateSelectedSize(string size)
    {
        selectedSize = size;
        Debug.Log("Tamanho selecionado: " + selectedSize);

        // Habilita o botão "Adicionar ao Carrinho" apenas se um tamanho for selecionado
        addToCartButton.interactable = !string.IsNullOrEmpty(selectedSize);
    }

    // Método para resetar a seleção de tamanho
    public void ResetSelection()
    {
        selectedSize = ""; // Limpa o tamanho selecionado

        // Reseta a cor de todos os botões no SizeButtons
        sizeButtons.ResetButtonColors();

        // Desativa o botão "Adicionar ao Carrinho"
        addToCartButton.interactable = false;
    }

    // Listener do botão "Adicionar ao Carrinho"
    private void OnAddToCartButtonClick()
    {
        if (!string.IsNullOrEmpty(selectedSize))
        {
            // Executa a função de adicionar ao carrinho
            Debug.Log("Item adicionado ao carrinho com o tamanho: " + selectedSize);
            // Chame aqui a função de adicionar ao carrinho
        }
        else
        {
            Debug.LogWarning("Nenhum tamanho selecionado!");
        }
    }
}
