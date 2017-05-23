﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Amigos.Models
{
    public class Amigo
    {
        public int ID { get; set; }
        [Display(Name = "Nombre")]
        public string name { get; set; }
        [Display(Name = "Longitud")]
        public string longi { get; set; }
        [Display(Name = "Latitud")]
        public string lati { get; set; }
    }
}