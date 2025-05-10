using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LanternLight : MonoBehaviour
{
    public Light2D lanternLight;        // El Light 2D que simula el farol
    public float minIntensity = 0.5f;   // Intensidad m�nima de la luz
    public float maxIntensity = 1f;     // Intensidad m�xima de la luz
    public float minRadius = 3f;       // Radio m�nimo de la luz
    public float maxRadius = 6f;       // Radio m�ximo de la luz
    public float flickerSpeed = 1f;    // Velocidad del parpadeo
    public float movementSpeedThreshold = 0.5f;  // Umbral de velocidad de movimiento para cambiar el radio y la intensidad
    public float smoothTime = 0.3f;    // Tiempo de suavizado para las transiciones

    private Vector2 lastPosition;
    private float velocityIntensity = 0f;
    private float velocityRadius = 0f;

    void Start()
    {
        lastPosition = transform.position;
    }

    void Update()
    {
        // Obtener la velocidad de movimiento
        Vector2 movement = (Vector2)transform.position - lastPosition;
        lastPosition = transform.position;

        // Calcular la intensidad basada en el movimiento y el parpadeo
        float speed = movement.magnitude / Time.deltaTime;

        // Parpadeo de la intensidad (simula desgaste o variaci�n de la l�mpara)
        lanternLight.intensity = Mathf.SmoothDamp(lanternLight.intensity,
            Mathf.Lerp(minIntensity, maxIntensity, Mathf.PerlinNoise(Time.time * flickerSpeed, 0)),
            ref velocityIntensity, smoothTime);

        // Si el jugador se mueve r�pido, cambia el radio
        if (speed > movementSpeedThreshold)
        {
            // Aumentar el radio cuando se mueve r�pido
            lanternLight.pointLightOuterRadius = Mathf.SmoothDamp(lanternLight.pointLightOuterRadius, maxRadius, ref velocityRadius, smoothTime);
        }
        else
        {
            // Disminuir el radio cuando est� quieto o se mueve lentamente
            lanternLight.pointLightOuterRadius = Mathf.SmoothDamp(lanternLight.pointLightOuterRadius, minRadius, ref velocityRadius, smoothTime);
        }
    }
}
