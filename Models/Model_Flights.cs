using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FlightLogs.Models
{
    public class Model_Flights
    {
        public int flight_id { get; set; }
        public string PilotName { get; set; }
        public string Flight1 { get; set; }
        [DataType(DataType.DateTime), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}",//0:yyyy-MM-ddThh:mm:ss}", // @"{0:dd\/MM\/yyyy HH:mm}",
ApplyFormatInEditMode = true)]
        //public Nullable<System.DateTime> Schedule { get; set; }
        public DateTime Schedule { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
    }
}