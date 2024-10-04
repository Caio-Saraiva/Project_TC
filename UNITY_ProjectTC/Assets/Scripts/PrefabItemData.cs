using System.Collections.Generic;
using UnityEngine;

public class PrefabItemData : MonoBehaviour
{
    // Lista de c�digos dos produtos agrupados (itens com o mesmo nome, mas com varia��es de cor, tamanho, etc.)
    public List<int> codProdutoList = new List<int>();

    // C�digo do produto �nico (para quando o item � instanciado no carrinho)
    public int codProduto;

    // M�todo para configurar os itens agrupados, armazenando os c�digos dos produtos
    public void SetGroupedItems(List<ItemsShop> groupedItems)
    {
        codProdutoList = groupedItems.ConvertAll(item => item.cod_produto);
    }

    // M�todo para retornar o c�digo do primeiro produto (para exibir como padr�o, por exemplo)
    public int GetFirstCodProduto()
    {
        if (codProdutoList != null && codProdutoList.Count > 0)
        {
            return codProdutoList[0]; // Retorna o primeiro c�digo de produto como refer�ncia
        }
        else
        {
            Debug.LogError("Nenhum produto dispon�vel no grupo.");
            return -1; // Retorna um valor inv�lido caso n�o haja produtos
        }
    }

    // M�todo para obter todos os c�digos de produtos agrupados
    public List<int> GetCodProdutoList()
    {
        return codProdutoList;
    }

    // M�todo para definir um �nico c�digo de produto (quando o item � adicionado ao carrinho)
    public void SetCodProduto(int cod)
    {
        codProduto = cod; // Atribui o c�digo do produto selecionado
    }

    // M�todo para adicionar um c�digo de produto ao grupo (se precisar dinamicamente)
    public void AddCodProduto(int codProduto)
    {
        if (!codProdutoList.Contains(codProduto))
        {
            codProdutoList.Add(codProduto);
        }
    }

    // M�todo para remover um c�digo de produto do grupo (opcional, para casos de altera��o)
    public void RemoveCodProduto(int codProduto)
    {
        if (codProdutoList.Contains(codProduto))
        {
            codProdutoList.Remove(codProduto);
        }
    }
}
