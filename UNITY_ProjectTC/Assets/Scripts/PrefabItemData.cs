using System.Collections.Generic;
using UnityEngine;

public class PrefabItemData : MonoBehaviour
{
    // Lista de códigos dos produtos agrupados (itens com o mesmo nome, mas com variações de cor, tamanho, etc.)
    public List<int> codProdutoList = new List<int>();

    // Código do produto único (para quando o item é instanciado no carrinho)
    public int codProduto;

    // Método para configurar os itens agrupados, armazenando os códigos dos produtos
    public void SetGroupedItems(List<ItemsShop> groupedItems)
    {
        codProdutoList = groupedItems.ConvertAll(item => item.cod_produto);
    }

    // Método para retornar o código do primeiro produto (para exibir como padrão, por exemplo)
    public int GetFirstCodProduto()
    {
        if (codProdutoList != null && codProdutoList.Count > 0)
        {
            return codProdutoList[0]; // Retorna o primeiro código de produto como referência
        }
        else
        {
            Debug.LogError("Nenhum produto disponível no grupo.");
            return -1; // Retorna um valor inválido caso não haja produtos
        }
    }

    // Método para obter todos os códigos de produtos agrupados
    public List<int> GetCodProdutoList()
    {
        return codProdutoList;
    }

    // Método para definir um único código de produto (quando o item é adicionado ao carrinho)
    public void SetCodProduto(int cod)
    {
        codProduto = cod; // Atribui o código do produto selecionado
    }

    // Método para adicionar um código de produto ao grupo (se precisar dinamicamente)
    public void AddCodProduto(int codProduto)
    {
        if (!codProdutoList.Contains(codProduto))
        {
            codProdutoList.Add(codProduto);
        }
    }

    // Método para remover um código de produto do grupo (opcional, para casos de alteração)
    public void RemoveCodProduto(int codProduto)
    {
        if (codProdutoList.Contains(codProduto))
        {
            codProdutoList.Remove(codProduto);
        }
    }
}
