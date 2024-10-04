using UnityEngine;
using UnityEngine.Events;

public class ButtonHandler : MonoBehaviour
{
    [Header("Eventos Din�micos")]
    public UnityEvent buttonEvents; // Lista de eventos configurados no Inspector

    // M�todo para configurar e executar os eventos din�micos do bot�o
    public void SetupButtonEvents()
    {
        // Executa todos os eventos configurados no Inspector quando o bot�o for clicado
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
