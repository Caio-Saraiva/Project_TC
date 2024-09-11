using TMPro;
using UnityEngine;

public class ScrollViewItemCounter : MonoBehaviour
{
    // Referências ao conteúdo do ScrollView e TextMeshPro para exibir a contagem de itens
    public Transform scrollViewContent;
    public TextMeshProUGUI itemCountText;

    // GameObject que será ativado/desativado
    public GameObject toggleObject;

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

    // Método para atualizar a contagem de itens e o estado do GameObject
    void UpdateItemCount()
    {
        // Conta o número de filhos diretos no conteúdo do ScrollView
        int itemCount = scrollViewContent.childCount;

        // Atualiza o texto do TextMeshPro com a contagem
        itemCountText.text = itemCount.ToString();

        // Ativa/desativa o GameObject com base na contagem
        toggleObject.SetActive(itemCount == 0);

        // Armazena a nova contagem para futuras comparações
        previousItemCount = itemCount;
    }
}
