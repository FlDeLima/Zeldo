using UnityEngine;

public class DollyZoom : MonoBehaviour
{
    public Transform target;  // L'objet à garder à taille constante
    public float dollySpeed = 5f; // Vitesse du mouvement de la caméra
    public float fovMin = 10f;
    public float fovMax = 60f;

    private Camera cam;
    private float initialDistance;

    void Start()
    {
        cam = GetComponent<Camera>();

        if (target == null)
        {
            Debug.LogError("Aucun target assigné au Dolly Zoom.");
            return;
        }

        // Distance initiale entre la caméra et la cible
        initialDistance = Vector3.Distance(transform.position, target.position);
    }

    void Update()
    {
        if (target == null) return;

        // Déplacer la caméra en avant/arrière avec la molette ou touches fléchées
        float scroll = Input.GetAxis("Mouse ScrollWheel") * dollySpeed;
        float move = Input.GetAxis("Vertical") * dollySpeed * Time.deltaTime;

        transform.position += transform.forward * (scroll + move);

        // Nouvelle distance entre la caméra et la cible
        float currentDistance = Vector3.Distance(transform.position, target.position);

        // Ajuster le champ de vision de la caméra pour compenser
        cam.fieldOfView = Mathf.Clamp((initialDistance / currentDistance) * 30f, fovMin, fovMax);
    }
}
