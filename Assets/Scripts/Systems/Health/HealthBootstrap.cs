using UnityEngine;

public class HealthBootstrap : MonoBehaviour
{
        [SerializeField] private EntityNames m_EntityName;
    public Health Model;
    public HealthView View;
    private HealthPresenter m_Presenter;

    void Start()
    {
        m_Presenter = new HealthPresenter(Model, View);
    }

    void OnDestroy()
    {
        m_Presenter?.Dispose();
    }
}