using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CamaraSeguimiento : MonoBehaviour
{
    public static CamaraSeguimiento Instance { get; private set; }

    public Transform objetivo;
    public Vector3 offset = new Vector3(0f, 1.5f, -10f);
    public float suavizado = 5f;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        Camera cam = GetComponent<Camera>();
        if (cam != null)
            cam.tag = "MainCamera"; // asegurar que siga siendo Camera.main

        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(AsignarObjetivoCuandoListo());
    }

    private IEnumerator AsignarObjetivoCuandoListo()
    {
        while (Jugador.Instance == null)
            yield return null;

        AsignarObjetivo(Jugador.Instance.transform);
    }

    private void LateUpdate()
    {
        if (objetivo == null) return;

        Vector3 destino = objetivo.position + offset;
        transform.position = Vector3.Lerp(transform.position, destino, suavizado * Time.deltaTime);
    }

    public void AsignarObjetivo(Transform nuevoObjetivo)
    {
        objetivo = nuevoObjetivo;
    }
}
