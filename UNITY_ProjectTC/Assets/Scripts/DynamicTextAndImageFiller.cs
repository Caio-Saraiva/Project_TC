using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DynamicTextAndImageFiller : MonoBehaviour
{
    [System.Serializable]
    public class TextImageReferencePair
    {
        public TextMeshProUGUI textObject;    // O TextMeshProUGUI que ser� preenchido
        public string referenceString;        // A string de refer�ncia para preencher o TextMeshProUGUI
        public Image imageObject;             // O componente Image que ser� preenchido
        public Sprite referenceSprite;        // A sprite que ser� aplicada ao Image
    }

    [SerializeField]
    private List<TextImageReferencePair> textImageReferencePairs = new List<TextImageReferencePair>(); // Lista din�mica de pares

    private void Awake()
    {
        FillTextAndImages();
    }

    // M�todo para preencher os TextMeshProUGUI e Imagens com as strings e sprites correspondentes
    public void FillTextAndImages()
    {
        foreach (var pair in textImageReferencePairs)
        {
            // Preenche o TextMeshProUGUI com a string de refer�ncia
            if (pair.textObject != null)
            {
                pair.textObject.text = pair.referenceString;
            }

            // Preenche a imagem com a sprite de refer�ncia
            if (pair.imageObject != null && pair.referenceSprite != null)
            {
                pair.imageObject.sprite = pair.referenceSprite;
            }
        }
    }
}
