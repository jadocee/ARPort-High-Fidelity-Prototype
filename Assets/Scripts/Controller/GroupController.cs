using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using Helpers;
using Interface.GroupTracking;
using Microsoft.MixedReality.Toolkit.SpatialManipulation;
using Model;
using Navigation;
using UnityEngine;
using Random = System.Random;

namespace Controller
{
    public class GroupController : MonoBehaviour
    {
        private static readonly Random random = new Random();

        // Set of demo group member data
        private static readonly List<GroupMember> members = new List<GroupMember>
        {
            new GroupMember("Jordean", "Mikell", "McDonald's"),
            new GroupMember("A", " ", "McDonald's"),
            new GroupMember("B", " ", "McDonald's"),
            new GroupMember("C", " ", "McDonald's"),
            new GroupMember("D", " ", "McDonald's"),
            new GroupMember("E", " ", "McDonald's"),
            new GroupMember("F", " ", "McDonald's"),
            new GroupMember("G", " ", "McDonald's"),
            // new GroupMember("Alexandre", "Swill", "Checkpoint 2"),
            // new GroupMember("Aydindi", "Dhaman", "Muffin Munch"),
            // new GroupMember("Imensyd", "Alocel", "Cash Converters"),
            // new GroupMember("Jimmy", "McHill", "Travel News"),
            // new GroupMember("Happy", "Gilless", "Gate A"),
            // new GroupMember("Jesse", "Binkman", "2F Toilets"),
            // new GroupMember("Ivanoff", "Pert", "King Coffee")
        };

        /*private static readonly List<Vector3> locationPositions = new List<Vector3>
        {
            new Vector3(1, 1, 2),
            new Vector3(4, 0, 5),
            new Vector3(-1, -1, 2),
            new Vector3(0, 0, 2)
        };*/
        [SerializeField] private GameObject memberPrefab;
        [SerializeField] private Canvas menuContent;
        [SerializeField] private Transform membersContainer;
        [SerializeField] private DistanceCalculator distanceCalculator;
        [SerializeField] private LandmarkManager landmarkManager;


        public void Awake()
        {
            if (!memberPrefab) Debug.Log("Missing member prefab");
            if (!distanceCalculator) Debug.Log("Missing distance calculator");

            // Hide menu content
            menuContent.gameObject.SetActive(false);

            /*var currentGroup = GetRandomGroup();
            for (var i = 0; i < currentGroup.Count; i++)
            {
                // Should only iterate 4 times
                var current = currentGroup[i];
                if (current == null)
                {
                    Debug.Log("Null group member");
                    continue;
                }

                var memberButton = Instantiate(memberPrefab, membersContainer, false);
                var memberButtonScript = memberButton.GetComponent<GroupMemberButton>();
                if (!memberButtonScript)
                {
                    Debug.Log("Couldn't find member button script");
                    continue;
                }

                var landmark = landmarkManager.GetLandmarkByName(current.LocationName);
                if (landmark != null)
                {
                    memberButtonScript.SetMemberLocation(landmark);
                    memberButtonScript.SetMemberName($"{current.FirstName} {current.LastName}");
                    memberButtonScript.SetDistanceCalculator(distanceCalculator);
                }
            }*/


            StartCoroutine(WaitForEventSystem());
        }

        private IEnumerator<WaitUntil> WaitForEventSystem()
        {
            yield return new WaitUntil((() => EventSystem.IsInitialized));
            EventSystem.Instance.NavigationEvent += args =>
            {
                if (args.NavigationState.State.Equals(NavigationEventArgs.EventState.Start))
                {
                    menuContent.gameObject.SetActive(false);
                    gameObject.GetComponent<RadialView>().enabled = false;
                }
            };
        }


        public List<GroupMember> GetCurrentGroup()
        {
            var randomGroup = GetRandomGroup();
            return GetRandomGroup();
        }

        private List<GroupMember> GetRandomGroup()
        {
            var groupMembers = new List<GroupMember>();
            var i = random.Next(0, 2);
            if (i == 0)
            {
                groupMembers.Add(members[0]);
                groupMembers.Add(members[1]);
                groupMembers.Add(members[2]);
                groupMembers.Add(members[3]);
            }
            else if (i == 1)
            {
                groupMembers.Add(members[4]);
                groupMembers.Add(members[5]);
                groupMembers.Add(members[6]);
                groupMembers.Add(members[7]);
            }

            return groupMembers;
        }

        public void ShowMenu()
        {
            if (!menuContent) return;
            menuContent.gameObject.SetActive(true);
            gameObject.GetComponent<RadialView>().enabled = true;
        }

        [Obsolete]
        private void TrackMember(GroupMemberButton groupMemberButton)
        {
            // Intended to parse a Transform, but method only takes a GameObject
            // tracker.changeTarget(groupMemberButton.GetLocationTransform().gameObject);
            //statusText.text = $"Tracking {groupMemberButton.GetMemberName()}";
            // Hide menu
            menuContent.gameObject.SetActive(false);
        }

        // TODO: End tracking
        private void EndTracking()
        {
        }
    }
}