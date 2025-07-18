using UnityEngine;

public class MostrarOpcionesGuardado : MonoBehaviour
{
    [SerializeField] private GameObject panelOpciones;

    public void AlternarPanel()
    {
        if (panelOpciones != null)
        {
            panelOpciones.SetActive(!panelOpciones.activeSelf);
        }
    }
}
