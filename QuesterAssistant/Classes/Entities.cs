using System.Collections.Generic;
using MyNW.Classes;
using MyNW.Internals;

namespace QuesterAssistant.Classes
{
    internal static class Entities
    {
        public static List<Entity> GetContactEntities()
        {
            List<Entity> list = new List<Entity>();
            List<ContactInfo> nearbyContacts = EntityManager.LocalPlayer.Player.InteractInfo.NearbyContacts;
            nearbyContacts.AddRange(EntityManager.LocalPlayer.Player.InteractInfo.NearbyInteractCritterEnts);
            foreach (ContactInfo contactInfo in nearbyContacts)
            {
                if (contactInfo.Entity.IsValid)
                {
                    list.Add(contactInfo.Entity);
                }
            }
            return list;
        }
    }
}
