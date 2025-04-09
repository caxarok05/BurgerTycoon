using Client.Services;
using Client.Services.MoneyService;
using UnityEditor;
using UnityEngine;

public class Tools
{
    [MenuItem("Tools/ClearPrefs")]
    public static void ClearPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
    }

}
