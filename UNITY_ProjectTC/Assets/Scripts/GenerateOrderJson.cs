using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Newtonsoft.Json;

public class GenerateOrderJson : MonoBehaviour
{
    [SerializeField] private Transform cartContent; // Content do ScrollView onde os itens são instanciados
    [SerializeField] private Transform outputContent; // Content onde os TextMeshProUGUI serão instanciados
    [SerializeField] private TextMeshProUGUI textPrefab; // Prefab do TextMeshProUGUI que será instanciado
    [SerializeField] private int codCliente = 1; // Código do cliente, configurável no Inspector
    private JsonLoader jsonLoader; // Referência ao JsonLoader para acessar o JSON carregado

    // Classe para definir a estrutura do item no pedido
    [Serializable]
    public class PedidoItem
    {
        public int cod_produto;
        public int qtd_pedido;
        public double valor_item;
    }

    // Classe para definir a estrutura completa do pedido
    [Serializable]
    public class Pedido
    {
        public int cod_cliente;
        public string data_pedido;
        public double valor_pedido;
        public List<PedidoItem> items = new List<PedidoItem>();
    }

    private void Start()
    {
        jsonLoader = FindObjectOfType<JsonLoader>(); // Obtém o JsonLoader na cena
    }

    // Método chamado ao pressionar o botão para gerar o JSON
    public void GenerateJson()
    {
        Pedido pedido = new Pedido();
        pedido.cod_cliente = codCliente; // Define o código do cliente

        // Define a data do pedido no formato "ano-mes-dia"
        pedido.data_pedido = DateTime.Now.ToString("yyyy-MM-dd");

        Dictionary<int, PedidoItem> itensConsolidados = new Dictionary<int, PedidoItem>();
        double valorTotal = 0.0;

        // Limpa o conteúdo existente antes de instanciar novos itens
        foreach (Transform child in outputContent)
        {
            Destroy(child.gameObject);
        }

        // Itera pelos itens no carrinho
        foreach (Transform item in cartContent)
        {
            PrefabItemData prefabItemData = item.GetComponent<PrefabItemData>();
            if (prefabItemData != null)
            {
                int codProduto = prefabItemData.codProduto;
                ItemsShop itemData = jsonLoader.GetElementByCodProduto(codProduto);

                if (itemData != null)
                {
                    double valorUnitario = Math.Round(itemData.valor_unidade, 2); // Arredonda para 2 casas decimais

                    // Se o item já existe no dicionário, acumula a quantidade e o valor
                    if (itensConsolidados.ContainsKey(codProduto))
                    {
                        itensConsolidados[codProduto].qtd_pedido += 1;
                        itensConsolidados[codProduto].valor_item += valorUnitario;
                    }
                    else
                    {
                        // Cria um novo item e adiciona ao dicionário
                        PedidoItem novoItem = new PedidoItem
                        {
                            cod_produto = codProduto,
                            qtd_pedido = 1, // Inicialmente a quantidade é 1
                            valor_item = valorUnitario
                        };
                        itensConsolidados[codProduto] = novoItem;
                    }

                    valorTotal += valorUnitario; // Calcula o valor total do pedido
                }
                else
                {
                    Debug.LogError("Item com ID " + codProduto + " não encontrado no JSON carregado.");
                }
            }
        }

        // Converte o dicionário de itens consolidados para a lista de itens do pedido
        pedido.items = new List<PedidoItem>(itensConsolidados.Values);

        // Itera sobre o dicionário de itens consolidados e instancia um TextMeshProUGUI para cada item
        foreach (var pedidoItem in itensConsolidados.Values)
        {
            ItemsShop itemData = jsonLoader.GetElementByCodProduto(pedidoItem.cod_produto);
            if (itemData != null)
            {
                // Cria uma instância do TextMeshProUGUI para cada item
                TextMeshProUGUI newText = Instantiate(textPrefab, outputContent);

                // Formata o texto com os detalhes do item
                string itemLine = $">> <b>Produto:</b> {itemData.nome}    <b>Tamanho:</b> {itemData.tamanho}    <b>Cor:</b> {itemData.cor}\n";
                string valueLine = $"      <b>Quantidade:</b> {pedidoItem.qtd_pedido}        <b>Valor por unidade:</b> R$ {Math.Round(itemData.valor_unidade, 2):F2}\n";

                // Define o texto no novo TextMeshProUGUI instanciado
                newText.text = itemLine + valueLine;
            }
        }

        // Define o valor total do pedido arredondado
        pedido.valor_pedido = Math.Round(valorTotal, 2);

        // Gera o JSON (usando JsonConvert ou JsonUtility)
        string jsonResult = JsonConvert.SerializeObject(pedido, Formatting.Indented); // Para Json.NET
        Debug.Log(jsonResult);

        // Se estiver usando JsonUtility (substitua o código acima por este)
        // string jsonResult = JsonUtility.ToJson(pedido, true);
        // Debug.Log(jsonResult);
    }
}
