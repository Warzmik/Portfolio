using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Crystal : MonoBehaviour, ICollectable
{
    [field: SerializeField] public GameData gameData { set; get; }
    public UnityAction onCollect;


    private void OnEnable()
    {
        GetComponent<SphereCollider>().enabled = true;
        GetComponent<MeshRenderer>().enabled = true;
    }


    private void Update()
    {
        transform.Rotate(Vector3.one * Time.deltaTime * 20f); // Rotation effect
    }


    public void Collect()
    {
        // Desactivate components
        GetComponent<SphereCollider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;

        gameData.crystals ++;
        onCollect?.Invoke();
        
        StartCoroutine(WaitForDestroy());
    }


    private IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(2f); // Wait for desactivate gameObject

        gameObject.SetActive(false);
    }
}
