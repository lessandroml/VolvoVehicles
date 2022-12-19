using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VolvoTrucks.Models
{
    public class Truck
    {
        [Display(Name = "ID")]
        public int TruckID { get; set; }


        public int TruckModelID { get; set; }

        [Display(Name = "Manufacturing Year")]
        public int ManufacturingYear { get; set; }

        [Display(Name = "Model Year")]
        public int ModelYear { get; set; }

        [Display(Name = "Model")]
        public TruckModel TruckModel { get; set; }
    }
}
