using System.Collections;
using UnityEngine;

public class TargetsController : MonoBehaviour
{
    [SerializeField] float allPoints;
    ShootTarget _shootTarget;
    public void ChangePoints(float points)
    {
        allPoints += points;
    }


    public void StartGame()
    {
        Debug.Log("start game");
        StopAllCoroutines();
        for (int i = 0; i < transform.childCount-1; i++)
        {
            _shootTarget = transform.GetChild(i).GetComponentInChildren<ShootTarget>();
            if (_shootTarget.isActive == true)
            {
                _shootTarget.DisableTarget();
            }
        }
        StartCoroutine(ActiveObjects());
    }

    IEnumerator ActiveObjects()
    {
        yield return new WaitForSeconds(1f);
        
       ChooseTarget();
    }

    void ChooseTarget()
    {
            int i = Random.Range(0, transform.childCount);    
            _shootTarget = transform.GetChild(i).GetComponentInChildren<ShootTarget>();
            if (_shootTarget.isActive == false)
            {
                _shootTarget.EnableTarget();
                StartCoroutine(ActiveObjects());
            }
            else
            {
                ChooseTarget();
            }
    }
}
