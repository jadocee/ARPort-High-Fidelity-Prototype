using System;
using System.Collections.Generic;
using Controller;
using Helpers;
using Model;
using TMPro;
using UnityEngine;

namespace Interface.GroupTracking
{
    public class GroupMemberList : MonoBehaviour
    {
        [SerializeField] private GroupController groupController;
        [SerializeField] private GameObject memberItemPrefab;
        [SerializeField] private DistanceCalculator distanceCalculator;
        [SerializeField] private LandmarkController landmarkController;
        private readonly List<GroupMember> _groupMembers;

        public GroupMemberList()
        {
            _groupMembers = new List<GroupMember>();
        }

        private void OnEnable()
        {
            if (!groupController)
            {
                Debug.Log("Missing GroupController");
                return;
            }

            if (_groupMembers.Count == 0)
            {
                _groupMembers.Clear();
                var incomingList = groupController.GetCurrentGroup();
                if (incomingList != null)
                {
                    _groupMembers.AddRange(incomingList);
                }
            }
        }

        private void OnDisable()
        {
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
        }

        private void DisplayMembers()
        {
            if (_groupMembers.Count == 0) return;
            for (var i = 0; i < _groupMembers.Count; i++)
            {
                // Should only iterate 4 times
                var current = _groupMembers[i];
                if (current == null)
                {
                    Debug.Log("Null group member");
                    continue;
                }

                var landmark = landmarkController.GetLandmarkByName(current.LocationName);
                if (landmark == null) continue;

                var memberButton = Instantiate(memberItemPrefab, transform, false);
                memberButton.SetActive(false);
                var memberButtonScript = memberButton.GetComponent<GroupMemberButton>();
                if (!memberButtonScript)
                {
                    Debug.Log("Couldn't find member button script");
                    continue;
                }

                memberButtonScript.SetMemberLocation(landmark);
                memberButtonScript.SetMemberName($"{current.FirstName} {current.LastName}");
                memberButtonScript.SetDistanceCalculator(distanceCalculator);
                memberButtonScript.gameObject.SetActive(true);
            }
        }

        private void Start()
        {
            DisplayMembers();
        }
    }
}