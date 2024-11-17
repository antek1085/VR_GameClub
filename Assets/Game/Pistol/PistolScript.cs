using System.Collections;
using UnityEngine;

public class PistolScript : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform shootPoint;
    
    [SerializeField] int ammunition;
    [SerializeField] float timeBetweenShoots;

    private bool canHeShoot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void TryToShoot()
    {
        if (canHeShoot == true && ammunition != 0)
        {
            StartCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        GameObject shootedBullet;
        canHeShoot = false;
        ammunition -= 1;
        shootedBullet = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        shootedBullet.GetComponent<Rigidbody>().AddForce(shootPoint.forward * 10);
        yield return new WaitForSeconds(timeBetweenShoots);
        canHeShoot = true;
    }
}
