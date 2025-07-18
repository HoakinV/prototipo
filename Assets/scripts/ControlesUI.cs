using UnityEngine;
using UnityEngine.UI;

public class ControlesUI : MonoBehaviour
{
    private PlayerController2D jugador;

    public Button botonIzquierda;
    public Button botonDerecha;

    public void SetJugador(PlayerController2D nuevoJugador)
    {
        jugador = nuevoJugador;
    }

    public void BotonIzquierdaDown()
    {
        jugador?.IniciarMoverIzquierda();
    }

    public void BotonIzquierdaUp()
    {
        jugador?.DetenerMoverIzquierda();
    }

    public void BotonDerechaDown()
    {
        jugador?.IniciarMoverDerecha();
    }

    public void BotonDerechaUp()
    {
        jugador?.DetenerMoverDerecha();
    }
}
