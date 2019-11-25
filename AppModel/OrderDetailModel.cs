using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppModel
{
   public class OrderDetailModel
    {
        public int id { get; set; }
        public string ItemName { get; set; }
        public string CustomerName { get; set; }
        public string  Price { get; set; }
        public string  RoomNo { get; set; }
        public int Qty { get; set; }
        public string AddedOn { get; set; }
        public string AddedBy { get; set; }
        //containers for inserting data
       
            public int CustomerId { get; set; }
        public string [] Item2 { get; set; }
        public string [] Price2 { get; set; }
        public string [] RoomNo2 { get; set; }
        public int [] Qty2 { get; set; }


    }
}
