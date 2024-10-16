using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PostButtonHandler : MonoBehaviour
{
    [SerializeField] private Button targetButton;  // Bot�o especificado no Inspector
    [SerializeField] private float delayTime = 5f; // Tempo de atraso em segundos, especificado no Inspector

    // M�todo para desativar o bot�o
    public void DisableButton()
    {
        if (targetButton != null)
        {
            targetButton.interactable = false;
        }
    }

    // M�todo para ativar o bot�o ap�s x segundos
    public void EnableButtonAfterDelay()
    {
        StartCoroutine(EnableButtonCoroutine());
    }

    // Coroutine que espera x segundos e ativa o bot�o
    private IEnumerator EnableButtonCoroutine()
    {
        yield return new WaitForSeconds(delayTime);
        if (targetButton != null)
        {
            targetButton.interactable = true;
        }
    }
}
