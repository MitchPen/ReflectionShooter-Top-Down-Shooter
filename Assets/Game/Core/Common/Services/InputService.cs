using UnityEngine;

namespace Game.Core.Common.Services
{
    public class InputService : MonoBehaviour
    {
        [SerializeField] private LayerMask _groundMask;
        private Camera _camera;
        private Ray _ray;
        public void SetCamera(Camera camera) => _camera = camera;
        private bool _enable;
        public void EnableKeyHandle() => _enable = true;
        public void DisableKeyHandling() => _enable = false;
        public (bool succes, Vector3 position) GetMousePosition()
        {
            _ray = _camera.ScreenPointToRay(Input.mousePosition);
            return Physics.Raycast(_ray, out var hitInfo, Mathf.Infinity, _groundMask) ? ( true, hitInfo.point) : (false, Vector3.zero);
        }
    }
}
