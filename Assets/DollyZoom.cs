using UnityEngine;

public class DollyZoom : MonoBehaviour
{
    public Transform target;  // L'objet � garder � taille constante
    public float dollySpeed = 5f; // Vitesse du mouvement de la cam�ra
    public float fovMin = 10f;
    public float fovMax = 60f;

    private Camera cam;
    private float initialDistance;

    void Start()
    {
        cam = GetComponent<Camera>();

        if (target == null)
        {
            Debug.LogError("Aucun target assign� au Dolly Zoom.");
            return;
        }

        // Distance initiale entre la cam�ra et la cible
        initialDistance = Vector3.Distance(transform.position, target.position);
    }

    void Update()
    {
        if (target == null) return;

        // D�placer la cam�ra en avant/arri�re avec la molette ou touches fl�ch�es
        float scroll = Input.GetAxis("Mouse ScrollWheel") * dollySpeed;
        float move = Input.GetAxis("Vertical") * dollySpeed * Time.deltaTime;

        transform.position += transform.forward * (scroll + move);

        // Nouvelle distance entre la cam�ra et la cible
        float currentDistance = Vector3.Distance(transform.position, target.position);

        // Ajuster le champ de vision de la cam�ra pour compenser
        cam.fieldOfView = Mathf.Clamp((initialDistance / currentDistance) * 30f, fovMin, fovMax);
    }
}
