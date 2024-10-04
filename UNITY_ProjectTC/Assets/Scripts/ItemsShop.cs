using System.Collections.Generic;

[System.Serializable]
public class ItemsShop
{
    public int cod_produto;       // C�digo do produto (ID �nico)
    public int cod_fornecedor;    // C�digo do fornecedor
    public string nome;           // Nome do produto
    public string categoria;      // Categoria do produto
    public string genero;         // G�nero (M/F)
    public string cor;            // Cor do produto
    public string tamanho;        // Tamanho do produto (PP, P, M, G, GG, XG)
    public double valor_unidade;  // Valor unit�rio do produto
}

[System.Serializable]
public class ItemsShopList
{
    public List<ItemsShop> itemsShop; // Lista dos itens carregados
}
