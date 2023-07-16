using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;
using UnityEngine.Serialization;

namespace BurningFrost
{
    public class GhostWeaponHandler : MonoBehaviour
    {
        public Transform firePoint;
        [MMReadOnly, SerializeField] private GhostWeapon _currentWeapon;

        private GhostCatcher _catcher;

        public void Init(GhostCatcher catcher)
        {
            _catcher = catcher;
        }

        public void SetWeapon(GhostWeapon weapon)
        {
            _currentWeapon = weapon;
            enabled = weapon;
        }

        // Update is called once per frame
        void Update()
        {
            if (!_currentWeapon) return;

            if (_currentWeapon.CurrentAmmo <= 0)
            {
                _catcher.EjectGhost();
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                _currentWeapon.PressedWeapon();
            }

            if (Input.GetMouseButton(0))
            {
                _currentWeapon.UseWeapon();
            }

            if (Input.GetMouseButtonUp(0))
            {
                _currentWeapon.ReleasedWeapon();
            }
        }
    }
}
