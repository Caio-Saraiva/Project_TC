using TMPro;
using UnityEngine;
using UnityEngine.UI; // Para trabalhar com botões

public class ScrollViewItemCounter : MonoBehaviour
{
    // Referências ao conteúdo do ScrollView e TextMeshPro para exibir a contagem de itens
    public Transform scrollViewContent;
    public TextMeshProUGUI itemCountText;

    // GameObject que será ativado/desativado
    public GameObject toggleObject;

    // Botão que será desativado quando o contador estiver em 0 ou menos
    public Button buttonToDisable; // Adicionando o botão a ser desativado

    // Variável para armazenar a última contagem de itens
    private int previousItemCount = -1;

    void Start()
    {
        // Atualiza o estado inicial assim que o jogo começa
        UpdateItemCount();
    }

    void Update()
    {
        // Verifica se a contagem de itens mudou
        if (scrollViewContent.childCount != previousItemCount)
        {
            UpdateItemCount();
        }
    }

    // Método para atualizar a contagem de itens e o estado do GameObject e botão
    void UpdateItemCount()
    {
        // Conta o número de filhos diretos no conteúdo do ScrollView
        int itemCount = scrollViewContent.childCount;

        // Atualiza o texto do TextMeshPro com a contagem
        itemCountText.text = itemCount.ToString();

        // Ativa/desativa o GameObject com base na contagem
        toggleObject.SetActive(itemCount == 0);

        // Desativa o botão quando o contador estiver em 0 ou menos
        if (buttonToDisable != null)
        {
            buttonToDisable.interactable = itemCount > 0; // O botão é desativado quando a contagem é 0 ou menos
        }

        // Armazena a nova contagem para futuras comparações
        previousItemCount = itemCount;
    }
}
