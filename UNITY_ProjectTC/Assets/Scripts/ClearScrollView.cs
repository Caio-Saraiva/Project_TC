using UnityEngine;

public class ClearScrollView : MonoBehaviour
{
    [SerializeField] private Transform scrollViewContent; // O container do conte�do do ScrollView

    // M�todo para limpar o conte�do do ScrollView
    public void ClearContent()
    {
        // Itera sobre todos os filhos do container e destr�i-os
        foreach (Transform child in scrollViewContent)
        {
            Destroy(child.gameObject);
        }
    }
}
