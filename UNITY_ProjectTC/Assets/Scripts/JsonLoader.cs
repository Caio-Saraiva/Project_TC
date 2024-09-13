using UnityEngine;

public class JsonLoader : MonoBehaviour
{
    public string jsonFileName = "items"; // Nome do arquivo JSON dentro da pasta Resources
    public ItemsShopList itemsShopList;

    private bool isJsonLoaded = false; // Flag para verificar se o JSON foi carregado

    void Awake()
    {
        LoadJson();
    }

    void LoadJson()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(jsonFileName);
        if (jsonFile != null)
        {
            itemsShopList = JsonUtility.FromJson<ItemsShopList>(jsonFile.text);
            isJsonLoaded = true; // Sinaliza que o JSON foi carregado
            Debug.Log("JSON carregado com sucesso. Total de elementos: " + itemsShopList.itemsShop.Count);
        }
        else
        {
            Debug.LogError("JSON não encontrado em Resources/" + jsonFileName);
        }
    }

    public bool IsJsonLoaded()
    {
        return isJsonLoaded; // Método para verificar se o JSON foi carregado
    }

    public ItemsShop GetElementById(int id)
    {
        // Verifica e retorna o item com o ID correspondente
        return itemsShopList.itemsShop.Find(item => item.id == id);
    }

}
