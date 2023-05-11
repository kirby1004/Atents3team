using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolObjectData
{
    public GameObject prefab;

    [Tooltip("poolCount�� �ּ� 1�� �̻� ����")]
    public int poolCount = 10;

    [Header("�ش� ������Ʈ ����")]
    public string explan;
}

/// <summary> Object Pooling Manager </summary>
public class ObjectPoolingManager : MonoBehaviour
{
    public static ObjectPoolingManager instance;

    [SerializeField] private bool dontDestroy = true;
    [SerializeField] private List<PoolObjectData> poolList = new List<PoolObjectData>();

    private Dictionary<string, Transform> poolParentDic = new Dictionary<string, Transform>();                  // ���� Ǯ������ �θ�
    private Dictionary<string, PoolObjectData> instantiateObject = new Dictionary<string, PoolObjectData>();    // Ǯ������ ������ ������Ʈ

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            if (dontDestroy == true)
            {
                DontDestroyOnLoad(this.gameObject);
            }
        }
        else
        {
            Destroy(this.gameObject);   // ������Ʈ Ǯ�� �ߺ� ����
        }

        MakeDir("Using");
        Pool(poolList);   // �ʱ� Ǯ���� ���ִ� ������Ʈ Ǯ��
    }

    #region Instantiate
    public void Pool(List<PoolObjectData> _poolObjectList)
    {
        GameObject prefab;

        string prefabName;
        int count;

        PoolObjectData data;
        for (int i = 0; i < _poolObjectList.Count; i++)
        {
            data = _poolObjectList[i];

            prefab = data.prefab;
            prefabName = prefab.name;
            count = data.poolCount;

            if (prefab == null)
            {
                Debug.LogError($" <color=red> ObjectPooler Instantiate Error</color> : <color=yellow>Index</color> <color=orange>{i}</color> <color=yellow>Is prefab missing</color>");
                continue;
            }

            if (poolParentDic.ContainsKey(prefabName) == false)
            {
                MakeDir(prefabName);
                InstantiatePool(prefab, poolParentDic[prefabName], count);

                instantiateObject[prefabName] = data;
            }
            else
            {
                continue;
            }
        }
    }

    /// <summary> ������Ʈ�� ��ȯ�ϰų� ������ �θ� ���� </summary>
    void MakeDir(string _name)
    {
        GameObject newDir = new GameObject(_name);
        newDir.transform.SetParent(this.transform);
        poolParentDic[_name] = newDir.transform;
    }

    /// <summary> ������Ʈ�� ���� ��ŭ �����ϰ� ������ ������ ������Ʈ�� ��ȯ�մϴ� </summary>
    GameObject InstantiatePool(GameObject _prefab, Transform _parent, int _count)
    {
        if (_count <= 0)
        {
            Debug.LogError($" <color=red> ObjectPooler Instantiate Error </color> <color=Orange>{_prefab.name}</color> <color=yellow> is pooling count 0</color>");
            return null;
        }

        GameObject inst = null;
        for (int i = 0; i < _count; i++)
        {
            inst = Instantiate(_prefab, Vector3.zero, Quaternion.identity, _parent);
            inst.name = _prefab.name;
            inst.SetActive(false);
        }
        return inst;
    }

    #endregion

    #region  Get

    /// <summary> ������Ʈ �ҷ����� </summary>
    public GameObject GetObject(string _id, Transform _parent = null, bool _enable = true)
    {
        if (poolParentDic.ContainsKey(_id) == false)
        {
            Debug.LogErrorFormat(" <color=red> ObjectPooler Get Error </color> : <color=yellow>not found object name </color> <color=orange> {0} </color>", _id);
            return null;
        }

        if (poolParentDic[_id].childCount > 0)
        {
            Transform pool = poolParentDic[_id].GetChild(0);
            if (pool.gameObject.activeSelf == false)
            {
                if (_parent == null)
                {
                    pool.SetParent(poolParentDic["Using"]);
                }
                else
                {
                    pool.SetParent(_parent);
                }

                pool.gameObject.SetActive(_enable);
                return pool.gameObject;
            }
        }

        // ������ ��� �߰� ���� �� �ϳ��� ��ȯ�� �ݴϴ�.
        return InstantiatePool(instantiateObject[_id].prefab, poolParentDic[_id], instantiateObject[_id].poolCount);
    }

    /// <summary> ������Ʈ�� �����ǰ� �����̼��� �����ϰ� ��ȯ�մϴ�. </summary>
    public GameObject GetObject(string _id, Vector3 _position, Quaternion _rotation, Transform _parent = null, bool _enable = true)
    {
        GameObject rtnObject = GetObject(_id, _parent, false);
        rtnObject.transform.SetPositionAndRotation(_position, _rotation);
        rtnObject.SetActive(_enable);
        return rtnObject;
    }

    /// <summary> ������Ʈ�� �����ǰ� �����̼��� �����ϰ� Ư�� Ÿ���� ���۳�Ʈ�� ��ȯ�մϴ� </summary>
    public T GetObject<T>(string _id, Vector3 _position, Quaternion _rotation, Transform _parent = null, bool _enable = true)
    {
        return GetObject(_id, _position, _rotation, _parent, _enable).GetComponent<T>();
    }

    /// <summary> ������Ʈ�� �����ǰ� �����̼��� �����ϰ� _rtnTime�� �ڵ� ���ϵ˴ϴ�. </summary>
    public GameObject GetObject(string _id, Vector3 _position, Quaternion _rotation, float _rtnTime, Transform _parent = null, bool _enable = true)
    {
        GameObject rtnObject = GetObject(_id, _position, _rotation, _parent, _enable);
        StartCoroutine(WaitReturnObject(rtnObject, _rtnTime));
        return rtnObject;
    }

    /// <summary> ������Ʈ�� �����ǰ� �����̼��� �����ϰ� _rtnTime�� �ڵ� ��ȯ �� ���۳�Ʈ ��ȯ </summary>
    public T GetObject<T>(string _id, Vector3 _position, Quaternion _rotation, float _rtnTime, Transform _parent = null, bool _enable = true)
    {
        return GetObject(_id, _position, _rotation, _parent, _enable).GetComponent<T>();
    }

    /// <summary> ������Ʈ�� �����ǰ� �����̼� �� ������ �����ϰ� ��ȯ�մϴ�. </summary>
    public GameObject GetObject(string _id, Vector3 _position, Quaternion _rotation, Vector3 _scale, Transform _parent = null, bool _enable = true)
    {
        GameObject rtnObject = GetObject(_id, _parent, false);
        rtnObject.transform.SetPositionAndRotation(_position, _rotation);
        rtnObject.transform.localScale = _scale;
        rtnObject.SetActive(_enable);
        return rtnObject;
    }

    /// <summary> ������Ʈ�� �����ǰ� �����̼� �� ������ ���� �� ���۳�Ʈ ��ȯ </summary>
    public T GetObject<T>(string _id, Vector3 _position, Quaternion _rotation, Vector3 _scale, Transform _parent = null, bool _enable = true)
    {
        return GetObject(_id, _position, _rotation, _scale, _parent, _enable).GetComponent<T>();
    }

    /// <summary> ������Ʈ�� �����ǰ� �����̼� �� ������ ���� �� �����ð� �� �ڵ� ���� </summary>
    public GameObject GetObject(string _id, Vector3 _position, Quaternion _rotation, Vector3 _scale, float _rtnTime, Transform _parent = null, bool _enable = true)
    {
        GameObject rtnObject = GetObject(_id, _position, _rotation, _scale, _parent, _enable);
        StartCoroutine(WaitReturnObject(rtnObject, _rtnTime));
        return rtnObject;
    }

    /// <summary> ������Ʈ�� �����ǰ� �����̼� �� ������ ���� �� �����ð� �� �ڵ� ���� �� Ư�� ���۳ʴ� ��ȯ </summary>
    public T GetObject<T>(string _id, Vector3 _position, Quaternion _rotation, Vector3 _scale, float _rtnTime, Transform _parent = null, bool _enable = true)
    {
        return GetObject(_id, _position, _rotation, _scale, _rtnTime, _parent, _enable).GetComponent<T>();
    }

    /// <summary> ������Ʈ�� �����ǰ� �����̼� �� ������ ���� �� ������Ʈ�� ��Ȱ��ȭ �� ��� �ڵ� ���� </summary>
    public GameObject GetObjectToAutoReturn(string _id, Vector3 _position, Quaternion _rotation, Vector3 _scale, Transform _parent = null, bool _enable = true)
    {
        GameObject rtnObject = GetObject(_id, _position, _rotation, _scale, _parent, _enable);
        StartCoroutine(AutoReturn(rtnObject));
        return rtnObject;
    }

    /// <summary> ������Ʈ�� �����ǰ� �����̼� �� ������ ���� �� ������Ʈ�� ��Ȱ��ȭ �� ��� �ڵ� ���� ���� �� ���۳�Ʈ ��ȯ </summary>
    public T GetObjectToAutoReturn<T>(string _id, Vector3 _position, Quaternion _rotation, Vector3 _scale, Transform _parent = null, bool _enable = true)
    {
        return GetObjectToAutoReturn(_id, _position, _rotation, _scale, _parent, _enable).GetComponent<T>();
    }

    /// <summary> ���� �ð��� ������Ʈ ��ȯ </summary>
    public GameObject GetObject(string _id, float _rtnTime, Transform _parent = null, bool _enable = true)
    {
        GameObject rtnObject = GetObject(_id, _parent, _enable);
        StartCoroutine(WaitReturnObject(rtnObject, _rtnTime));
        return rtnObject;
    }

    /// <summary> ���۳�Ʈ ��ȯ </summary>
    public T GetObject<T>(string _id, float _rtnTime, Transform _parent = null, bool _enable = true)
    {
        return GetObject(_id, _rtnTime, _parent, _enable).GetComponent<T>();
    }

    /// <summary> ������Ʈ �����ǰ�, �����̼� �� �θ� ���� �� �ڵ� ���� ���� </summary>
    public GameObject GetObjectToAutoReturn(string _id, Vector3 _position, Quaternion _rotation, Transform _parent = null, bool _enable = true)
    {
        GameObject rtnObject = GetObject(_id, _position, _rotation, _parent, _enable);
        StartCoroutine(AutoReturn(rtnObject));
        return rtnObject;
    }

    /// <summary> ������Ʈ �����ǰ�, �����̼� �� �θ� ���� �� �ڵ� ���� ���� �� Ư�� ���۳�Ʈ ��ȯ </summary>

    public T GetObjectToAutoReturn<T>(string _id, Vector3 _position, Quaternion _rotation, Transform _parent = null, bool _enable = true)
    {
        return GetObjectToAutoReturn(_id, _position, _rotation, _parent, _enable).GetComponent<T>();
    }

    /// <summary> �θ� ���� �� ������Ʈ ��Ȱ��ȭ�� �ڵ� ���� </summary>
    public GameObject GetObjectToAutoReturn(string _id, Transform _parent = null, bool _enable = true)
    {
        GameObject rtnObject = GetObject(_id, _parent, _enable);
        StartCoroutine(AutoReturn(rtnObject));
        return rtnObject;
    }

    /// <summary> Ư�� ���۳�Ʈ ��ȯ </summary>
    public T GetObjectToAutoReturn<T>(string _id, Transform _parent = null, bool _enable = true)
    {
        return GetObjectToAutoReturn(_id, _parent, _enable).GetComponent<T>();
    }

    #endregion

    #region Return

    /// <summary> ������Ʈ ���� </summary>
    public void ReturnObject(GameObject _object)
    {
        if (_object == null)
        {
            Debug.LogErrorFormat(" <color=red> ObjectPooler Return Error </color> : <color=yellow> return object is null</color>");
            return;
        }

        if (poolParentDic.ContainsKey(_object.name) == false)
        {
            Debug.LogWarningFormat(" <color=red> ObjectPooler Return Error </color> : <color=yellow> not found object name is</color> <color=orange> {0} </color>", _object.name);
            return;
        }

        _object.transform.SetParent(poolParentDic[_object.name]);
        _object.SetActive(false);
    }

    private IEnumerator WaitReturnObject(GameObject _object, float _waitTIme)
    {
        yield return new WaitForSeconds(_waitTIme);
        ReturnObject(_object);
    }

    private IEnumerator AutoReturn(GameObject _object)
    {
        while (_object.activeSelf == true)
        {
            yield return null;
        }
        ReturnObject(_object);
    }
    #endregion
}