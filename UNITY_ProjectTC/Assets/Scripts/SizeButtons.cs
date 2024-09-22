using UnityEngine;
using UnityEngine.UI;

public class SizeButtons : MonoBehaviour
{
    [System.Serializable]
    public struct ButtonConfig
    {
        public Button button;
        public string label;
    }

    public ButtonConfig[] buttons;
    public Color defaultColor = Color.white; // Cor padrão configurável no Inspector
    public Color selectedColor = Color.green; // Cor de seleção configurável no Inspector
    private Button selectedButton;
    public SizeSelectionManager sizeSelectionManager; // Referência ao SizeSelectionManager

    private void Start()
    {
        if (buttons.Length > 0)
        {
            foreach (var config in buttons)
            {
                Button button = config.button;
                string label = config.label;

                // Define a cor padrão inicial dos botões
                button.GetComponent<Image>().color = defaultColor;
                button.onClick.AddListener(() => SelectButton(button, label));
            }
        }
    }

    private void SelectButton(Button button, string label)
    {
        // Reseta a cor do botão anteriormente selecionado
        if (selectedButton != null)
        {
            selectedButton.GetComponent<Image>().color = defaultColor;
        }

        // Atualiza a cor do botão selecionado
        button.GetComponent<Image>().color = selectedColor;
        selectedButton = button;

        // Atualiza o tamanho selecionado no SizeSelectionManager
        sizeSelectionManager.UpdateSelectedSize(label);
    }

    // Método para resetar as cores de todos os botões
    public void ResetButtonColors()
    {
        foreach (var config in buttons)
        {
            config.button.GetComponent<Image>().color = defaultColor;
        }
        selectedButton = null; // Nenhum botão selecionado
    }
}
