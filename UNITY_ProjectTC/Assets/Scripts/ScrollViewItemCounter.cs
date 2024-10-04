using TMPro;
using UnityEngine;
using UnityEngine.UI; // Para trabalhar com bot�es

public class ScrollViewItemCounter : MonoBehaviour
{
    // Refer�ncias ao conte�do do ScrollView e TextMeshPro para exibir a contagem de itens
    public Transform scrollViewContent;
    public TextMeshProUGUI itemCountText;

    // GameObject que ser� ativado/desativado
    public GameObject toggleObject;

    // Bot�o que ser� desativado quando o contador estiver em 0 ou menos
    public Button buttonToDisable; // Adicionando o bot�o a ser desativado

    // Vari�vel para armazenar a �ltima contagem de itens
    private int previousItemCount = -1;

    void Start()
    {
        // Atualiza o estado inicial assim que o jogo come�a
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

    // M�todo para atualizar a contagem de itens e o estado do GameObject e bot�o
    void UpdateItemCount()
    {
        // Conta o n�mero de filhos diretos no conte�do do ScrollView
        int itemCount = scrollViewContent.childCount;

        // Atualiza o texto do TextMeshPro com a contagem
        itemCountText.text = itemCount.ToString();

        // Ativa/desativa o GameObject com base na contagem
        toggleObject.SetActive(itemCount == 0);

        // Desativa o bot�o quando o contador estiver em 0 ou menos
        if (buttonToDisable != null)
        {
            buttonToDisable.interactable = itemCount > 0; // O bot�o � desativado quando a contagem � 0 ou menos
        }

        // Armazena a nova contagem para futuras compara��es
        previousItemCount = itemCount;
    }
}
