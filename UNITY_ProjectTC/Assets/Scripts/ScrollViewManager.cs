using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScrollViewManager : MonoBehaviour
{
    [SerializeField] private GameObject codPedidoPrefab; // Prefab do item
    [SerializeField] private Transform scrollViewContent; // Conteúdo do ScrollView

    // Lista para armazenar os códigos de pedidos já instanciados
    private List<string> instantiatedCodPedidos = new List<string>();

    // Método para verificar se o código já foi instanciado
    public void InstantiatePrefabIfNotExists(string codPedido)
    {
        // Verifica se o codPedido já está na lista
        if (!instantiatedCodPedidos.Contains(codPedido))
        {
            // Instancia o prefab no ScrollView
            GameObject instance = Instantiate(codPedidoPrefab, scrollViewContent);
            TextMeshProUGUI txtCodPedido = instance.transform.Find("txt-codpedido").GetComponent<TextMeshProUGUI>();

            // Atribui o texto ao TextMeshProUGUI
            if (txtCodPedido != null)
            {
                txtCodPedido.text = "#" + codPedido;
            }

            // Adiciona o codPedido à lista de instanciados
            instantiatedCodPedidos.Add(codPedido);
        }
        else
        {
            Debug.Log("Item com cod_pedido #" + codPedido + " já foi instanciado.");
        }
    }

    // Método para carregar os pedidos salvos nos PlayerPrefs e instanciar no ScrollView
    public void LoadCodPedidosFromPrefs()
    {
        // Carrega os pedidos salvos no PlayerPrefs
        string savedPedidos = PlayerPrefs.GetString("cod_pedidos", "");

        // Se houver pedidos salvos, separa por vírgula e instancia cada um
        if (!string.IsNullOrEmpty(savedPedidos))
        {
            string[] codPedidosArray = savedPedidos.Split(',');

            foreach (string codPedido in codPedidosArray)
            {
                InstantiatePrefabIfNotExists(codPedido);
            }
        }
        else
        {
            Debug.Log("Nenhum pedido salvo encontrado.");
        }
    }

    // Método para limpar todos os pedidos instanciados
    public void ClearScrollView()
    {
        foreach (Transform child in scrollViewContent)
        {
            Destroy(child.gameObject);
        }

        // Limpa a lista de códigos instanciados
        instantiatedCodPedidos.Clear();
    }
}
