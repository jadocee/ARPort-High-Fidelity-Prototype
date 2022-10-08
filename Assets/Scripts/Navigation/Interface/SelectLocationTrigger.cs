using System;
using UnityEngine;

namespace Navigation.Interface
{
    public class SelectLocationTrigger : MonoBehaviour
    {
        private Guid landmarkId;

        public void SetLandmarkId(Guid landmarkId)
        {
            this.landmarkId = landmarkId;
        }


        public void OnSelect()
        {
            EventsManager.GetInstance().OnLocationSelect(landmarkId);
        }
    }
}