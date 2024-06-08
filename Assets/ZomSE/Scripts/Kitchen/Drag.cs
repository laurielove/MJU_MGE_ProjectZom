 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Vector3 originalPosition;
    private Vector3 curPosition;
    private Vector3 targetPosition;
    
    private bool isDragging = false;
    private bool trash = false;
    private string newName;
    private string c_newName;
    private string objName;
    private int objCount;

    [SerializeField]
    private Transform mainPosition;
    [SerializeField]
    private Transform subPosition_1;
    [SerializeField]
    private Transform subPosition_2;
    [SerializeField]
    private Transform subPosition_3;

    [SerializeField]
    private Transform p_mainPosition;
    [SerializeField]
    private Transform p_subPosition_1;
    [SerializeField]
    private Transform p_subPosition_2;
    [SerializeField]
    private Transform p_subPosition_3;
    [SerializeField]
    private Transform p_drinkPosition;

    [SerializeField]
    private Transform plate;

    [SerializeField]
    private GameObject prefab;

    private void Awake()
    {
        originalPosition = transform.position;
        targetPosition = originalPosition;
    }


    private void Update()
    {
        if ((isDragging == true) && (this.gameObject.tag != "cooking"))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = originalPosition.z;
            transform.position = mousePosition;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        if((this.transform.position == originalPosition) && (this.gameObject.name != "Plate") && (this.gameObject.name != "Cup_1") && (this.gameObject.name != "Cup_2"))
        {
            newName = gameObject.name;
            GameObject newObject = Instantiate(prefab, originalPosition, Quaternion.identity);
            newObject.name = newName;

            SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            Color color = spriteRenderer.color;
            color.a = 100f;
            spriteRenderer.color = color;

            BoxCollider2D boxCollider = gameObject.GetComponent<BoxCollider2D>();
            if (gameObject.tag == "main")
            {
                boxCollider.size = new Vector2(1.0f, 1.5f);
            }
            else if (gameObject.tag == "sub")
            {
                boxCollider.size = new Vector2(0.3f, 0.3f);
            }
            
        }
        curPosition = transform.position;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;

        if (trash == true)
        {
            if (this.gameObject.name == "Plate")
            {
                this.transform.position = originalPosition;

                foreach (Transform child in transform)
                {
                    if (child.CompareTag("plate"))
                    {
                        Destroy(child.gameObject);
                        trash = false;
                        
                    }
                }
            }
            else if ((this.gameObject.name == "Cup_1") || (this.gameObject.name == "Cup_2"))
            {
                targetPosition = originalPosition;
                gameObject.tag = "Untagged";
                trash = false;
                StateManger.p_drinkObject = null;

            }
            else
            {
                Destroy(this.gameObject);
            }

            StateManger.p_mainObject = null;
            StateManger.p_sub01Object = null;
            StateManger.p_sub02Object = null;
            StateManger.p_sub03Object = null;
        }

        if ((targetPosition == originalPosition) || (targetPosition == curPosition) || (this.gameObject.tag == "plate"))
        {
            if(this.gameObject.name == "Plate")
            {
                gameObject.GetComponent<CircleCollider2D>().enabled = false;
            }
            else
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }   
        StartCoroutine(MoveToTargetPosition());
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Wastebasket")
        {
            trash = true;
        }

        if (isDragging == true)
        {        
            
            if (col.gameObject.name == "Plate")
            {
                if ((this.gameObject.tag == "main") && (StateManger.p_mainObject == null))
                {
                    targetPosition = p_mainPosition.position;
                    
                }
                else if ((this.gameObject.tag == "sub") || (this.gameObject.tag == "cooked"))
                {
                    if(StateManger.p_sub01Object == null)
                    {
                        targetPosition = p_subPosition_1.position;
                    }
                    else if(StateManger.p_sub02Object == null)
                    {
                        targetPosition = p_subPosition_2.position;
                    }
                    else if(StateManger.p_sub03Object == null)
                    {
                        targetPosition = p_subPosition_3.position;
                    } 
                }
            }
            if (col.gameObject.name == "Table")
            {

                if ((this.gameObject.tag == "drink") && (StateManger.p_drinkObject == null))
                {
                    targetPosition = p_drinkPosition.position;
                }

            }

            if (col.gameObject.name == "Fire")
            {
                if ((StateManger.grillState == false) && (this.gameObject.tag == "main"))
                {
                    targetPosition = mainPosition.position;
                }
                else
                {
                    targetPosition = curPosition;
                }
            }

            else if (col.gameObject.name == "Pan_1")
            {
                if ((StateManger.pan01State == false) && (this.gameObject.tag == "sub"))
                {
                    targetPosition = subPosition_1.position;
                }
                else
                {
                    targetPosition = curPosition;
                }
            }

            else if (col.gameObject.name == "Pan_2")
            {
                if ((StateManger.pan02State == false) && (this.gameObject.tag == "sub"))
                {
                    targetPosition = subPosition_2.position;
                }
                else
                {
                    targetPosition = curPosition;
                }
            }

            else if (col.gameObject.name == "Pan_3")
            {
                if ((StateManger.pan03State == false) && (this.gameObject.tag == "sub"))
                {
                    targetPosition = subPosition_3.position;
                }
                else
                {
                    targetPosition = curPosition;
                }
            }
        }
    }       


    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "Wastebasket")
        {
            trash = false;
        }

        if ((col.gameObject.name == "Fire")&& (StateManger.grillState == true) && (StateManger.FireObject == this.gameObject))
        {
            StateManger.FireObject = null;
            StateManger.grillState = false;
        }
        else if ((col.gameObject.name == "Pan_1")&& (StateManger.pan01State == true) && (StateManger.Pan01Object == this.gameObject))
        {
            StateManger.Pan01Object = null;
            if(this.gameObject.tag != "cooked")
            {
                StateManger.pan01State = false;
            }  
        }
        else if ((col.gameObject.name == "Pan_2")&& (StateManger.pan02State == true) && (StateManger.Pan02Object == this.gameObject))
        {
            StateManger.Pan02Object = null;
            if(this.gameObject.tag != "cooked")
            {
                StateManger.pan02State = false;
            } 
        }
        else if ((col.gameObject.name == "Pan_3")&& (StateManger.pan03State == true) && (StateManger.Pan03Object == this.gameObject))
        {
            StateManger.Pan03Object = null;
            if(this.gameObject.tag != "cooked")
            {
                StateManger.pan03State = false;
            } 

        }
        
        if ((col.gameObject.tag == "cook") && (isDragging == true))
        {
            targetPosition = curPosition;
        }
    }

    private IEnumerator MoveToTargetPosition()
    {
        float duration = 0.5f;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(transform.position,  targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        // transform.position =  targetPosition;

        if(targetPosition == mainPosition.position)
        {
            StateManger.FireObject = this.gameObject;
            StateManger.grillState = true;
        }
        else if(targetPosition == subPosition_1.position)
        {
            StateManger.Pan01Object = this.gameObject;
            StateManger.pan01State = true;
        }
        else if(targetPosition== subPosition_2.position)
        {
            StateManger.Pan02Object = this.gameObject;
            StateManger.pan02State = true;
        }
        else if(targetPosition == subPosition_3.position)
        {
            StateManger.Pan03Object = this.gameObject;
            StateManger.pan03State = true;
        }
        //플레이팅
        if(targetPosition == p_mainPosition.position)
        {
            StateManger.p_mainObject = this.gameObject;
            gameObject.tag = "plate";
        }
        else if(targetPosition == p_subPosition_1.position)
        {
            StateManger.p_sub01Object = this.gameObject;
            gameObject.tag = "plate";
        }
        else if(targetPosition == p_subPosition_2.position)
        {
            StateManger.p_sub02Object = this.gameObject;
            gameObject.tag = "plate";
        }
        else if(targetPosition == p_subPosition_3.position)
        {
            StateManger.p_sub03Object = this.gameObject;
            gameObject.tag = "plate";
        }
        else if(targetPosition == p_drinkPosition.position)
        {
            StateManger.p_drinkObject = this.gameObject;
            gameObject.tag = "plate";
        }
        //삭제
        if((targetPosition == originalPosition) && (gameObject.name != "Plate") && (this.gameObject.name != "Cup_1") && (this.gameObject.name != "Cup_2"))
        {
            Destroy(this.gameObject);
        }

        if(gameObject.tag != "plate")
        {
            if(this.gameObject.name == "Plate")
            {
                gameObject.GetComponent<CircleCollider2D>().enabled = true;
            }
            else
            {
                gameObject.GetComponent<BoxCollider2D>().enabled = true;
            }
        }
        else if ((gameObject.tag == "plate") && (gameObject.name != "Cup_1") && (gameObject.name != "Cup_2"))
        {
            transform.SetParent(plate);
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else if ((gameObject.tag == "plate") && ((gameObject.name == "Cup_1") || (gameObject.name == "Cup_2")))
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
        
    }

}
