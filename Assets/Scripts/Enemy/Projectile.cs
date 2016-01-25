using UnityEngine;
using System.Collections;

/// <summary>
/// The projectile the tower shoots towards the enemy.
/// </summary>
public class Projectile:MonoBehaviour {

    #region Variables

    public Transform target; //Target of the Projectile.
    public AudioClip hitSound; // Sound of the Projectile hitting the enemy.
    public float damage; // Amount of damage the Projectile gives to Enemy.
    public Enemy targetScript; // Target of Projectile.

    #endregion


    #region Unity Functions

    void FixedUpdate () {

        if (target != null) {

            transform.LookAt(target.transform.position);
            transform.Translate(Vector3.forward);
            GetComponent<Renderer>().enabled = true;

        } else {

            GetComponent<Renderer>().enabled = false;
            Destroy(gameObject);

        }

    }

    void OnTriggerEnter (Collider coll) {

        Instantiate(Resources.Load("Particles/Arrowhit"), transform.position, transform.rotation);

    }

    void OnTriggerStay (Collider coll) {

        targetScript.Damage(damage);
        Destroy(gameObject);

    }

    void OnDestroy () {

        GetComponent<AudioSource>().PlayOneShot(hitSound);

    }

    #endregion

}
