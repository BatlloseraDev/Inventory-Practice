using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using TMPro; 

public class ItemWorld : MonoBehaviour
{
    public static ItemWorld SpawnItemWorld(Vector3 position, Item item)
    {
        Transform transform= Instantiate(ItemAssets.Instance.pfItemWorld, position, Quaternion.identity);

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld; 
    }//to spawn Items in the world
    public static ItemWorld DropItem(Vector3 dropPosition,Item item)
    {
        Vector3 randomDir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        ItemWorld itemWorld =  SpawnItemWorld(dropPosition + randomDir *1.5f,item);//here i multiply with 1.5f because sometimes the item spawn so near 
        itemWorld.GetComponent<Rigidbody2D>().AddForce(randomDir * 2f, ForceMode2D.Impulse);  

        return itemWorld;
    }


    private Item item;
    private SpriteRenderer spriteRenderer;
    private Light2D light2D;
    private TextMeshPro textMeshPro;



    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        light2D = GetComponent<Light2D>();
        textMeshPro = transform.Find("amountText").GetComponent<TextMeshPro>(); 
    }

    public void SetItem(Item item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
        light2D.color = item.GetColor();
        if (item.amount > 1)
        {
            textMeshPro.SetText(item.amount.ToString());
        }
        else
        {
            textMeshPro.SetText("");
        }
        
    }

    public Item GetItem()
    {
        return item; 
    }
    public void DestroySelf()
    {
        Destroy(gameObject); 
    }
}
