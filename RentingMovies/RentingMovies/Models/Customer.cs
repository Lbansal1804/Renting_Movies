using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RentingMovies.Models
{
    public class Customer
    {
        public int Id { get; set; } //

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        public MembershipType MembershipType { get; set; } // This is called Navigation Property. Allows to navigate from one type to another . 
                                                           //From customer to its membership type.
                                                           //Useful when we want to load object and its related object from db (Customer & its membership type)

        [Display(Name = "Membership Type")]
        public byte MembershipTypeId { get; set; } //Entity Framwework take it as foreign key here: name of type+Id (MembershipType class)


        [Display(Name = "Date of Birth")]
        public DateTime? DateOfBirth { get; set; }


    }
}