using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client.Data
{
    public class SpawnPointsData : MonoBehaviour
    {
        public List<Transform> chefPoints = new List<Transform>();
        public List<Transform> loaderPoints = new List<Transform>();
        public List<Transform> orderPoints = new List<Transform>();
        public List<Transform> customerPoints = new List<Transform>();
    }
}