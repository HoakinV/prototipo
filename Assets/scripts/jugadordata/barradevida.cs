using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{
    [SerializeField] private Image barraRelleno;
    [SerializeField] private Jugador jugador;

    void Update()
    {
        if (jugador == null || barraRelleno == null) return;

        float porcentaje = (float)jugador.saludActual / jugador.saludMaxima;
        barraRelleno.fillAmount = porcentaje;
    }
}
