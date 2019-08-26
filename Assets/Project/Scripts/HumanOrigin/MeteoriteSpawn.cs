using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class MeteoriteSpawn : MonoBehaviour
{
    [SerializeField] private GameObject Meteorite;
    [SerializeField] private float Speed;

    [SerializeField] private UnityEvent OnFinish;

   private Coroutine SPawn;

   [SerializeField] private uint MaxNumber=20;

   private uint HasNumber;
 [HideInInspector] public List<Meteorite> Meteorites=new List<Meteorite>();
   // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartSpawn()
    {
        SPawn = StartCoroutine(UpdateSpawn());
    }

    public void StopSpawn()
    {
        if(SPawn!=null)
            StopCoroutine(SPawn);
    }

    IEnumerator UpdateSpawn()
    {
        while (true)
        {
            if(MaxNumber>=HasNumber)
                yield return new  WaitForFixedUpdate();
            
            yield return new  WaitForSecondsRealtime(Random.Range(1f,5f));

            for (int i = 0; i < Random.Range(5, 6); i++)
            {
                var  _Meteorite= Instantiate(Meteorite, new Vector3(transform.position.x + Random.Range(-5f, 5f),
                    transform.position.y + Random.Range(-5f, 5f), transform.position.z), Quaternion.identity);
                _Meteorite.GetComponent<Meteorite>().InitMeteor(this,Speed,_Meteorite.transform.position+Vector3.left*25);
                HasNumber++;
                Meteorites.Add(_Meteorite.GetComponent<Meteorite>());
                yield return new  WaitForFixedUpdate();
            }
        }
    }

    public void CheckIsHit(Vector3 transformPosition)
    {
        OnFinish?.Invoke();
        HasNumber--;
    }
    public void DestoryAllMeteorite()
    {
        foreach (var VARIABLE in Meteorites)
        {
            VARIABLE.DestoryMeteorite();
        }

        StopSpawn();
    }
}
