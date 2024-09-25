using UnityEngine;
using UnityEngine.UI;

public class ToggleManager : MonoBehaviour
{
    public ToggleGroup toggleGroup; // Refer�ncia ao ToggleGroup

    // M�todo para limpar a sele��o de todos os Toggles
    public void ClearSelection()
    {
        // Varre todos os toggles no grupo e desativa a sele��o
        foreach (var toggle in toggleGroup.GetComponentsInChildren<Toggle>())
        {
            toggle.isOn = false;
        }
    }
}
