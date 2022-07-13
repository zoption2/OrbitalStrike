using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    private WaitForFixedUpdate update = new WaitForFixedUpdate();
    private void Start()
    {
        StartCoroutine(Follow());
    }

    private IEnumerator Follow()
    {
        var templastPosition = player.position;
        yield return update;
        templastPosition.z = transform.position.z;
        transform.position = templastPosition;
        StartCoroutine(Follow());
    }
}
