using UnityEngine;
using UnityEngine.UI;

public class ItemSelector : MonoBehaviour
{
    public Toggle toggle; // Referência ao Toggle
    public Button actionButton; // Botão a ser liberado no Inspector

    private void Start()
    {
        // Desabilita o botão inicialmente
        actionButton.interactable = false;

        // Adiciona listener ao Toggle
        toggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    private void OnToggleValueChanged(bool isOn)
    {
        // Ativa o botão apenas se o Toggle estiver ligado
        actionButton.interactable = isOn;
    }
}
