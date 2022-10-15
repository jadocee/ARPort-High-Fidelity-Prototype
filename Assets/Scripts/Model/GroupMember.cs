using System;

namespace Model
{
    [Serializable]
    public class GroupMember
    {
        public GroupMember(string firstName, string lastName, string locationName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.locationName = locationName;
        }

        public string firstName { get; set; }
        public string lastName { get; set; }
        public string locationName { get; set; }
    }
}