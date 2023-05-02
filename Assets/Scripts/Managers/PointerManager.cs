using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointerManager : MonoBehaviour
{

    [SerializeField] PointerIcon _pointerPrefab;
    [SerializeField] Dictionary<Destroyable, PointerIcon> _dictionary = new Dictionary<Destroyable, PointerIcon>();
    [SerializeField] Transform _playerTransform;
    [SerializeField] Camera _camera;

    public static PointerManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        Hide();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
      public void Show()
    {
        gameObject.SetActive(true);
    }

    //public void InitPointerManager()
    //{
    //    gameObject.SetActive(true);
    //}

    public void DestroyPoints()
    {
        foreach (var i in _dictionary)
        {
            Destroy(i.Key.gameObject);
            Destroy(i.Value.gameObject);
        }
        _dictionary.Clear();
    }

    public void SetPlayerTransform(Transform player)
    {
        _playerTransform = player;
    }
    public void AddToList(Destroyable destroyable)
    {
        
        PointerIcon newPointer = Instantiate(_pointerPrefab, transform);
        _dictionary.Add(destroyable, newPointer);
    }

    public void RemoveFromList(Destroyable destroyable)
    {

        if (_dictionary.Count != 0)
        {

            Destroy(_dictionary[destroyable].gameObject);
            _dictionary.Remove(destroyable);

        }
    }

    void FixedUpdate()
    {

        // Left, Right, Down, Up
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_camera);
        foreach (var kvp in _dictionary)
        {

            Destroyable destroyable = kvp.Key;
            PointerIcon pointerIcon = kvp.Value;

            Vector3 toDestroyable = destroyable.transform.position - _playerTransform.position;
            Ray ray = new Ray(_playerTransform.position, toDestroyable);
            //Debug.DrawRay(_playerTransform.position, toZombie);




            float rayMinDistance = Mathf.Infinity;
            //float rayMinDistance = 0;

            int index = 0;



            for (int p = 0; p < 4; p++)
            {
                if (planes[p].Raycast(ray, out float distance))
                {
                    //Debug.DrawRay(_playerTransform.position, toDestroyable, Color.red);

                    if (distance < rayMinDistance)
                    {
                        rayMinDistance = distance;
                        index = p;
                    }

                }
            }
            //RaycastHit hit;

            //Physics.Raycast(ray, out hit);



            rayMinDistance = Mathf.Clamp(rayMinDistance, 0, toDestroyable.magnitude);
            Vector3 worldPosition = ray.GetPoint(rayMinDistance);
            Vector3 position = _camera.WorldToScreenPoint(worldPosition);

            Quaternion rotation = GetIconRotation(index);



            if (toDestroyable.magnitude > rayMinDistance)
            {
                //Debug.Log("Show!!");

                pointerIcon.Show();
            }
            else
            {
                pointerIcon.Hide();
            }

            pointerIcon.SetIconPosition(position, rotation);



        }

    }

    Quaternion GetIconRotation(int planeIndex)
    {
        if (planeIndex == 0)
        {
            return Quaternion.Euler(0f, 0f, 90f);
        }
        else if (planeIndex == 1)
        {
            return Quaternion.Euler(0f, 0f, -90f);
        }
        else if (planeIndex == 2)
        {
            return Quaternion.Euler(0f, 0f, 180);
        }
        else if (planeIndex == 3)
        {
            return Quaternion.Euler(0f, 0f, 0f);
        }
        return Quaternion.identity;
    }

}