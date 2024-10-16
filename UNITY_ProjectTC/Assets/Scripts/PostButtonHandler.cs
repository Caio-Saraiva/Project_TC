using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PostButtonHandler : MonoBehaviour
{
    [SerializeField] private Button targetButton;  // Botão especificado no Inspector
    [SerializeField] private float delayTime = 5f; // Tempo de atraso em segundos, especificado no Inspector

    // Método para desativar o botão
    public void DisableButton()
    {
        if (targetButton != null)
        {
            targetButton.interactable = false;
        }
    }

    // Método para ativar o botão após x segundos
    public void EnableButtonAfterDelay()
    {
        StartCoroutine(EnableButtonCoroutine());
    }

    // Coroutine que espera x segundos e ativa o botão
    private IEnumerator EnableButtonCoroutine()
    {
        yield return new WaitForSeconds(delayTime);
        if (targetButton != null)
        {
            targetButton.interactable = true;
        }
    }
}
