using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DynamicTextAndImageFiller : MonoBehaviour
{
    [System.Serializable]
    public class TextImageReferencePair
    {
        public TextMeshProUGUI textObject;    // O TextMeshProUGUI que será preenchido
        public string referenceString;        // A string de referência para preencher o TextMeshProUGUI
        public Image imageObject;             // O componente Image que será preenchido
        public Sprite referenceSprite;        // A sprite que será aplicada ao Image
    }

    [SerializeField]
    private List<TextImageReferencePair> textImageReferencePairs = new List<TextImageReferencePair>(); // Lista dinâmica de pares

    private void Awake()
    {
        FillTextAndImages();
    }

    // Método para preencher os TextMeshProUGUI e Imagens com as strings e sprites correspondentes
    public void FillTextAndImages()
    {
        foreach (var pair in textImageReferencePairs)
        {
            // Preenche o TextMeshProUGUI com a string de referência
            if (pair.textObject != null)
            {
                pair.textObject.text = pair.referenceString;
            }

            // Preenche a imagem com a sprite de referência
            if (pair.imageObject != null && pair.referenceSprite != null)
            {
                pair.imageObject.sprite = pair.referenceSprite;
            }
        }
    }
}
