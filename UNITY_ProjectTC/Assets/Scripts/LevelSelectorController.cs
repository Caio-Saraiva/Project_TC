using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelSelectorController : MonoBehaviour
{
    public ScrollRect scrollRect;  // Referência à ScrollView
    public int totalItems;  // Número total de itens no conteúdo da ScrollView
    public float scrollSpeed = 0.2f;  // Velocidade de transição

    private Coroutine scrollCoroutine;

    // Função para mover a ScrollView até um índice específico
    public void ScrollToIndex(int targetIndex)
    {
        // Verifica se o índice está dentro dos limites
        if (targetIndex >= 0 && targetIndex < totalItems)
        {
            float targetPosition = CalculateTargetPosition(targetIndex);

            // Interrompe a rolagem anterior, se necessário
            if (scrollCoroutine != null)
            {
                StopCoroutine(scrollCoroutine);
            }

            // Inicia a rolagem suave para a nova posição
            scrollCoroutine = StartCoroutine(SmoothScroll(targetPosition));
        }
        else
        {
            Debug.LogWarning("Índice fora do intervalo permitido.");
        }
    }

    // Calcula a posição normalizada com base no índice
    private float CalculateTargetPosition(int targetIndex)
    {
        return (float)targetIndex / (totalItems - 1);
    }

    // Corrotina para rolar suavemente até a posição desejada
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

        // Certifica-se de que a posição final seja exata
        scrollRect.horizontalNormalizedPosition = targetPosition;
    }
}
