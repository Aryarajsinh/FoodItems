using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodItem.Models.ViewModel
{
    public class RegisterModel
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string Username { get; set; }


        [Required]
        //[RegularExpression(@"^([a-z]+['-]?[ ]?|[a-z]+['-]?)*?[a-z]$",ErrorMessage = "Please Currect FirstName Digit Are Not Allow")]
        public string FirstName { get; set; }
       
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        //[RegularExpression(@"^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$",ErrorMessage = "Please Enter Currect Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Please Currect Password")]
        public string ConformPassword { get; set; }

        [Required]
        [EmailAddress]
        //[RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*",ErrorMessage ="Please Valid Email")]
        public string Email { get; set; }

        [Required]
        public string[] hobbies { get; set; }
        [Required]
        public string gender { get; set; }
        [Required]
        public string profilePicture { get; set; }
        [Required]
        public string[] DestinationId { get; set; }
   
        
        [Required]
        [StringLength(10)]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessage = "Date of birth is required")]
        [Display(Name = "Date of Birth")]
        [DataType(DataType.DateTime)]
        public System.DateTime Dob { get; set; }
        [Required]
        public Nullable<int> role { get; set; }
        [Required]
        public Nullable<int> Country { get; set; }
        [Required]
        public Nullable<int> State { get; set; }
        [Required]
        public Nullable<int> City { get; set; }
    }
}
