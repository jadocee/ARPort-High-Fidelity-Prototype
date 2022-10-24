using System.Collections.Generic;
using Helpers;
using Model;
using UnityEngine;
using Random = System.Random;

namespace Controller
{
    public class GroupController : MonoBehaviour
    {
        private static readonly Random Rand = new Random();

        // Set of demo group member data
        private static readonly List<GroupMember> Members = new List<GroupMember>
        {
            new GroupMember("Jordean", "Mikell", "McDonald's"),
            new GroupMember("Alexandre", "Swill", "Gate C"),
            new GroupMember("Aydindi", "Dhaman", "Muffin Munch"),
            new GroupMember("Imensyd", "Alocel", "G9 Toilets"),
            new GroupMember("Jimmy", "McHill", "Oporto"),
            new GroupMember("Happy", "Gilless", "Gate A"),
            new GroupMember("Jesse", "Binkman", "2F Toilets"),
            new GroupMember("Jordean", "Peelsgrapes", "King Coffee")
        };

        [SerializeField] private GameObject memberPrefab;
        [SerializeField] private DistanceCalculator distanceCalculator;

        public void Awake()
        {
            if (!memberPrefab) Debug.Log("Missing member prefab");
            if (!distanceCalculator) Debug.Log("Missing distance calculator");
        }

        // Prototype; calls GetRandomGroup
        public static List<GroupMember> GetCurrentGroup()
        {
            return GetRandomGroup();
        }

        private static List<GroupMember> GetRandomGroup()
        {
            var groupMembers = new List<GroupMember>();
            var i = Rand.Next(0, 2);
            switch (i)
            {
                case 0:
                    groupMembers.Add(Members[0]);
                    groupMembers.Add(Members[1]);
                    groupMembers.Add(Members[2]);
                    groupMembers.Add(Members[3]);
                    break;
                case 1:
                    groupMembers.Add(Members[4]);
                    groupMembers.Add(Members[5]);
                    groupMembers.Add(Members[6]);
                    groupMembers.Add(Members[7]);
                    break;
            }

            return groupMembers;
        }
    }
}