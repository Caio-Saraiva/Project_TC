using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Events;
using Newtonsoft.Json;
using TMPro;
using System.Collections.Generic;

public class PostRequestHandler : MonoBehaviour
{
    [SerializeField] private GenerateOrderJson generateOrderJson; // Refer�ncia ao script GenerateOrderJson
    [SerializeField] private string apiUrl = "https://example.com/api/post"; // URL da API para enviar a requisi��o POST
    [SerializeField] private UnityEvent onSuccess; // Lista de eventos para sucesso
    [SerializeField] private UnityEvent onFailure; // Lista de eventos para falha
    [SerializeField] private TextMeshProUGUI codPedidoText; // Campo para mostrar o c�digo do pedido

    // M�todo para enviar o pedido via POST ap�s a revis�o
    public void SendOrderToApi()
    {
        // Gera o objeto Pedido chamando o m�todo do GenerateOrderJson
        GenerateOrderJson.Pedido order = generateOrderJson.GenerateOrderJsonAndReturn();

        // Converte o objeto Pedido para JSON
        string orderJson = JsonConvert.SerializeObject(order);

        // Inicia a coroutine para enviar o POST
        StartCoroutine(SendPostRequest(orderJson));
    }

    // Coroutine para enviar a requisi��o POST
    private IEnumerator SendPostRequest(string jsonData)
    {
        UnityWebRequest request = new UnityWebRequest(apiUrl, "POST");
        byte[] bodyRaw = new System.Text.UTF8Encoding().GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError("Erro na requisi��o: " + request.error);
            onFailure?.Invoke(); // Invoca os eventos de falha
        }
        else
        {
            Debug.Log("Requisi��o POST enviada com sucesso!");
            Debug.Log("Resposta da API: " + request.downloadHandler.text);

            // Tenta extrair o cod_pedido da resposta
            string jsonResponse = request.downloadHandler.text;
            var responseObj = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonResponse);
            if (responseObj != null && responseObj.ContainsKey("cod_pedido"))
            {
                string codPedido = responseObj["cod_pedido"].ToString();
                Debug.Log("C�digo do Pedido: #" + codPedido);

                // Atualiza o campo TextMeshProUGUI com o c�digo do pedido
                if (codPedidoText != null)
                {
                    codPedidoText.text = "#" + codPedido;
                }
            }
            onSuccess?.Invoke(); // Invoca os eventos de sucesso
        }
    }
}
