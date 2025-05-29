using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float parallaxSpeedMultiplier = 0.5f; // NOWA: Mnożnik prędkości paralaksy (do edycji w Inspektorze)
    public float backgroundWidth = 20f;          // Szerokość pojedynczej grafiki tła w jednostkach Unity

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position; // Zapisujemy początkową pozycję tła
    }

    void Update()
    {
        // Sprawdzamy, czy gra nie jest zatrzymana
        if (Time.timeScale <= 0) return;

        float currentGlobalSpeed = 0f;
        if (GameManager.instance != null)
        {
            currentGlobalSpeed = GameManager.instance.currentGameSpeed;
        }
        else
        {
            // Jeśli GameManager nie jest dostępny, używamy jakiejś domyślnej prędkości, np. 2f
            currentGlobalSpeed = 2f;
        }

        // Obliczamy prędkość ruchu tła
        // Tło porusza się wolniej niż reszta obiektów, aby stworzyć efekt paralaksy
        float scrollSpeed = currentGlobalSpeed * parallaxSpeedMultiplier;

        // Przesuwamy tło w lewo
        transform.position += Vector3.left * scrollSpeed * Time.deltaTime;

        // Sprawdzamy, czy tło wyszło poza ekran i wymaga zapętlenia
        // Jeśli lewy brzeg tła (aktualna pozycja X - połowa szerokości) przeszedł poza punkt początkowy
        if (transform.position.x < startPosition.x - backgroundWidth)
        {
            // Przesuwamy tło z powrotem na początek, aby stworzyć efekt zapętlenia
            transform.position = startPosition;
            // Lub:
            // float offset = Mathf.Repeat(Time.time * scrollSpeed, backgroundWidth);
            // transform.position = startPosition + Vector3.left * offset;
        }
    }
}