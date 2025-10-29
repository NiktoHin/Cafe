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
        mainCanvas = FindObjectOfType<Canvas>(); // ��������� � ������ �������� Canvas ��� �����
    }

    public void OnTriggerStay2D(Collider2D collision) //����� ������� �� �� ����� �� �� � ���� ������� ��������� � ��� ����� "�������� � ����� ����"
    {
        if (collision.CompareTag("CupAmericanoT"))
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero; //�������� �������
            collision.transform.rotation = Quaternion.Euler(0, 0, 0); //����������� ��������� ���������

            RectTransform rt = collision.GetComponent<RectTransform>(); //RectTransform ������� ��� ������������ �� �������
            rt.anchoredPosition = ClientPlace;
            Cupis = collision.gameObject; //������� ��� ����� �� ���� ������� ��� ����� �������
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == Cupis) Cupis = null; // ���� ����� ������ � ������ ��
    }

    public void Forclients() //�� ����� ������� ���������
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
