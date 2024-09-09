using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScrollToIndex : MonoBehaviour
{
    public ScrollRect scrollRect;  // O componente ScrollRect
    public RectTransform content;  // O conteúdo dentro do ScrollView
    public bool isVertical = true; // Define se o ScrollView é vertical ou horizontal
    public float scrollDuration = 0.5f; // Duração da rolagem suave

    // Método para rolar até o índice
    public void ScrollToItem(int itemIndex)
    {
        // Pegue o total de itens no conteúdo
        int totalItems = content.childCount;

        // Calcule a posição percentual (0.0 para o topo e 1.0 para o final)
        float targetPosition = (float)itemIndex / (float)(totalItems - 1);
        targetPosition = Mathf.Clamp01(targetPosition); // Limite entre 0 e 1

        // Inicia a coroutine para rolar suavemente até o alvo
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
            // Função de interpolação Ease In-Out
            float t = elapsedTime / scrollDuration;
            t = t * t * (3f - 2f * t); // Interpolação Ease In-Out

            float newPosition = Mathf.Lerp(startPosition, targetPosition, t);

            // Atualiza a posição do Scroll dependendo da orientação
            if (isVertical)
                scrollRect.verticalNormalizedPosition = 1 - newPosition; // Inverte para que 1 seja o topo
            else
                scrollRect.horizontalNormalizedPosition = newPosition;

            yield return null;
        }

        // Garante que a posição final seja exatamente a desejada
        if (isVertical)
            scrollRect.verticalNormalizedPosition = 1 - targetPosition;
        else
            scrollRect.horizontalNormalizedPosition = targetPosition;
    }
}
