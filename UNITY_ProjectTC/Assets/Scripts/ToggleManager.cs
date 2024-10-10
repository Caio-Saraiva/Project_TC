using UnityEngine;
using UnityEngine.UI;

public class ToggleManager : MonoBehaviour
{
    public ToggleGroup toggleGroup; // Referência ao ToggleGroup

    // Método para limpar a seleção de todos os Toggles
    public void ClearSelection()
    {
        // Varre todos os toggles no grupo e desativa a seleção
        foreach (var toggle in toggleGroup.GetComponentsInChildren<Toggle>())
        {
            toggle.isOn = false;
        }
    }
}
