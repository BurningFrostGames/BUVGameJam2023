using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MoreMountains.Feedbacks;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace BurningFrost
{
    [Serializable]

    public struct GhostParameter
    {
        public GhostWeapon Ghost;
    }

    public class GhostCatcher : MonoBehaviour
    {
        [SerializeField] private Collider2D catchAreaCollider;
        [SerializeField] private ContactFilter2D filter;
        [SerializeField] private float catchDistance = 0.1f;
        [SerializeField] private float pullSpeed = 10f;

        [MMReadOnly, SerializeField] private GhostWeapon _currentSelectedGhost;

        [MMReadOnly, SerializeField] private GhostWeapon _holdingGhost;

        public GhostWeaponHandler ghostWeaponHandler;

        private Vector3 catchPosition => catchAreaCollider.transform.position;

        private Vector3 _randomSpreadDirection;

        private void Start()
        {
            ghostWeaponHandler.enabled = _holdingGhost;
            ghostWeaponHandler.Init(this);
        }

        public void InjectGhost(GhostWeapon ghost)
        {
            _holdingGhost = ghost;
            _currentSelectedGhost.gameObject.SetActive(false);
            _currentSelectedGhost = null;

            ghostWeaponHandler.SetWeapon(_holdingGhost);
            _holdingGhost.OnInjected(ghostWeaponHandler);

            MMEventManager.TriggerEvent(new GhostParameter
            {
                Ghost = _holdingGhost
            });
        }

        public void EjectGhost()
        {
            if (_holdingGhost.CurrentAmmo > 0)
            {
                var point = catchAreaCollider.transform;
                _holdingGhost.transform.position = point.position;
                _holdingGhost.gameObject.SetActive(true);
                _holdingGhost.AddForce(point.up * 10f);
                _holdingGhost.OnEjected();
            }
            else
            {
                Destroy(_holdingGhost.gameObject);
            }

            ghostWeaponHandler.SetWeapon(null);

            MMEventManager.TriggerEvent(new GhostParameter
            {
                Ghost = null
            });

            _holdingGhost = null;
        }

        private void Update()
        {
            if (Input.GetMouseButton(1))
            {
                if (_holdingGhost)
                {
                    return;
                }

                if (_currentSelectedGhost)
                {
                    //If the ghost reached players position, inject it to the weapon
                    if (Vector3.Distance(_currentSelectedGhost.transform.position, catchPosition) <=
                        catchDistance)
                    {
                        InjectGhost(_currentSelectedGhost);
                        return;
                    }

                    //Else, try to pull the ghost
                    _currentSelectedGhost.transform.position = Vector3.Lerp(
                        _currentSelectedGhost.transform.position,
                        catchPosition,
                        Time.deltaTime * pullSpeed);
                    return;
                }

                FindGhost();
            }

            if (Input.GetMouseButtonUp(1))
            {
                _currentSelectedGhost = null;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (_holdingGhost)
                    EjectGhost();
            }
        }

        private void FindGhost()
        {
            var allColliders = new List<Collider2D>();
            catchAreaCollider.OverlapCollider(filter, allColliders);
            allColliders = allColliders.Where(col => col.GetComponent<GhostWeapon>()).ToList();

            if (allColliders.Count > 0)
            {
                Collider2D col = allColliders[Random.Range(0, allColliders.Count)];
                _currentSelectedGhost = col.GetComponent<GhostWeapon>();
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(catchPosition, catchDistance);
        }
    }
}
