using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RentingMovies.Models;


namespace RentingMovies.ViewModels
{
    public class CustomerFormViewModel
    {
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
        public Customer Customer { get; set; }
    }
}