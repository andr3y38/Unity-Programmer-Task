using UnityEngine;

public class SetShopIndex : MonoBehaviour
{
    [SerializeField] private int SetIndex;

    public void SetShopindex()
    {
        if (SetIndex >= 0 && SetIndex < UnlockableSkinsManager.Instance.shopDetails.Length)
        {
            UnlockableSkinsManager.Instance.ShopItem[UnlockableSkinsManager.Instance.currentIndex].SetActive(false);
            UnlockableSkinsManager.Instance.currentIndex = SetIndex;
            UnlockableSkinsManager.Instance.ShopItem[UnlockableSkinsManager.Instance.currentIndex].SetActive(true);
            UnlockableSkinsManager.Instance.UpdateUI();
            
            Debug.Log(UnlockableSkinsManager.Instance.currentIndex);
        }
    }
}