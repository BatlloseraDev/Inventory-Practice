using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{

 
    #region Properties
    [Header("Properties")]
    private GameObject characterToMove;
    [Tooltip("Test the speed of the object")][SerializeField]private float moveSpeed;
    [SerializeField] private UI_Inventory uiIventory; 
    private Rigidbody2D rb; //Rigid Body of the Character
    private float screenWidth;
    private float screenHeight;
    private float quantityToAdd;
    private Animator animator;
    private Vector3 moveDirection;
    private Inventory inventory; 
    #endregion


    private void Awake()
    {
        screenHeight = Camera.main.orthographicSize * 2f;
        screenWidth = screenHeight * ((float)Screen.width / (float)Screen.height);
        characterToMove = gameObject;
        rb = characterToMove.GetComponent<Rigidbody2D>();

        inventory = new Inventory(UseItem);
        

    }

    void Start()
    {
        uiIventory.SetPlayer(this);
        uiIventory.SetInventory(inventory);//sometimes invoke it on awake generates problems 

        //Character or Object
        quantityToAdd = characterToMove.GetComponent<Collider2D>().bounds.size.x/2;
        animator = gameObject.GetComponent<Animator>(); 
   
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemWorld itemWorld = collision.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            //touch the item and add to inventory
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }

    private void UseItem(Item item)
    {
        switch (item.itemType)
        {
            case Item.ItemType.HealthPotion:
                Debug.Log("You used an potion and healed yourself");
                inventory.RemoveItem(new Item { itemType = Item.ItemType.HealthPotion, amount = 1 });
                break;
            case Item.ItemType.Bow:
                Debug.Log("You used your fantastic bow for attack and wasted and arrow");
                //here is the same to the inventory.RemoveItem but on this case avoid throw an arrow when dont exist on the inventary. so make an check for see if exist in the "inventory"(List)
                break;
            case Item.ItemType.InvincibilityPotion:
                Debug.Log("Now you feel so strongly");
                inventory.RemoveItem(new Item { itemType = Item.ItemType.InvincibilityPotion, amount = 1 });
                break;
            case Item.ItemType.Arrow:
                Debug.Log("You have tried shake your arrow back and forth, I don't think you expected that, nothing special has happened");
                break;
            case Item.ItemType.Sword:
                Debug.Log("You feel secure about yourself, you have the sword in your hand, and you make an attack in the air, but there is no enemy, people now look at you weird");
                break;
        }


    }


    // Update is called once per frame
    void Update()
    {
     
        if(Time.timeScale != 0) //when time is 0, you are on the inventory
        {
           Controller();
           ControlSlide();
           FixPosition(); 
        }
        
    }
    private void FixedUpdate()
    {
        
        rb.velocity = moveDirection * moveSpeed;
    }
    private void ControlSlide() //this is  for dont go out of the borders
    {
        
        if (characterToMove.transform.position.x + quantityToAdd>=screenWidth/2 && rb.velocity.x > 0f)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
           
        } else if(characterToMove.transform.position.x - quantityToAdd<=screenWidth/-2 && rb.velocity.x < 0f)
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }
        if(characterToMove.transform.position.y + quantityToAdd>=screenHeight/2 && rb.velocity.y > 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }else if(characterToMove.transform.position.y - quantityToAdd<=screenHeight/-2 && rb.velocity.y < 0f)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0f);
        }
    }




    private void Controller()
    {
        float moveXaxis = 0f;
        float moveYaxis = 0f; 
     
        
        if (Input.GetKey(KeyCode.W))
        {
            moveYaxis = +1f;
            animator.SetTrigger("Up");

        }
       
        if (Input.GetKey(KeyCode.D))
        {
            moveXaxis = +1f;
            animator.SetTrigger("Right");
        }
        

        if (Input.GetKey(KeyCode.A))
        {
            moveXaxis = -1f;
            animator.SetTrigger("Left");
        }
       

        if (Input.GetKey(KeyCode.S))
        {
            moveYaxis = -1f;
            animator.SetTrigger("Down");

        }
        moveDirection = new Vector3(moveXaxis, moveYaxis).normalized;
    }
    void FixPosition() //sometimes the character go out a bit for the velocity, this fix this
    {
        if (characterToMove.transform.position.x + quantityToAdd > screenWidth/2)
        {
            characterToMove.transform.position = new Vector2(screenWidth/2 - quantityToAdd, characterToMove.transform.position.y);
        }
        else if (characterToMove.transform.position.x - quantityToAdd < screenWidth / -2)
        {
            characterToMove.transform.position = new Vector2((screenWidth /-2) + quantityToAdd, characterToMove.transform.position.y);
        }
        if (characterToMove.transform.position.y + quantityToAdd > screenHeight/2)
        {
            characterToMove.transform.position = new Vector2(characterToMove.transform.position.x, screenHeight / 2 - quantityToAdd);
        }
        else if (characterToMove.transform.position.y - quantityToAdd < screenHeight / -2)
        {
            characterToMove.transform.position = new Vector2(characterToMove.transform.position.x, (screenHeight / -2) + quantityToAdd);
        }
    }

   public Vector3 GetPosition()
    {
        return gameObject.transform.position; 
    }


}
