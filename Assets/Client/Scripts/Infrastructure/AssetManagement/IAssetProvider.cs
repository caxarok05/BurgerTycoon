using Client.Services;
using UnityEngine;

namespace Client.Infrastructure.AssetManagement
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string path, Vector3 at);
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Transform parent);
    }
}