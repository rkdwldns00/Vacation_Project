using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadObject : MonoBehaviour
{
    [SerializeField] private GameObject _playerDeadEffect;
    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;
    private Rigidbody _rigidBody;
    private float _moveSpeed;

    private void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _meshRenderer = GetComponent<MeshRenderer>();
        _rigidBody = GetComponent<Rigidbody>();
    }

    public void SetDeadObjectData(Mesh mesh, Material[] materials, float moveSpeed, Vector3 scale, float yPos, Vector3 velocity)
    {
        _meshFilter.sharedMesh = mesh;
        _meshRenderer.materials = materials;
        _moveSpeed = moveSpeed;
        transform.localScale = scale;
        transform.position += new Vector3(0, yPos);
        _rigidBody.velocity = velocity;
        _rigidBody.AddForceAtPosition(velocity, transform.forward);

        StartCoroutine(StartAnimation());
    }

    private IEnumerator StartAnimation()
    {
        /*Vector3 targetPos = transform.position + new Vector3(0,0,_moveSpeed / 2);
        
        while (transform.position != targetPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, _moveSpeed / 2 * Time.deltaTime);
            transform.Rotate(transform.up * -60 * Time.deltaTime);
            yield return null;
        }*/

        yield return new WaitForSeconds(0.75f);
        _playerDeadEffect.SetActive(true);

        yield break;
    }
}
