using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseMove : EventTrigger
{
    public float turn; //для поврота обєкта в руках

    private bool dragging; //для руху
    private Vector2 offset;
    private RectTransform rectTransform;
    private Canvas canvas;

    private Rigidbody2D rb; //для фізики
    private Vector2 lastMousePos; // попередня позиція миші
    private Vector2 mouseVelocity; // швидкість миші

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>(); //для руху
        canvas = GetComponentInParent<Canvas>();

        rb = GetComponent<Rigidbody2D>();
        
    }

    public void Update()
    {
        
        if (dragging) //для руху
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                Input.mousePosition,
                canvas.worldCamera,
                out Vector2 localPoint
            );

            turn = Input.GetAxis("Mouse ScrollWheel"); //повертання обєктів що тримає мишка
            Vector3 TurnVe = new Vector3(0, 0, turn * 80);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + TurnVe);

            rectTransform.anchoredPosition = localPoint - offset;

            Vector2 currentMousePos = Input.mousePosition;  // Розрахунок швидкості миші (інерція)
            mouseVelocity = (currentMousePos - lastMousePos) / Time.deltaTime;
            lastMousePos = currentMousePos;
        }
    }

    private void Empty() { gameObject.tag = "CupF"; } //тег шоби обєкт не тепався в зону без участі миші

    public override void OnPointerDown(PointerEventData eventData) //коли мишку нажали на обєкті
    {
        dragging = true; //для руху

        rb.isKinematic = true;  // прибирає вплив фізики
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;

        RectTransformUtility.ScreenPointToLocalPointInRectangle( //для руху
            canvas.transform as RectTransform,
            eventData.position,
            canvas.worldCamera,
            out Vector2 localPoint
        );

        offset = localPoint - rectTransform.anchoredPosition; //шоби можна було брати за любу точку обєкта
    }

    public override void OnPointerUp(PointerEventData eventData) //коли мишку відпустили
    {
        dragging = false; //для руху
        rb.isKinematic = false; //має включати фізику назад

        //додаємо інерцію у напрямку руху миші
        Vector2 throwForce = mouseVelocity * 0.01f;
        throwForce = Vector2.ClampMagnitude(throwForce, 25f);
        rb.velocity = throwForce;
        rb.angularVelocity = Random.Range(-100f, 100f);

        gameObject.tag = "CupT"; // включеня тегу
        Invoke("Empty", 0.1f);
    }
}
