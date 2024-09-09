using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelSelectorController : MonoBehaviour
{
    public ScrollRect scrollRect;  // Refer�ncia � ScrollView
    public int totalItems;  // N�mero total de itens no conte�do da ScrollView
    public float scrollSpeed = 0.2f;  // Velocidade de transi��o

    private Coroutine scrollCoroutine;

    // Fun��o para mover a ScrollView at� um �ndice espec�fico
    public void ScrollToIndex(int targetIndex)
    {
        // Verifica se o �ndice est� dentro dos limites
        if (targetIndex >= 0 && targetIndex < totalItems)
        {
            float targetPosition = CalculateTargetPosition(targetIndex);

            // Interrompe a rolagem anterior, se necess�rio
            if (scrollCoroutine != null)
            {
                StopCoroutine(scrollCoroutine);
            }

            // Inicia a rolagem suave para a nova posi��o
            scrollCoroutine = StartCoroutine(SmoothScroll(targetPosition));
        }
        else
        {
            Debug.LogWarning("�ndice fora do intervalo permitido.");
        }
    }

    // Calcula a posi��o normalizada com base no �ndice
    private float CalculateTargetPosition(int targetIndex)
    {
        return (float)targetIndex / (totalItems - 1);
    }

    // Corrotina para rolar suavemente at� a posi��o desejada
    private IEnumerator SmoothScroll(float targetPosition)
    {
        float startPosition = scrollRect.horizontalNormalizedPosition;
        float elapsedTime = 0f;

        while (elapsedTime < scrollSpeed)
        {
            scrollRect.horizontalNormalizedPosition = Mathf.Lerp(startPosition, targetPosition, elapsedTime / scrollSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Certifica-se de que a posi��o final seja exata
        scrollRect.horizontalNormalizedPosition = targetPosition;
    }
}
