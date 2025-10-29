using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSlotForClient : MonoBehaviour
{
    private Vector3 ClientPlace = new Vector3(274.4f, -133.6f, 0);
    public GameObject moneys;
    private GameObject Cupis;
    private Canvas mainCanvas;

    void Start()
    {
        mainCanvas = FindObjectOfType<Canvas>(); // добавлено — шукаємо головний Canvas при старті
    }

    public void OnTriggerStay2D(Collider2D collision) //якось доказуєм що ця чашка це та з якою потрібно працювати і тоді можна "наливати в чашку кофе"
    {
        if (collision.CompareTag("CupAmericanoT"))
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero; //скидання інерції
            collision.transform.rotation = Quaternion.Euler(0, 0, 0); //виставлення нульового обертання

            RectTransform rt = collision.GetComponent<RectTransform>(); //RectTransform потрібен для кординування по канвасу
            rt.anchoredPosition = ClientPlace;
            Cupis = collision.gameObject; //передаєм дані обєкта на гейм обджект для інших функцій
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == Cupis) Cupis = null; // коли чашка вийшла — забути її
    }

    public void Forclients() //по кнопкі проводи махінації
    {
        if (Cupis != null)
        {
            Destroy(Cupis);

            GameObject newMoney = Instantiate(moneys, mainCanvas.transform);
            RectTransform moneyRect = newMoney.GetComponent<RectTransform>();
            moneyRect.anchoredPosition = ClientPlace;
        }
    }
}
