using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{

    [SerializeField] TextMesh priceText;
    [SerializeField] ISellable itemToSell;
    [SerializeField] SpriteRenderer itemIcon;

    int itemPrice;
    bool canBePurchased = true;
    bool playerEntered;
    GameObject player;

    public void SetShopItem(int price, ISellable item, Sprite icon)
    {
        itemPrice = price;
        priceText.text = itemPrice.ToString();
        itemToSell = item;
        itemIcon.sprite = icon;
    }

    private void Update()
    {
        if (!playerEntered) return;

        if (Input.GetKeyDown(KeyCode.W) && canBePurchased)
        {
            int currentPoints = PlayerPoints.instance.GetPoints();

            if (itemPrice <= currentPoints)
            {
                itemToSell.Sell(player);
                PlayerPoints.instance.AddPoints(-itemPrice);
                canBePurchased = false;
                itemIcon.sprite = null;
                priceText.text = "XXXX";
            }
            else
            {
                Debug.Log("This item can't be purchased!");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {      
        if (collision.tag == "Player")
        { 
            playerEntered = true;
            player = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerEntered = false;
            player = null;
        }
    }
}
