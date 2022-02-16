using UnityEngine;

public class AimTargetManager : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Vector3 mouseWorldPosition;
    private RaycastHit raycastHit;

    [SerializeField] private LayerMask aimColliderMask;
    [SerializeField] private Transform debugTransform;

    public Transform TargetTransform { get => raycastHit.transform; }
    public Vector3 MouseWorldPosition { get => raycastHit.point; }
    public RaycastHit RaycastHit { get => raycastHit;}

    public bool HasTarget { get => targetTransform != null; }


    private void Update()
    {
        Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        if (Camera.main != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);
            if (Physics.Raycast(ray, out raycastHit, 999f, aimColliderMask))
            {
                targetTransform = TargetTransform;
                mouseWorldPosition = MouseWorldPosition;
                if (debugTransform != null) debugTransform.position = raycastHit.point;
            }
        }
    }
}
