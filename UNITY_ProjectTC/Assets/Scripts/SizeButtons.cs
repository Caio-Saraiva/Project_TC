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
    public Color defaultColor = Color.white; // Cor padr�o configur�vel no Inspector
    public Color selectedColor = Color.green; // Cor de sele��o configur�vel no Inspector
    private Button selectedButton;
    public SizeSelectionManager sizeSelectionManager; // Refer�ncia ao SizeSelectionManager

    private void Start()
    {
        if (buttons.Length > 0)
        {
            foreach (var config in buttons)
            {
                Button button = config.button;
                string label = config.label;

                // Define a cor padr�o inicial dos bot�es
                button.GetComponent<Image>().color = defaultColor;
                button.onClick.AddListener(() => SelectButton(button, label));
            }
        }
    }

    private void SelectButton(Button button, string label)
    {
        // Reseta a cor do bot�o anteriormente selecionado
        if (selectedButton != null)
        {
            selectedButton.GetComponent<Image>().color = defaultColor;
        }

        // Atualiza a cor do bot�o selecionado
        button.GetComponent<Image>().color = selectedColor;
        selectedButton = button;

        // Atualiza o tamanho selecionado no SizeSelectionManager
        sizeSelectionManager.UpdateSelectedSize(label);
    }

    // M�todo para resetar as cores de todos os bot�es
    public void ResetButtonColors()
    {
        foreach (var config in buttons)
        {
            config.button.GetComponent<Image>().color = defaultColor;
        }
        selectedButton = null; // Nenhum bot�o selecionado
    }
}
