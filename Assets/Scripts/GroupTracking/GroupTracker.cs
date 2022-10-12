using System;
using System.Collections.Generic;
using DefaultNamespace;
using Microsoft.MixedReality.Toolkit.SpatialManipulation;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;


namespace GroupTracking
{
    public class GroupTracker : MonoBehaviour
    {
        private static readonly Random random = new Random(1);

        // Set of demo group member data
        private static readonly List<GroupMember> members = new List<GroupMember>
        {
            new GroupMember("Jaide", "Nugget", "McDonald's"),
            new GroupMember("Alexandre", "S", "Ryt B Hind U"),
            new GroupMember("Ayyden", "B", "3F Toilets"),
            new GroupMember("Imansyd", "A", "Cash Converters"),
            // Next 4 members are duplicates of the first 4; need to be changed :)
            // TODO update data of next 4 members
            new GroupMember("Jaide", "Nugget", "McDonald's"),
            new GroupMember("Alexandre", "S", "Ryt B Hind U"),
            new GroupMember("Ayyden", "B", "3F Toilets"),
            new GroupMember("Imansyd", "A", "Cash Converters")
        };

        [SerializeField] private GameObject memberPrefab;
        [SerializeField] private Canvas menuContent;
        [SerializeField] private Tracking tracker;
        [SerializeField] private List<Transform> locations;
        [SerializeField] private Transform membersContainer;
        [SerializeField] private DistanceCalculator distanceCalculator;
        public Text statusText;

        public GroupTracker()
        {
            locations = new List<Transform>();
        }

        private List<GroupMember> GetRandomGroup()
        {
            List<GroupMember> groupMembers = new List<GroupMember>();
            int i = random.Next(0, 2);
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

        public void Awake()
        {
            if (!memberPrefab) Debug.Log("Missing member prefab");
            if (!tracker) Debug.Log("Missing tracker");
            if (!distanceCalculator) Debug.Log("Missing distance calculator");
            menuContent.gameObject.SetActive(false);
            List<GroupMember> currentGroup = GetRandomGroup();
            for (int i = 0; i < currentGroup.Count; i++)
            {
                // Should only iterate 4 times
                var current = currentGroup[i];
                if (current == null)
                {
                    Debug.Log("Null group member");
                    continue;
                }

                ;
                var memberButton = Instantiate(memberPrefab, membersContainer, false);
                var memberButtonScript = memberButton.GetComponent<GroupMemberButton>();
                if (!memberButtonScript)
                {
                    Debug.Log("Couldn't find member button script");
                    continue;
                };
                memberButtonScript.SetMemberLocation(current.locationName);
                memberButtonScript.SetMemberName($"{current.firstName} {current.lastName}");
                memberButtonScript.SetMemberDistance(0);
                memberButtonScript.SetLocationTransform(locations[i]);
                memberButtonScript.SetDistanceCalculator(this.distanceCalculator);
            }

            // Listen for event calls
            GroupMemberButton.OnMemberSelectedEvent += TrackMember;
        }

        public void ShowMenu()
        {
            if (!menuContent) return;
            menuContent.gameObject.SetActive(true);
            gameObject.GetComponent<RadialView>().enabled = true;
        }


        private void TrackMember(GroupMemberButton groupMemberButton)
        {
            // Intended to parse a Transform, but method only takes a GameObject
            tracker.changeTarget(groupMemberButton.GetLocationTransform().gameObject);
            statusText.text = $"Tracking {groupMemberButton.GetMemberName()}";
            // Hide menu
            menuContent.gameObject.SetActive(false);
        }

        private void EndTracking()
        {
        }
    }
}