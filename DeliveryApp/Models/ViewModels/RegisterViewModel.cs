using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Ce champs est obligatoire.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Ce champs est obligatoire.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Ce champs est obligatoire."), EmailAddress(ErrorMessage = "Adresse E-mail invalide.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Ce champs est obligatoire."), MinLength(4, ErrorMessage ="Doit contenir au moins 4 caractères.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Ce champs est obligatoire."), MaxLength(8, ErrorMessage = "Doit contenir 8 caractères."), MinLength(8, ErrorMessage = "Doit contenir 8 caractères.")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Ce champs est obligatoire.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Ce champs est obligatoire.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Ce champs est obligatoire.")]
        public int ZipCode { get; set; }
    }
}
