using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalleScript : MonoBehaviour
{
    [Range(1,100)] public int scoreMax = 11;
    public int scoreJoueurGauche = 0;
    public int scoreJoueurDroit = 0;
    [Range(1f,100f)] public float vitesseMax = 5f;
    public string nomJoueurGauche = "Toto";
    public string nomJoueurDroit = "Titi";
    public bool enPause = false;
    public Rigidbody body;
    public TextMesh scoreJ1, scoreJ2;
    float directionLancerX = 1f;
    public AudioClip sonBut;
    public TrailRenderer maQueue;


    void Start() // Quand on lance le jeu
    {
       NouvellePartie(); // On lance une nouvelle partie au démarrage du jeu
    }

    void NouvellePartie() // Au lancement d'une nouvelle partie
    {
        if (Random.value>0.5f) directionLancerX = -1f;

        scoreJoueurDroit = 0; // On remet les scores à zéro
        scoreJoueurGauche = 0;
        NouvelleBalle(); // On lance une nouvelle balle
    }

    void NouvelleBalle()
    {
        CancelInvoke();
        if (scoreJoueurDroit>=scoreMax || scoreJoueurGauche>=scoreMax) Invoke("GameOver",2f);
        scoreJ1.text = nomJoueurGauche + "\n" + scoreJoueurGauche ; // on met à jour l'affichage des scores
        scoreJ2.text = nomJoueurDroit + "\n" + scoreJoueurDroit ;
        body.velocity = Vector3.zero; // on arrête la balle
        transform.position = Vector3.zero; // on remet la balle au centre
        maQueue.Clear(); // on efface la trainée derrière la balle
        Invoke("LancerBalle", 2f) ; // on attend deux secondes puis on lance la balle
    }

    void LancerBalle()
    {
        Vector3 direction = Vector3.one;      
        direction.x = directionLancerX;
        if (Random.value > 0.5f) direction.y = -1f;
        body.AddForce(direction.normalized * (vitesseMax+(scoreJoueurDroit+scoreJoueurGauche)*10f), ForceMode.VelocityChange);
    }

    void GameOver()
    {
        NouvellePartie();
    }



    void Update()
    {
        DetecterBut(); // j'appelle la fonction DetecterBut()
    }

    void DetecterBut()
    {
        if (transform.position.x > 36f) // si la balle sort de l'écran à droite
        {
            ButJ1(); // Le joueur de gauche a marqué
        }

        if (transform.position.x < -36f) // si la balle sort de l'écran à gauche
        {
            ButJ2(); // Le joueur de gauche a marqué
        }
    }
    

    void ButJ1()
    {
        scoreJoueurGauche += 1;
        directionLancerX = 1f;
        AudioSource.PlayClipAtPoint(sonBut, Vector3.right*10f); // je joue le son 10m à droite
        NouvelleBalle();
    }

    void ButJ2()
    {
        scoreJoueurDroit += 1;
        directionLancerX = -1f;
        AudioSource.PlayClipAtPoint(sonBut, Vector3.left*10f); // je joue le son 10m à gauche
        NouvelleBalle();
    }






} // FIN DU SCRIPT

