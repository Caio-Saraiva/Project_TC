using System.Collections.Generic;

[System.Serializable]
public class ItemsShop
{
    public int id;
    public string title;
    public double price;
    public string image;
    public string season;
    public string type;
}

[System.Serializable]
public class ItemsShopList
{
    public List<ItemsShop> itemsShop;
}
