using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Application.Common;

namespace CustomerInvitationApp.Model
{
    public sealed class Customer : IComparable<Customer>
    {
        [Header]
        [JsonPropertyName("user_id")]
        public int Id { get; set; }

        [Header]
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("latitude")]
        public string Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public string Longitude { get; set; }

        public int CompareTo(Customer other)
        {
            return this.Id.CompareTo(other.Id);
        }
    }
}