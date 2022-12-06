using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaquetteScript : MonoBehaviour
{
    public float vitesse = 15f;
    public string axe;

    void Update()
    {
        Vector3 raquettePos = transform.position;
        // je récupère la position de la raquette et je la stocke dans une boite
        raquettePos.y += Input.GetAxisRaw(axe) * vitesse * Time.deltaTime;
        raquettePos.y = Mathf.Clamp(raquettePos.y, -18f, 18f); // on empêche la raquette de sortir de l'écran
        transform.position = raquettePos; // on applique la nouvelle position

    }
} //FIN DU SCRIPT
