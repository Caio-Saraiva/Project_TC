using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScrollToIndex : MonoBehaviour
{
    public ScrollRect scrollRect;  // O componente ScrollRect
    public RectTransform content;  // O conte�do dentro do ScrollView
    public bool isVertical = true; // Define se o ScrollView � vertical ou horizontal
    public float scrollDuration = 0.5f; // Dura��o da rolagem suave

    // M�todo para rolar at� o �ndice
    public void ScrollToItem(int itemIndex)
    {
        // Pegue o total de itens no conte�do
        int totalItems = content.childCount;

        // Calcule a posi��o percentual (0.0 para o topo e 1.0 para o final)
        float targetPosition = (float)itemIndex / (float)(totalItems - 1);
        targetPosition = Mathf.Clamp01(targetPosition); // Limite entre 0 e 1

        // Inicia a coroutine para rolar suavemente at� o alvo
        StartCoroutine(SmoothScroll(targetPosition));
    }

    // Coroutine para realizar a rolagem suave
    private IEnumerator SmoothScroll(float targetPosition)
    {
        float elapsedTime = 0f;
        float startPosition = isVertical ? scrollRect.verticalNormalizedPosition : scrollRect.horizontalNormalizedPosition;

        while (elapsedTime < scrollDuration)
        {
            elapsedTime += Time.deltaTime;
            // Fun��o de interpola��o Ease In-Out
            float t = elapsedTime / scrollDuration;
            t = t * t * (3f - 2f * t); // Interpola��o Ease In-Out

            float newPosition = Mathf.Lerp(startPosition, targetPosition, t);

            // Atualiza a posi��o do Scroll dependendo da orienta��o
            if (isVertical)
                scrollRect.verticalNormalizedPosition = 1 - newPosition; // Inverte para que 1 seja o topo
            else
                scrollRect.horizontalNormalizedPosition = newPosition;

            yield return null;
        }

        // Garante que a posi��o final seja exatamente a desejada
        if (isVertical)
            scrollRect.verticalNormalizedPosition = 1 - targetPosition;
        else
            scrollRect.horizontalNormalizedPosition = targetPosition;
    }
}
