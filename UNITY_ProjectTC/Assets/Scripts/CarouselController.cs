using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CarouselController : MonoBehaviour
{
    public ScrollRect scrollRect;  // A refer�ncia � ScrollView
    public int totalItems;  // O n�mero total de itens no conte�do da ScrollView
    public float scrollSpeed = 0.2f;  // Velocidade de transi��o

    private int currentIndex = 0;
    private float targetPosition;
    private Coroutine scrollCoroutine;

    void Start()
    {
        // Ajusta a posi��o inicial
        SetScrollPosition(0);
    }

    public void ScrollLeft()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            StartScroll();
        }
    }

    public void ScrollRight()
    {
        if (currentIndex < totalItems - 1)
        {
            currentIndex++;
            StartScroll();
        }
    }

    private void StartScroll()
    {
        targetPosition = (float)currentIndex / (totalItems - 1);

        if (scrollCoroutine != null)
        {
            StopCoroutine(scrollCoroutine);
        }
        scrollCoroutine = StartCoroutine(SmoothScroll(targetPosition));
    }

    private IEnumerator SmoothScroll(float targetPosition)
    {
        float startPosition = scrollRect.horizontalNormalizedPosition;
        float elapsedTime = 0;

        while (elapsedTime < scrollSpeed)
        {
            scrollRect.horizontalNormalizedPosition = Mathf.Lerp(startPosition, targetPosition, elapsedTime / scrollSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        scrollRect.horizontalNormalizedPosition = targetPosition;
    }

    private void SetScrollPosition(float position)
    {
        scrollRect.horizontalNormalizedPosition = position;
    }
}
