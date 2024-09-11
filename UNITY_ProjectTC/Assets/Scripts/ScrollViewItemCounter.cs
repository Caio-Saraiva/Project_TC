using TMPro;
using UnityEngine;

public class ScrollViewItemCounter : MonoBehaviour
{
    // Refer�ncias ao conte�do do ScrollView e TextMeshPro para exibir a contagem de itens
    public Transform scrollViewContent;
    public TextMeshProUGUI itemCountText;

    // GameObject que ser� ativado/desativado
    public GameObject toggleObject;

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

    // M�todo para atualizar a contagem de itens e o estado do GameObject
    void UpdateItemCount()
    {
        // Conta o n�mero de filhos diretos no conte�do do ScrollView
        int itemCount = scrollViewContent.childCount;

        // Atualiza o texto do TextMeshPro com a contagem
        itemCountText.text = itemCount.ToString();

        // Ativa/desativa o GameObject com base na contagem
        toggleObject.SetActive(itemCount == 0);

        // Armazena a nova contagem para futuras compara��es
        previousItemCount = itemCount;
    }
}
