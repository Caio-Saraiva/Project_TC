using UnityEngine;
using UnityEngine.Events;

public class ButtonHandler : MonoBehaviour
{
    [Header("Eventos Dinâmicos")]
    public UnityEvent buttonEvents; // Lista de eventos configurados no Inspector

    // Método para configurar e executar os eventos dinâmicos do botão
    public void SetupButtonEvents()
    {
        // Executa todos os eventos configurados no Inspector quando o botão for clicado
        if (buttonEvents != null)
        {
            buttonEvents.Invoke();
        }
    }

    // Chama o SetupButtonEvents ao clicar
    public void OnButtonClick()
    {
        SetupButtonEvents();
    }
}
