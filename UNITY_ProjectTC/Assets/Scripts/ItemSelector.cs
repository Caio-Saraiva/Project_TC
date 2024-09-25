using UnityEngine;
using UnityEngine.UI;

public class ItemSelector : MonoBehaviour
{
    public Toggle toggle; // Refer�ncia ao Toggle
    public Button actionButton; // Bot�o a ser liberado no Inspector

    private void Start()
    {
        // Desabilita o bot�o inicialmente
        actionButton.interactable = false;

        // Adiciona listener ao Toggle
        toggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    private void OnToggleValueChanged(bool isOn)
    {
        // Ativa o bot�o apenas se o Toggle estiver ligado
        actionButton.interactable = isOn;
    }
}
