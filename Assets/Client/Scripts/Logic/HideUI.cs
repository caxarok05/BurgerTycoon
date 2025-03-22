using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideUI : MonoBehaviour
{
    [SerializeField] private GameObject showSprite;
    [SerializeField] private GameObject hideSprite;

    private bool isHidden = false;

    private void Start()
    {
        HideShowUI();
    }

    public void HideShowUI()
    {
        if (!isHidden)
        {
            Camera.main.cullingMask = LayerMask.GetMask("Default", "TransparentFX", "Ignore Raycast", "Water", "UI");
            hideSprite.SetActive(false);
            showSprite.SetActive(true);
            isHidden = true;
        }
        else
        {
            Camera.main.cullingMask = LayerMask.GetMask("Default", "TransparentFX", "Ignore Raycast", "Water");
            hideSprite.SetActive(true);
            showSprite.SetActive(false);
            isHidden = false;
        }
    }
}
