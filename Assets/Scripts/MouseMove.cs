using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseMove : EventTrigger
{
    public float turn; //��� ������� ����� � �����

    private bool dragging; //��� ����
    private Vector2 offset;
    private RectTransform rectTransform;
    private Canvas canvas;

    private Rigidbody2D rb; //��� ������
    private Vector2 lastMousePos; // ��������� ������� ����
    private Vector2 mouseVelocity; // �������� ����

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>(); //��� ����
        canvas = GetComponentInParent<Canvas>();

        rb = GetComponent<Rigidbody2D>();
        
    }

    public void Update()
    {
        
        if (dragging) //��� ����
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                Input.mousePosition,
                canvas.worldCamera,
                out Vector2 localPoint
            );

            turn = Input.GetAxis("Mouse ScrollWheel"); //���������� ����� �� ����� �����
            Vector3 TurnVe = new Vector3(0, 0, turn * 80);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + TurnVe);

            rectTransform.anchoredPosition = localPoint - offset;

            Vector2 currentMousePos = Input.mousePosition;  // ���������� �������� ���� (�������)
            mouseVelocity = (currentMousePos - lastMousePos) / Time.deltaTime;
            lastMousePos = currentMousePos;
        }
    }

    private void Empty() { gameObject.tag = "CupF"; } //��� ���� ���� �� ������� � ���� ��� ����� ����

    public override void OnPointerDown(PointerEventData eventData) //���� ����� ������ �� ����
    {
        dragging = true; //��� ����

        rb.isKinematic = true;  // ������� ����� ������
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;

        RectTransformUtility.ScreenPointToLocalPointInRectangle( //��� ����
            canvas.transform as RectTransform,
            eventData.position,
            canvas.worldCamera,
            out Vector2 localPoint
        );

        offset = localPoint - rectTransform.anchoredPosition; //���� ����� ���� ����� �� ���� ����� �����
    }

    public override void OnPointerUp(PointerEventData eventData) //���� ����� ���������
    {
        dragging = false; //��� ����
        rb.isKinematic = false; //�� �������� ������ �����

        //������ ������� � �������� ���� ����
        Vector2 throwForce = mouseVelocity * 0.01f;
        throwForce = Vector2.ClampMagnitude(throwForce, 25f);
        rb.velocity = throwForce;
        rb.angularVelocity = Random.Range(-100f, 100f);

        gameObject.tag = "CupT"; // �������� ����
        Invoke("Empty", 0.1f);
    }
}
