using System.Collections.Generic;
using UnityEngine;

public class ButtonDataManager : MonoBehaviour
{
    // Lista de dados dos bot�es, preenchida no Inspector
    [SerializeField] private List<ButtonData> buttonDataList = new List<ButtonData>();

    // M�todo para buscar dados pelo ID
    public ButtonData GetButtonDataById(int id)
    {
        // Busca na lista os dados associados ao ID
        return buttonDataList.Find(data => data.id == id);
    }
}
