using UnityEngine;

public class ClearScrollView : MonoBehaviour
{
    [SerializeField] private Transform scrollViewContent; // O container do conteúdo do ScrollView

    // Método para limpar o conteúdo do ScrollView
    public void ClearContent()
    {
        // Itera sobre todos os filhos do container e destrói-os
        foreach (Transform child in scrollViewContent)
        {
            Destroy(child.gameObject);
        }
    }
}
