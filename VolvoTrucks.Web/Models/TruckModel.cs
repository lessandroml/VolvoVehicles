using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace VolvoTrucks.Models
{
    public class TruckModel
    {
        //DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "ID")]
        public int TruckModelID { get; set; }

        [Display(Name = "Model Name")]
        [StringLength(10, MinimumLength = 1)]
        public string ModelName { get; set; }

        public ICollection<Truck> Trucks { get; set; }
    }
}
