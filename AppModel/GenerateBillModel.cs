using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppModel
{
   public class GenerateBillModel
    {
        public int id { get; set; }
        public string RoomNo { get; set; }
        public string CustomerName { get; set; }
        public string ItemName { get; set; }
        public DateTime CheckIn { get; set; }
        public int bookingDuration { get; set; }
        public string Price { get; set; }
        public int Qty { get; set; }
        public int RoomRent { get; set; }
        public string Payment { get; set; }
        public int tax { get; set; }
        public string TotalRoomCharge { get; set; }

        public string ExtramountFor { get; set; }
        public string BillNo { get; set; }
        public string TotalPrice { get; set; }
        public string ExtraAmount { get; set; }
        public string AdvanceGiven { get; set; }
        public string Absconding { get; set; }
        public string RoomRentPaid { get; set; }
        public string contact { get; set; }
        public string Checkout { get; set; }
        public string AmountPaidByCustomer { get; set; }
    }
}
