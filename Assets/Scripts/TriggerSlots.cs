using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;

public class TriggerSlots : MonoBehaviour
{   Vector3 CoffMahinePlase = new Vector3(-249.8f, -88.3f, 0);
    public GameObject Cupis;
    public Sprite CupAmerima;

    public void OnTriggerStay2D(Collider2D collision) //якось доказуєм що ця чашка це та з якою потрібно працювати і тоді можна "наливати в чашку кофе"
    {
        if (collision.CompareTag("CupT"))
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero; //скидання інерції
            collision.transform.rotation = Quaternion.Euler(0,0,0); //виставлення нульового обертання

            RectTransform rt = collision.GetComponent<RectTransform>(); //RectTransform потрібен для кординування по канвасу
            rt.anchoredPosition = CoffMahinePlase;
            Cupis = collision.gameObject; //передаєм дані обєкта на гейм обджект для інших функцій
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == Cupis) Cupis = null; // коли чашка вийшла — забути її
    }

    public void AddCoffe() //по кнопкі проводи махінації
    {
        if (Cupis != null) 
        { 
            Cupis.GetComponent<Image>().sprite = CupAmerima;
            Cupis.tag = "CupAmericano";
        }
        
    }
}
