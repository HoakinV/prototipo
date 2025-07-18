using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ControlesUI))]
public class ConexionControles : MonoBehaviour
{
    private IEnumerator Start()
    {
        // Esperar hasta que el jugador persistente esté disponible
        while (Jugador.Instance == null)
        {
            yield return null;
        }

        PlayerController2D playerController = Jugador.Instance.GetComponent<PlayerController2D>();
        if (playerController == null)
        {
            Debug.LogWarning("⚠ El jugador no tiene componente PlayerController2D.");
            yield break;
        }

        ControlesUI controlesUI = GetComponent<ControlesUI>();
        if (controlesUI != null)
        {
            controlesUI.SetJugador(playerController);
            Debug.Log("✅ Controles UI conectados al jugador.");
        }
        else
        {
            Debug.LogWarning("⚠ No se encontró ControlesUI en este objeto.");
        }
    }
}
