using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemSpawner : MonoBehaviour
{
    [SerializeField]
    ItemAbility it;
    [SerializeField]
    ItemStats[] allItems;
    [SerializeField]
    ItemStats currentItem;
    [SerializeField]
    float raritySteepness;
    [SerializeField]
    private TMP_Text description, cost;
    [SerializeField]
    private Button button;
    [SerializeField]
    private Image image;
    
    private string itemName;
    private int itemCost = 0;
    private float totalItems;
    public void SpawnItem() {
        if (currentItem != null) currentItem = null;
        int item = RarityEquation();
        itemCost = CalculateCost(item);
        currentItem = allItems[item];

        description.text = currentItem.description + "\n$" + itemCost;
        cost.text = itemCost.ToString();
        itemName = currentItem.name;
        image.sprite = currentItem.image;
    }
    private int RarityEquation() {
        totalItems = allItems.Length;
        float rand = Random.Range(0, 1f);
        return (int) (Mathf.Pow(rand, raritySteepness) * totalItems);
    }
    private int CalculateCost(int rarity) {
        return (int) (Mathf.Pow(rarity, 1.5f) * 2);
    }
    public void ReplaceSide() {
        if (GUI.gui.GetMoney() > itemCost) {
            GUI.gui.AddMoney(-itemCost);
            it.ActivateItem(itemName);
            gameObject.SetActive(false);
        }
    }
}
