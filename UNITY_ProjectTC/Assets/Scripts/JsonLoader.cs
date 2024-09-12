using UnityEngine;

public class JsonLoader : MonoBehaviour
{
    public string jsonFileName = "items"; // Nome do arquivo JSON dentro da pasta Resources
    public ItemsShopList itemsShopList;

    void Start()
    {
        LoadJson();
    }

    void LoadJson()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(jsonFileName);
        if (jsonFile != null)
        {
            itemsShopList = JsonUtility.FromJson<ItemsShopList>(jsonFile.text);
            Debug.Log("JSON loaded successfully. Total elements: " + itemsShopList.itemsShop.Count);
        }
        else
        {
            Debug.LogError("JSON file not found in Resources/" + jsonFileName);
        }
    }

    public ItemsShop GetElementById(int id)
    {
/*        Debug.Log("Searching for element with ID: " + id);
        foreach (var element in itemsShopList.itemsShop)
        {
            Debug.Log("Checking element with ID: " + element.id);
        }*/
        return itemsShopList.itemsShop.Find(item => item.id == id);
    }
}
