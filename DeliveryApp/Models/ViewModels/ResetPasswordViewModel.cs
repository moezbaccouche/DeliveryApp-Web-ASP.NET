using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Models.ViewModels
{
    public class ResetPasswordViewModel
    {
        public string IdentityId { get; set; }
        public string Token { get; set; }

        [Required(ErrorMessage = "Ce champs est obligatoire !"), MinLength(4, ErrorMessage = "Doit contenir au moins 4 caractères !")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Ce champs est obligatoire !")]
        [Compare("Password", ErrorMessage = "Les mots de passe ne correspondent pas !")]
        public string RepeatedPassword { get; set; }
    }
}
