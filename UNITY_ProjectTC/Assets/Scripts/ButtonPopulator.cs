using UnityEngine;
using UnityEngine.UI;
using TMPro; // Certifique-se de usar TextMeshPro

public class ButtonPopulator : MonoBehaviour
{
    public JsonLoader jsonLoader;
    public int elementId; // ID do elemento a ser buscado
    public Button button; // Refer�ncia ao bot�o que ter� seu sprite atualizado
    public TextMeshProUGUI titleText; // Texto do t�tulo
    public TextMeshProUGUI priceText; // Texto do pre�o

    void Start() // Ou Awake()
    {
        PopulateButton();
    }

    void PopulateButton()
    {
        // Buscar o elemento pelo ID
        ItemsShop element = jsonLoader.GetElementById(elementId);

        if (element != null)
        {
            // Atualizar o t�tulo
            titleText.text = element.title;

            // Atualizar o pre�o com duas casas decimais
            priceText.text = element.price.ToString("F2");

            // Carregar o sprite para o bot�o
            Sprite sprite = Resources.Load<Sprite>(element.image);
            if (sprite != null)
            {
                button.image.sprite = sprite;
            }
            else
            {
                Debug.LogError("Image not found at Resources/" + element.image);
            }
        }
        else
        {
            Debug.LogError("Element with ID " + elementId + " not found.");
        }
    }
}
