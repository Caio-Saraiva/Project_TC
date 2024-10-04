using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class JsonLoader : MonoBehaviour
{
    public ItemsShopList itemsShopList; // Lista que armazenará os dados carregados
    private bool jsonLoaded = false;    // Flag para indicar se o JSON foi carregado com sucesso

    [Header("API Settings")]
    public string apiUrl; // URL da API, configurada via Inspector

    public void Awake()
    {
        LoadData();
    }

    // Método para verificar se o JSON já foi carregado
    public bool IsJsonLoaded()
    {
        return jsonLoaded;
    }

    public void LoadData()
    {
        // Inicia a chamada assíncrona para carregar o JSON da API
        StartCoroutine(LoadJson(apiUrl));
    }

    private IEnumerator LoadJson(string url)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                string jsonResult = www.downloadHandler.text;

                // Exibe o JSON carregado no console para depuração
                Debug.Log("Resposta JSON carregada da API: " + jsonResult);

                // Converte o JSON para a estrutura de dados ItemsShopList
                itemsShopList = JsonUtility.FromJson<ItemsShopList>("{\"itemsShop\":" + jsonResult + "}");

                jsonLoaded = true;
                Debug.Log("JSON carregado com sucesso.");
            }
            else
            {
                Debug.LogError("Erro ao carregar JSON: " + www.error);
            }
        }
    }

    // Método para buscar um item pelo código do produto (cod_produto)
    public ItemsShop GetElementByCodProduto(int codProduto)
    {
        return itemsShopList.itemsShop.Find(item => item.cod_produto == codProduto);
    }
}
