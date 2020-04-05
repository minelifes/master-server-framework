﻿using Barebones.Networking;
using System.Collections.Generic;
using System.Text;

namespace Barebones.MasterServer
{
    /// <summary>
    /// List of options, which are sent to master server during registration
    /// </summary>
    public class RoomOptions : SerializablePacket
    {
        /// <summary>
        /// Name of the room
        /// </summary>
        public string Name { get; set; } = "Unnamed";

        /// <summary>
        /// IP of the machine on which the room was created
        /// (Only used in the <see cref="RoomController.DefaultAccessProvider"/>)
        /// </summary>
        public string RoomIp { get; set; } = string.Empty;

        /// <summary>
        /// Port, required to access the room 
        /// (Only used in the <see cref="RoomController.DefaultAccessProvider"/>)
        /// </summary>
        public int RoomPort { get; set; } = -1;

        /// <summary>
        /// If true, room will appear in public listings
        /// </summary>
        public bool IsPublic { get; set; } = true;

        /// <summary>
        /// If 0 - player number is not limited
        /// </summary>
        public int MaxConnections { get; set; } = 0;

        /// <summary>
        /// Room password
        /// </summary>
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Number of seconds, after which unconfirmed (pending) accesses will removed
        /// to allow new players. Make sure it's long enought to allow player to load gameplay scene
        /// </summary>
        public float AccessTimeoutPeriod { get; set; } = 10;

        /// <summary>
        /// If set to false, users will no longer be able to request access directly.
        /// This is useful when you want players to get accesses through other means, for example
        /// through Lobby module,
        /// </summary>
        public bool AllowUsersRequestAccess { get; set; } = true;

        /// <summary>
        /// Region, to which the spawned room belongs
        /// </summary>
        public string Region { get; set; } = "International";

        /// <summary>
        /// Extra properties that you might want to send to master server
        /// </summary>
        public Dictionary<string, string> Properties { get; set; }

        public override void ToBinaryWriter(EndianBinaryWriter writer)
        {
            writer.Write(Name);
            writer.Write(RoomIp);
            writer.Write(RoomPort);
            writer.Write(IsPublic);
            writer.Write(MaxConnections);
            writer.Write(Password);
            writer.Write(AccessTimeoutPeriod);
            writer.Write(AllowUsersRequestAccess);
            writer.Write(Region);
            writer.Write(Properties);
        }

        public override void FromBinaryReader(EndianBinaryReader reader)
        {
            Name = reader.ReadString();
            RoomIp = reader.ReadString();
            RoomPort = reader.ReadInt32();
            IsPublic = reader.ReadBoolean();
            MaxConnections = reader.ReadInt32();
            Password = reader.ReadString();
            AccessTimeoutPeriod = reader.ReadSingle();
            AllowUsersRequestAccess = reader.ReadBoolean();
            Region = reader.ReadString();
            Properties = reader.ReadDictionary();
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            s.Append($"Name: {Name}, ");
            s.Append($"RoomIp: {RoomIp}, ");
            s.Append($"RoomPort: {RoomPort}, ");
            s.Append($"MaxConnections: {MaxConnections}, ");
            s.Append($"Use Password: {!string.IsNullOrEmpty(Password)}, ");
            s.Append($"AccessTimeoutPeriod: {AccessTimeoutPeriod} sec., ");
            s.Append($"AllowUsersRequestAccess: {AllowUsersRequestAccess}, ");
            s.Append($"Region: {Region}, ");

            if(Properties != null && Properties.Count > 0)
            {
                foreach (var kvp in Properties)
                {
                    s.Append($"{kvp.Key}: {kvp.Value}, ");
                }
            }

            return s.ToString();
        }
    }
}