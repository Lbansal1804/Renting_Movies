using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentingMovies.Models
{
    public class MembershipType
    {
        public byte Id { get; set; } //In Entity Framwework every entity (Membershiptpe) MUST have key which gets mapped to Primary Key to corressponding table. 
                                    //By convention, it is called either 'Id' or Name of the type+ Id.

        public short SignUpFree { get; set; }

        [Required]
        public string name { get; set; }
        public byte DurationInMonths { get; set; }
        public byte DiscountRate { get; set; }

    }
}