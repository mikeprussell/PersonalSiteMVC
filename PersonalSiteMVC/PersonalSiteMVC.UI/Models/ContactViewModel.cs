﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations; //Added for access to annotations (i.e. [Required])

namespace PersonalSiteMVC.UI.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "*")]
        public string Name { get; set; }

        [Required(ErrorMessage = "*")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "*")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "*")]
        [UIHint("MultilineText")] //Provide a TextArea (larger text box) in the UI
        public string Message { get; set; }

    }

}