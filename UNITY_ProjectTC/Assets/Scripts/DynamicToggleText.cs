using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DynamicToggleText : MonoBehaviour
{
    [System.Serializable]
    public class ToggleTextPair
    {
        public ToggleGroup toggleGroup; // O grupo de toggles
        public TextMeshProUGUI outputText; // O TextMeshProUGUI que mostrará o toggle selecionado
    }

    [SerializeField]
    private List<ToggleTextPair> toggleTextPairs = new List<ToggleTextPair>(); // Lista de pares ToggleGroup e TextMeshProUGUI

    private void Start()
    {
        // Inscreve-se em eventos de alteração para todos os ToggleGroups
        foreach (var pair in toggleTextPairs)
        {
            if (pair.toggleGroup != null && pair.outputText != null)
            {
                // Inscreve cada toggle no grupo para ouvir quando ele for clicado
                foreach (var toggle in pair.toggleGroup.GetComponentsInChildren<Toggle>())
                {
                    if (toggle != null)
                    {
                        toggle.onValueChanged.AddListener((isOn) => OnToggleValueChanged(pair));
                    }
                }
                // Inicializa o texto com o valor do toggle selecionado no início
                UpdateToggleText(pair);
            }
            else
            {
                Debug.LogWarning("Um dos pares de ToggleGroup ou TextMeshProUGUI está nulo.");
            }
        }
    }

    // Método chamado sempre que um toggle de um grupo é alterado
    private void OnToggleValueChanged(ToggleTextPair pair)
    {
        if (pair != null && pair.toggleGroup != null && pair.outputText != null)
        {
            UpdateToggleText(pair); // Atualiza o texto baseado no toggle selecionado
        }
        else
        {
            Debug.LogWarning("ToggleTextPair, ToggleGroup ou TextMeshProUGUI está nulo.");
        }
    }

    // Atualiza o TextMeshProUGUI com o nome do toggle selecionado
    private void UpdateToggleText(ToggleTextPair pair)
    {
        if (pair != null && pair.toggleGroup != null && pair.outputText != null)
        {
            foreach (var toggle in pair.toggleGroup.GetComponentsInChildren<Toggle>())
            {
                if (toggle.isOn)
                {
                    TextMeshProUGUI toggleText = toggle.GetComponentInChildren<TextMeshProUGUI>();
                    if (toggleText != null)
                    {
                        pair.outputText.text = toggleText.text; // Atualiza o TMP com o texto do Toggle selecionado
                    }
                    else
                    {
                        Debug.LogWarning("Nenhum componente TextMeshProUGUI encontrado no Toggle.");
                    }
                    break;
                }
            }
        }
    }
}
