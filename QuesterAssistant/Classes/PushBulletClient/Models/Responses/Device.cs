using QuesterAssistant.Classes.Common;
using System;
using System.Runtime.Serialization;

namespace QuesterAssistant.Classes.PushBulletClient.Models.Responses
{
    [Serializable]
    [DataContract]
    public class Device : OverrideHash
    {
        /// <summary>
        /// Gets or sets the iden.
        /// </summary>
        /// <value>
        /// The iden.
        /// </value>
        [DataMember(Name = "iden"), HashInclude]
        public string Iden { get; set; }

        /// <summary>
        /// Gets or sets the nickname.
        /// </summary>
        /// <value>
        /// The nickname.
        /// </value>
        [DataMember(Name = "nickname"), HashInclude]
        public string Nickname { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Device"/> is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if active; otherwise, <c>false</c>.
        /// </value>
        [DataMember(Name = "active"), HashInclude]
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Device"/> is pushable.
        /// </summary>
        /// <value>
        ///   <c>true</c> if pushable; otherwise, <c>false</c>.
        /// </value>
        [DataMember(Name = "pushable"), HashInclude]
        public bool Pushable { get; set; }

        //public bool Selected { get; set; }

        public override string ToString()
        {
            if (!string.IsNullOrWhiteSpace(Nickname))
            {
                return Nickname;
            }
            return base.ToString();
        }
    }
}