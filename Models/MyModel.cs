using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WeddingPlanner.Models
{
    public class Users
    {
       [Key]
       public int UserId {get;set;}
       
       [Required]
       [MinLength(2)]
       public string FirstName {get;set;}

       [Required]
       [MinLength(2)]
       public string LastName {get;set;}

       [Required]
       [EmailAddress]
       public string Email {get;set;}

       [DataType(DataType.Password)]
       [Required]
       [MinLength(8, ErrorMessage="Password must be 8 characters or longer!")]
       public string Password {get;set;}
       public DateTime CreatedAt {get;set;} = DateTime.Now;
       public DateTime UpdatedAt {get;set;} = DateTime.Now;
       public List<Wedding> CreatedWedding {get;set;}
       public List<RSVP> Attending {get;set;}

       [NotMapped]
       [Compare("Password")]
       [DataType(DataType.Password)]
       public string Confirm {get;set;}
    }

    public class RSVP
    {
        [Key]
        public int RSVPId {get;set;}
        public int WeddingId {get;set;}
        public int UserId {get;set;}
        public Users Users {get;set;}
        public Wedding Wedding {get;set;}
    }

    public class Wedding
    {
        [Key]
        public int WeddingId {get;set;}
        public int UserId {get;set;}
        public List<RSVP> Attending {get;set;}
        public string Wedder1 {get;set;}
        public string Wedder2 {get;set;}
        public DateTime DateTime {get;set;}
        public string Address {get;set;}

    }
}