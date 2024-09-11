using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PrefabItemController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private Image itemImage;

    // Método para configurar os campos do prefab
    public void Setup(string title, string description, Sprite image)
    {
        if (titleText != null)
            titleText.text = title;

        if (descriptionText != null)
            descriptionText.text = description;

        if (itemImage != null)
            itemImage.sprite = image;
    }
}
