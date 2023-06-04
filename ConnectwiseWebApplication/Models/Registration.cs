using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace ConnectwiseWebApplication.Models
{
	
	public class Registration
    {
        public string companyName { get; set; }
        //[Required, EmailAddress, Display(Name = "Email is required")]
        public string companyEmail {get; set; }
        //[RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",ErrorMessage = "Please enter a valid email address")]
        //[Required(ErrorMessage = "Password is required.")]
        
        //[DataType(DataType.Password)]
        public string adminPassword { get; set; }
        public string BranchName { get; set; }
        public string street { get; set; }
        public string LandMark { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public int pincode { get; set; }

       
    }


    }
