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
      else if (other.tag == "Bell")
      {
        Destroy(gameObject);
      }
   }
   void Awake()
   {
      rigidbody = GetComponent<Rigidbody>();
   }

   void Update()
   {
      lifeTime -= Time.deltaTime;
      if (lifeTime < 0)
      {
         Destroy(gameObject);
      }
   }
   
   void FixedUpdate()
   {
   }


}
