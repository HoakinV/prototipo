using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Objeto : MonoBehaviour
{
    [Header("Referencias UI")]
    [SerializeField] private Image imagenObjeto;
    [SerializeField] private TextMeshProUGUI textoObjeto;
    [SerializeField] private TextMeshProUGUI precioObjeto;

    private int precio;
    private PlantillaObjeto datosObjeto;

    public void CrearObjeto(PlantillaObjeto datos)
    {
        if (datos == null)
        {
            Debug.LogWarning("⚠ Datos de objeto nulos.");
            return;
        }

        datosObjeto = datos;
        precio = datos.precioObjeto;

        if (imagenObjeto != null) imagenObjeto.sprite = datos.imagenObjeto;
        if (textoObjeto != null) textoObjeto.text = datos.textoObjeto;
        if (precioObjeto != null) precioObjeto.text = precio.ToString();
    }

    public void ComprarObjeto()
    {
        if (Equipo.Instance == null)
        {
            Debug.LogWarning("⚠ No se puede comprar: 'Equipo' no asignado.");
            return;
        }

        Equipo.Instance.IncluirEquipo(precio, imagenObjeto, datosObjeto.esCurativo, datosObjeto.cantidadCura);
    }
}
