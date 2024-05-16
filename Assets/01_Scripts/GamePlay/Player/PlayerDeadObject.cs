using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadObject : MonoBehaviour
{
    [SerializeField] private MeshFilter _mesh;
    [SerializeField] private GameObject _playerDeadEffect;
    private float _moveSpeed;

    private void Awake()
    {
        
    }

    public void SetDeadObjectData(Mesh mesh, float moveSpeed)
    {
        _mesh.sharedMesh = mesh;
        _moveSpeed = moveSpeed;

        StartCoroutine(StartAnimation());
    }

    private IEnumerator StartAnimation()
    {
        Vector3 targetPos = transform.position + new Vector3(0,0,3);
        
        while (transform.position != targetPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, _moveSpeed * Time.deltaTime);
            transform.Rotate(transform.up * 10 * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(0.75f);
        _playerDeadEffect.SetActive(true);

        Debug.Log("End");

        yield break;
    }
}
