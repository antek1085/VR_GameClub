using System;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
   Rigidbody rigidbody;
   [SerializeField] float lifeTime;
   void OnTriggerEnter(Collider other)
   {
      IDamageable damageable = other.GetComponent<IDamageable>();
      if (damageable != null)
      {
         damageable.Damageable();
      }
      else
      {
        // Destroy(gameObject);
      }
   }

   void Update()
   {
      lifeTime -= Time.deltaTime;
      if (lifeTime < 0)
      {
         Destroy(gameObject);
      }
   }


}
