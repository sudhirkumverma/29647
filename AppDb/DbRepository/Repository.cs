using AppModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppDb.DbRepository
{
    public class Repository
    {
        string connStr = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;


        public List<LoginDetailModel> AdminLogin(LoginDetailModel Objlog)
        {
            var data = new List<LoginDetailModel>();
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                string username = Objlog.UserName;
                string Password = Objlog.PassWord;
                SqlCommand cmd = new SqlCommand("select * from tblStaffMaster where UserName='" + username + "' and PassWord='" + Password + "'", con);


                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {

                        data.Add(item: new LoginDetailModel
                        {
                            Name = dr["Name"].ToString(),
                            UserName = Objlog.UserName,
                            Role = dr["Type"].ToString()
                        });

                    }
                }
                else
                {

                }
            }

            return data.ToList();

        }

        public string TotalCustomer()
        {
            string count = "";

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select count(*)as member from tbl_customer_master", con);
                string img = "";
                SqlDataReader dr6 = cmd.ExecuteReader();
                int i = 0;
                while (dr6.Read())
                {
                    count = dr6["member"].ToString();

                }

            }
            return count;


        }
        public string TotalMoney()
        {
            string count = "";

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select count(*)as member from tbl_customer_master", con);
                string img = "";
                SqlDataReader dr6 = cmd.ExecuteReader();
                int i = 0;
                while (dr6.Read())
                {
                    count = dr6["member"].ToString();

                }

            }
            return count;


        }

        public string PaidAmount()
        {
            string count = "";
            string count2 = "";

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select sum(cast(PaidAmount as int)) as member from Payment", con);

                SqlDataReader dr6 = cmd.ExecuteReader();
                int i = 0;
                while (dr6.Read())
                {
                    count = dr6["member"].ToString();

                }


            }

            if (count == "")
            {
                count = "0";
            }
            return count.ToString();


        }

        public string pendingAmt()
        {
            string count = "";
            string count2 = "";

            using (SqlConnection con = new SqlConnection(connStr))
            {

                con.Open();
                SqlCommand cmd = new SqlCommand("select sum(Tprice) as member from Customer_Food_Expence where status='0'", con);
                string img = "";
                SqlDataReader dr6 = cmd.ExecuteReader();
                int i = 0;
                while (dr6.Read())
                {
                    count = dr6["member"].ToString();

                }
                SqlCommand cmd2 = new SqlCommand("select  sum(PaidAmount - TotalAmountPaidByCustomer) as tpen from Payment where PaidAmount > TotalAmountPaidByCustomer and Status !='3' ", con);

                SqlDataReader dr = cmd2.ExecuteReader();

                if (dr.Read())
                {
                    if (dr["tpen"].ToString() != "" && dr["tpen"].ToString() != null)
                    {
                        count2 = dr["tpen"].ToString();
                    }

                }




                if (count == "" && count2 == "")
                {
                    count = "0";
                }
                else if (count != "" && count2 != "")
                {
                    int mc = int.Parse(count) + int.Parse(count2);
                    count = mc.ToString();
                }
                else if (count == "" && count2 != "")
                {
                    count = count2;
                }
                else if (count2 != "" && count2 == "")
                {

                }
            }
            return count;


        }


        public string ActiveCustomer()
        {
            string count = "";

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select count(*)as member from Customer_Food_Expence where status!='1'", con);
                string img = "";
                SqlDataReader dr6 = cmd.ExecuteReader();
                int i = 0;
                while (dr6.Read())
                {
                    count = dr6["member"].ToString();

                }

            }
            return count;


        }
        public string TotalItem()
        {
            string count = "";

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select count(*)as item from Foodmenu where Status=1", con);

                string img = "";
                SqlDataReader dr6 = cmd.ExecuteReader();
                int i = 0;
                while (dr6.Read())
                {
                    count = dr6["item"].ToString();

                }

            }
            return count;


        }

        public List<ItemDetailModel> ItemList(ItemDetailModel ObjItem)
        {
            var data = new List<ItemDetailModel>();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("SELECT  TOP (100) PERCENT dbo.categoryMaster.CategoryName,dbo.categoryMaster.id as cid, dbo.SubcategoryMaster.SubCateGoryName,dbo.SubcategoryMaster.id as scid, dbo.Foodmenu.ItemName, dbo.Foodmenu.itemPrice, dbo.Foodmenu.status, dbo.Foodmenu.id FROM  dbo.categoryMaster INNER JOIN dbo.Foodmenu ON dbo.categoryMaster.id = dbo.Foodmenu.CateGoryId INNER JOIN dbo.SubcategoryMaster ON dbo.Foodmenu.SubCateGoryId = dbo.SubcategoryMaster.id where dbo.Foodmenu.status !=0 ORDER BY dbo.Foodmenu.id DESC", con);

                if (ObjItem.SubcategoryId != 0)
                {
                    cmd = new SqlCommand("SELECT  TOP (100) PERCENT dbo.categoryMaster.CategoryName,dbo.categoryMaster.id as cid, dbo.SubcategoryMaster.SubCateGoryName,dbo.SubcategoryMaster.id as scid, dbo.Foodmenu.ItemName, dbo.Foodmenu.itemPrice, dbo.Foodmenu.status, dbo.Foodmenu.id FROM  dbo.categoryMaster INNER JOIN dbo.Foodmenu ON dbo.categoryMaster.id = dbo.Foodmenu.CateGoryId INNER JOIN dbo.SubcategoryMaster ON dbo.Foodmenu.SubCateGoryId = dbo.SubcategoryMaster.id where dbo.Foodmenu.status !=0 and dbo.Foodmenu.SubCateGoryId='" + ObjItem.SubcategoryId + "' ORDER BY dbo.Foodmenu.id DESC", con);

                }

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    data.Add(item: new ItemDetailModel()
                    {
                        id = int.Parse(dr["id"].ToString()),
                        ItemName = dr["ItemName"].ToString(),
                        ItemPrice = dr["itemPrice"].ToString(),
                        CategoryName = dr["CategoryName"].ToString(),
                        SubCategoryName = dr["SubCateGoryName"].ToString(),
                        SubcategoryId = int.Parse(dr["scid"].ToString()),
                        categoryId = int.Parse(dr["cid"].ToString())


                    });
                }
            }
            return data.ToList();

        }

        public List<StaffDetailModel> GetAllStaff()
        {
            var data = new List<StaffDetailModel>();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from tblStaffMaster where Type!='Admin' order by id desc", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    data.Add(item: new StaffDetailModel()
                    {
                        id = int.Parse(dr["id"].ToString()),
                        name = dr["name"].ToString(),
                        Email = dr["Email"].ToString(),
                        mobile = dr["mobile"].ToString(),
                        adresss = dr["adresss"].ToString(),
                        UserName = dr["UserName"].ToString(),
                        AddedOn = dr["AddedOn"].ToString()

                    });
                }
            }
            return data.ToList();
        }

        public bool AddStaff(StaffDetailModel ObjStaff)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("select * from tblStaffMaster where mobile='" + ObjStaff.mobile + "' and Email='" + ObjStaff.Email + "'", con);


                SqlDataReader dr = cmd1.ExecuteReader();
                if (dr.HasRows)
                {
                    return false;
                }
                else
                {
                    dr.Close();
                    SqlCommand cmd = new SqlCommand("insert into tblStaffMaster values('" + ObjStaff.name + "','" + ObjStaff.Email + "','" + ObjStaff.mobile + "','" + ObjStaff.adresss + "','" + ObjStaff.Email + "','" + ObjStaff.Password + "','" + ObjStaff.AddedOn + "','Staff','1')", con);
                    int i = cmd.ExecuteNonQuery();
                    return true;
                }
            }

        }





        public bool AddItem(ItemDetailModel ObjItem)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("select * from Foodmenu where ItemName=@Iname and SubCateGoryId='" + ObjItem.SubcategoryId + "'and CateGoryId='" + ObjItem.categoryId + "'", con);

                cmd1.Parameters.AddWithValue("Iname", ObjItem.ItemName);

                SqlDataReader dr = cmd1.ExecuteReader();
                if (dr.HasRows)
                {
                    return false;
                }
                else
                {
                    dr.Close();
                    SqlCommand cmd = new SqlCommand("insert into Foodmenu(ItemName,itemPrice,status,SubCateGoryId,CateGoryId) values(@Iname,@Price,'1','" + ObjItem.SubcategoryId + "','" + ObjItem.categoryId + "')", con);
                    cmd.Parameters.AddWithValue("Iname", ObjItem.ItemName);
                    cmd.Parameters.AddWithValue("Price", ObjItem.ItemPrice);
                    int i = cmd.ExecuteNonQuery();
                    return true;
                }
            }

        }
        public bool AddSubCategory(ItemDetailModel ObjItem)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("select * from SubcategoryMaster where CategoryId=@Iname and SubCateGoryName=@SubCateGoryName", con);

                cmd1.Parameters.AddWithValue("Iname", ObjItem.categoryId);
                cmd1.Parameters.AddWithValue("SubCateGoryName", ObjItem.SubCategoryName);

                SqlDataReader dr = cmd1.ExecuteReader();
                if (dr.HasRows)
                {
                    return false;
                }
                else
                {
                    dr.Close();
                    SqlCommand cmd = new SqlCommand("insert into SubcategoryMaster(CategoryId,SubCateGoryName,status) values(@cid,@scname,'1')", con);
                    cmd.Parameters.AddWithValue("cid", ObjItem.categoryId);
                    cmd.Parameters.AddWithValue("scname", ObjItem.SubCategoryName);
                    int i = cmd.ExecuteNonQuery();
                    return true;
                }
            }

        }








        public bool UpdateItem(ItemDetailModel ObjItem)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("update FoodMenu set ItemName='" + ObjItem.ItemName + "' , itemPrice='" + ObjItem.ItemPrice + "' where id='" + ObjItem.id + "'", con);
                int x = cmd1.ExecuteNonQuery();
            }
            return true;
        }
        public bool UpdateSubCategory(ItemDetailModel ObjItem)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("update SubcategoryMaster set CategoryId='" + ObjItem.categoryId + "' , SubCateGoryName='" + ObjItem.SubCategoryName + "' where id='" + ObjItem.id + "'", con);
                int x = cmd1.ExecuteNonQuery();
            }
            return true;
        }
        public bool UpdateCategory(ItemDetailModel ObjItem)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("update categoryMaster set CategoryName='" + ObjItem.CategoryName + "'  where id='" + ObjItem.id + "'", con);
                int x = cmd1.ExecuteNonQuery();
            }
            return true;
        }
        public bool UpdateStaff(StaffDetailModel ObjStaff)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("update tblStaffMaster set name='" + ObjStaff.name + "',Email='" + ObjStaff.Email + "',mobile='" + ObjStaff.mobile + "',adresss='" + ObjStaff.adresss + "' where id='" + ObjStaff.id + "'", con);
                int x = cmd1.ExecuteNonQuery();
            }
            return true;
        }

        public bool DeleteItem(ItemDetailModel ObjItem)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("update FoodMenu set status='0' where id='" + ObjItem.id + "'", con);
                int x = cmd1.ExecuteNonQuery();
            }
            return true;
        }

        public List<customer_Detail_Model> GetCustomerForGuest(customer_Detail_Model ObjCust)
        {
            var data = new List<customer_Detail_Model>();
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                SqlCommand cmdr = new SqlCommand("select * from tbl_Customer_Master where AllOk='0'", con);
                SqlDataReader drr = cmdr.ExecuteReader();
                while (drr.Read())
                {

                    SqlCommand cmd = new SqlCommand("select id,  case when((select count(*) from Guest where CustomerId='" + drr["id"].ToString() + "') = (select TotalMember from  tbl_customer_master where id='" + drr["id"].ToString() + "') ) then '' else (select name from tbl_customer_master where id='" + drr["id"].ToString() + "') end as demo from tbl_customer_master where id='" + drr["id"].ToString() + "'", con);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        data.Add(item: new customer_Detail_Model()
                        {
                            id = int.Parse(dr["id"].ToString()),
                            Name = dr["Name"].ToString(),


                        });
                    }
                }
            }
            return data.ToList();


        }

        public List<customer_Detail_Model> GetAllCustomer(customer_Detail_Model ObjCust)
        {


            var data = new List<customer_Detail_Model>();
            string query = "select a.*,b.*,b.id as pid,b.PaymentMode as pmode from tbl_customer_master a left outer join  Payment b on a.id=b.CustomerId order  by a.id desc";

            if (ObjCust.AllOk != 0)
            {
                query = "select a.*,b.*,b.PaymentMode as pmode from tbl_customer_master a left outer join Payment b on a.id=b.CustomerId  where a.AllOk='0' order  by a.id desc";
            }

            else if (ObjCust.CheckinDate != null && ObjCust.CheckOut != null)
            {
                var chkin = DateTime.Parse(ObjCust.CheckinDate);
                var chkout = DateTime.Parse(ObjCust.CheckOut);
                query = "select a.*,b.*,b.PaymentMode as pmode from tbl_customer_master a left outer join Payment b on a.id=b.CustomerId where  a.CheckinDate >= '" + chkin + "' and a.CheckOut < = '" + chkout + "' order  by a.id desc";

            }
            else if (ObjCust.Mobile != null)
            {
                query = "select a.*,b.*,b.PaymentMode as pmode from tbl_customer_master a left outer join Payment b on a.id=b.CustomerId  where  a.mobile='" + ObjCust.Mobile + "' order by a.id desc";

            }
            if (ObjCust.Ctype != null)
            {
                string type = "1";
                if (ObjCust.Ctype == "pending")
                {
                    type = "0";
                }
                query = "select a.*,b.*,b.PaymentMode as pmode from tbl_customer_master a left outer join Payment b on a.id=b.CustomerId where a.AllOk='" + type + "' or (b.PaidAmount > b.TotalAmountPaidByCustomer and b.Status !='3') order  by a.id desc";

            }
            string amountpaid = "";
            int extra = 0;
            int Status = 0;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (dr["amountPaid"].ToString() == null || dr["amountPaid"].ToString() == "")

                    {
                        SqlCommand cmdg = new SqlCommand("select sum(Tprice) as Tprice from Customer_Food_Expence where CustomerId='" + dr["id"].ToString() + "'", con);

                        SqlDataReader drr = cmdg.ExecuteReader();
                        if (drr.Read())
                        {
                            amountpaid = drr["Tprice"].ToString();
                        }
                    }
                    else
                    {
                        amountpaid = dr["amountpaid"].ToString();
                    }

                    if (string.IsNullOrEmpty(dr["ExtraCharge"].ToString()) == false)
                    {
                        extra = int.Parse(dr["ExtraCharge"].ToString());
                    }
                    if (string.IsNullOrEmpty(dr["Status"].ToString()) == false)
                    {
                        Status = int.Parse(dr["Status"].ToString());
                    }
                    data.Add(item: new customer_Detail_Model()
                    {
                        id = int.Parse(dr["id"].ToString()),
                        Name = dr["Name"].ToString(),
                        Age = dr["Age"].ToString(),
                        Gender = dr["Gender"].ToString(),
                        TotalMember = int.Parse(dr["TotalMember"].ToString()),
                        IdProof = dr["IdProof"].ToString(),
                        CustomerType = dr["CustomerType"].ToString(),
                        RommAlloted = dr["RommAlloted"].ToString(),
                        RoomRent = int.Parse(dr["RoomRent"].ToString()),
                        CheckinDate = dr["CheckinDate"].ToString(),
                        AllOk = int.Parse(dr["AllOk"].ToString()),
                        CheckOut = dr["CheckOut"].ToString(),
                        amountPaid = amountpaid,
                        Mobile = dr["Mobile"].ToString(),
                        IdProof2 = dr["IdProof2"].ToString(),
                        AddressDetail = dr["Adress"].ToString(),
                        AlternateContact = dr["AlternateContact"].ToString(),
                        AdvanceGiven = dr["AdvanceGiven"].ToString(),
                        Email = dr["Email"].ToString(),
                        IdType = dr["IdType"].ToString(),
                        CheckoutBy = dr["CheckoutBy"].ToString(),
                        StayingFor = dr["StayTime"].ToString(),
                        BookingId = dr["BookingId"].ToString(),
                        PaymentMode = dr["AdvancePaymentMode"].ToString(),
                        RoomRentPaid = int.Parse(dr["RoomRentPaid"].ToString()),
                        Absconding = dr["AbsCond"].ToString(),
                        ExtraCharge = extra,
                        PaidAmount = dr["TotalAmountPaidByCustomer"].ToString(),
                        TotalBill = dr["PaidAmount"].ToString(),
                        CheckoutpaymentMode = dr["pmode"].ToString(),
                        status = Status




                    });
                }
            }
            return data.ToList();
        }


        public string pendingBill()
        {
            try
            {
                string query = "select count(distinct(id)) as member from  tbl_Customer_Master where AllOk='0'";

                string count = "";
                string count2 = "";

                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(query, con);

                    SqlDataReader dr6 = cmd.ExecuteReader();

                    while (dr6.Read())
                    {
                        count = dr6["member"].ToString();

                    }
                    SqlCommand cmd2 = new SqlCommand("select  count(*) as tpen from Payment where PaidAmount > TotalAmountPaidByCustomer and Status !='3' ", con);

                    SqlDataReader dr = cmd2.ExecuteReader();

                    if (dr.Read())
                    {
                        if (dr["tpen"].ToString() != "" && dr["tpen"].ToString() != null)
                        {
                            count2 = dr["tpen"].ToString();
                        }

                    }




                    if (count == "" && count2 == "")
                    {
                        count = "0";
                    }
                    else if (count != "" && count2 != "")
                    {
                        int mc = int.Parse(count) + int.Parse(count2);
                        count = mc.ToString();
                    }
                    else if (count == "" && count2 != "")
                    {
                        count = count2;
                    }
                    else if (count2 != "" && count2 == "")
                    {

                    }
                }
                return count;
            }
            catch (Exception)
            {
                return "0";
            }
        }




        public bool UpdatePassword(LoginDetailModel ObjLog)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("update tbl_login set PassWord='" + ObjLog.PassWord + "'where UserName='" + ObjLog.UserName + "'", con);
                int x = cmd1.ExecuteNonQuery();
            }
            return true;
        }

        public string AddCustomer(customer_Detail_Model ObjCustomer, string filename, string filename2, string Cuser)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();
                    SqlCommand cmd1 = new SqlCommand("select * from tbl_customer_master where Mobile=@Mobile and AllOk='0'", con);

                    cmd1.Parameters.AddWithValue("Mobile", ObjCustomer.Mobile);

                    SqlDataReader dr = cmd1.ExecuteReader();
                    if (dr.HasRows)
                    {
                        return "0";
                    }
                    else
                    {
                        SqlCommand cmd2 = new SqlCommand("select * from tbl_customer_master where RommAlloted='" + ObjCustomer.RommAlloted + "' and AllOk='0'", con);

                        cmd1.Parameters.AddWithValue("room", ObjCustomer.RommAlloted);

                        SqlDataReader dr2 = cmd2.ExecuteReader();
                        if (dr.HasRows)
                        {
                            return "2";
                        }
                        else
                        {
                            int roomRent = ObjCustomer.RoomRent;
                            Int16 stayingFor = 0;
                            Int16.TryParse(ObjCustomer.StayingFor, out stayingFor);

                            int advanceGiven = 0;
                            int roomRentPaid = ObjCustomer.RoomRentPaid;
                            if((roomRent * stayingFor) >= roomRentPaid)
                            {
                                advanceGiven = 0;
                            } else
                            {
                                advanceGiven = roomRentPaid - (roomRent * stayingFor);
                                roomRentPaid = (roomRent * stayingFor);
                            }

                            string BookingId = ObjCustomer.BookingId;
                            Random rd = new Random();
                            int x = rd.Next(123, 123456);
                            if (ObjCustomer.BookingId == null)
                            {
                                BookingId = x.ToString();
                            }

                            SqlCommand cmd = new SqlCommand(@"INSERT INTO [dbo].[tbl_customer_master](
                                [Name],[Age],[Gender],[TotalMember],[IdProof],
                                [CustomerType],[RommAlloted],[RoomRent],[CheckinDate],
                                [AllOk],[CheckOut],[Mobile],[AlternateContact],[AddedBy],
                                [Adress],[IdType],[Idproof2],[Email],[AdvanceGiven],
                                [BookingId],[StayTime],[AdvancePaymentMode],[RoomRentPaid],[ExtraCharge])
                                values('" + ObjCustomer.Name + "','" + 
                                ObjCustomer.Age + "','" + 
                                ObjCustomer.Gender + "','" + 
                                ObjCustomer.TotalMember + "','" + 
                                filename + "','" + 
                                ObjCustomer.CustomerType + "','" + 
                                ObjCustomer.RommAlloted + "','" + 
                                ObjCustomer.RoomRent + "','" + 
                                ObjCustomer.CheckinDate + "','0','','" + 
                                ObjCustomer.Mobile + "','" + 
                                ObjCustomer.AlternateContact + "','" + 
                                Cuser + "','" + 
                                ObjCustomer.AddressDetail + "','" + 
                                ObjCustomer.IdType + "','" + 
                                filename2 + "','" + 
                                ObjCustomer.Email + "','" +
                                advanceGiven + "','" + //ObjCustomer.AdvanceGiven + "','" + 
                                BookingId + "','" + 
                                ObjCustomer.StayingFor + "','" + 
                                ObjCustomer.PaymentMode + "','" +
                                roomRentPaid + "','" + //ObjCustomer.RoomRentPaid + "','" + 
                                ObjCustomer.ExtraCharge + "')", con);

                            int i = cmd.ExecuteNonQuery();
                            return "1";
                        }
                    }
                }
            }
            catch (Exception)
            {
                return "5";
            }
        }

        public List<OrderDetailModel> GetAllOrder()
        {
            var data = new List<OrderDetailModel>();
            string cname = "";
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select a.*,b.ItemName as iname,a.id as cid from Customer_Food_Expence  a join FoodMenu b on a.ItemName=b.id where a.status='0' order by a.id desc ", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    SqlCommand cmdg = new SqlCommand("select * from tbl_customer_master where id='" + dr["CustomerId"].ToString() + "'", con);

                    SqlDataReader drr = cmdg.ExecuteReader();
                    if (drr.Read())
                    {
                        cname = drr["Name"].ToString() + "-" + drr["mobile"].ToString();
                    }


                    data.Add(item: new OrderDetailModel()
                    {
                        id = int.Parse(dr["id"].ToString()),
                        ItemName = dr["iname"].ToString(),
                        CustomerName = cname,
                        Price = dr["Tprice"].ToString(),
                        RoomNo = dr["RommNo"].ToString(),
                        Qty = int.Parse(dr["Qty"].ToString()),
                        AddedOn = dr["dateAdded"].ToString(),
                        AddedBy = dr["AddedBy"].ToString()



                    });
                }
            }
            return data.ToList();

        }

        public List<customer_Detail_Model> GetAroom()
        {
            var data = new List<customer_Detail_Model>();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from tbl_customer_master where AllOk='0' order by id desc", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    data.Add(item: new customer_Detail_Model()
                    {
                        id = int.Parse(dr["id"].ToString()),
                        RommAlloted = dr["RommAlloted"].ToString(),
                        Name = dr["Name"].ToString(),



                    });
                }
            }
            return data.ToList();

        }

        public List<customer_Detail_Model> GetCustomer(string RoomNo)
        {
            var data = new List<customer_Detail_Model>();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from tbl_customer_master where RommAlloted='" + RoomNo + "' order by id desc", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    data.Add(item: new customer_Detail_Model()
                    {
                        id = int.Parse(dr["id"].ToString()),
                        Name = dr["Name"].ToString(),
                        Mobile = dr["Mobile"].ToString()

                    });
                }
            }
            return data.ToList();

        }


        public List<ItemDetailModel> getPrice(string ItemId)
        {
            var data = new List<ItemDetailModel>();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from FoodMenu where id='" + ItemId + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    data.Add(item: new ItemDetailModel()
                    {
                        id = int.Parse(dr["id"].ToString()),
                        ItemPrice = dr["itemPrice"].ToString()



                    });
                }
            }
            return data.ToList();

        }

        public bool ProcessOrder(OrderDetailModel Objo, string AddedBy)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connStr))
                {
                    con.Open();
                    for (var i = 0; i < Objo.Item2.Count(); i++)
                    {
                        SqlCommand cmd = new SqlCommand("insert into Customer_Food_Expence values('" + Objo.RoomNo2[i] + "','" + Objo.Item2[i] + "','" + Objo.Qty2[i] + "','" + Objo.Price2[i] + "','" + Objo.AddedOn + "','0','" + Objo.CustomerId + "','" + AddedBy + "')", con);
                        int x = cmd.ExecuteNonQuery();
                    }

                }
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteOrder(int id)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("delete from Customer_Food_Expence where id='" + id + "'", con);
                int x = cmd.ExecuteNonQuery();

            }
            return true;
        }

        public List<GenerateBillModel> GenerateBill(string RoomNo/*this is actually customer id*/)
        {
            var data = new List<GenerateBillModel>();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select a.*,b.Name,b.CheckinDate,b.RommAlloted ,b.StayTime,b.AdvanceGiven,b.RoomRentPaid ,c.ItemName as Iname,a.Qty,a.Tprice,b.RoomRent,b.ExtraCharge from Customer_Food_Expence a left outer join tbl_customer_master b on a.RommNo=b.RommAlloted and a.CustomerId=b.id join Foodmenu c on a.ItemName=c.id where b.id='" + RoomNo + "' and b.AllOk='0'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        data.Add(item: new GenerateBillModel()
                        {
                            id = int.Parse(dr["id"].ToString()),
                            CustomerName = dr["Name"].ToString(),
                            RoomNo = RoomNo,
                            ItemName = dr["Iname"].ToString(),
                            Price = dr["Tprice"].ToString(),
                            Qty = int.Parse(dr["Qty"].ToString()),
                            CheckIn = DateTime.Parse(dr["CheckinDate"].ToString()),
                            RoomRent = int.Parse(dr["RoomRent"].ToString()),
                            bookingDuration = int.Parse(dr["StayTime"].ToString()),
                            AdvanceGiven = dr["AdvanceGiven"].ToString(),
                            RoomRentPaid = dr["RoomRentPaid"].ToString(),
                            ExtraAmount = dr["ExtraCharge"].ToString()


                        });
                    }
                }
                else
                {
                    SqlCommand cmd2 = new SqlCommand("select * from tbl_Customer_Master where id='" + RoomNo + "' and AllOk='0'", con);
                    SqlDataReader dr2 = cmd2.ExecuteReader();

                    while (dr2.Read())
                    {
                        data.Add(item: new GenerateBillModel()
                        {
                            id = int.Parse(dr2["id"].ToString()),
                            CustomerName = dr2["Name"].ToString(),
                            RoomNo = RoomNo,


                            CheckIn = DateTime.Parse(dr2["CheckinDate"].ToString()),
                            RoomRent = int.Parse(dr2["RoomRent"].ToString()),
                            bookingDuration = int.Parse(dr2["StayTime"].ToString()),
                            AdvanceGiven = dr2["AdvanceGiven"].ToString(),
                            RoomRentPaid = dr2["RoomRentPaid"].ToString(),
                            ExtraAmount = dr2["ExtraCharge"].ToString()


                        });
                    }



                }
            }
            return data.ToList();

        }

        public bool CheckOut(GenerateBillModel ObjBill, string checkoutBy)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("select * from Payment where CustomerId='" + ObjBill.RoomNo + "'", con);
                SqlDataReader checkout = cmd.ExecuteReader();
                if (checkout.HasRows)
                {
                    return false;
                }
                else
                {
                    SqlCommand cmd1 = new SqlCommand("update tbl_customer_master set AllOk='1' ,CheckoutBy='" + checkoutBy + "', CheckOut='" + ObjBill.Checkout + "',amountPaid='" + ObjBill.Price + "',AbsCond='" + ObjBill.Absconding + "' where id='" + ObjBill.RoomNo + "' and AllOk='0'", con);
                    int x = cmd1.ExecuteNonQuery();

                    SqlCommand cmd2 = new SqlCommand("update Customer_Food_Expence set status='1'where CUstomerId='" + ObjBill.RoomNo + "' and status='0'", con);
                    int x2 = cmd2.ExecuteNonQuery();


                    SqlCommand cmd3 = new SqlCommand("insert into Payment values('" + ObjBill.RoomNo + "','" + ObjBill.Price + "','" + ObjBill.Checkout + "','" + checkoutBy + "','" + ObjBill.tax + "','" + ObjBill.BillNo + "','" + ObjBill.ExtraAmount + "','" + ObjBill.Absconding + "','" + ObjBill.TotalPrice + "','" + ObjBill.AdvanceGiven + "','" + ObjBill.ExtramountFor + "','2','" + ObjBill.Payment + "')", con);
                    int x3 = cmd3.ExecuteNonQuery();
                }
                return true;
            }

        }

        public List<GenerateBillModel> getBill(string CustomerId)
        {
            var data = new List<GenerateBillModel>();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select a.* ,b.* from Payment a join PaymentView b on a.CustomerId=b.CustomerId where a.CustomerId='" + CustomerId + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        data.Add(item: new GenerateBillModel()
                        {
                            id = int.Parse(dr["id"].ToString()),
                            CustomerName = dr["Name"].ToString(),

                            ItemName = dr["ItemName"].ToString(),
                            Price = dr["Tprice"].ToString(),
                            Qty = int.Parse(dr["Qty"].ToString()),
                            CheckIn = DateTime.Parse(dr["DateAdded"].ToString()),
                            TotalPrice = dr["PaidAmount"].ToString(),
                            tax = int.Parse(dr["tax"].ToString()),
                            BillNo = dr["BillNo"].ToString(),
                            RoomNo = dr["RommAlloted"].ToString(),
                            contact = dr["Mobile"].ToString(),
                            bookingDuration = int.Parse(dr["StayTime"].ToString()),
                            RoomRent = int.Parse(dr["RoomRent"].ToString()),
                            ExtraAmount = dr["ExtraCharge"].ToString() + "-" + dr["ExtraAmount"].ToString(),
                            AdvanceGiven = dr["AdditionalAmount"].ToString(),
                            ExtramountFor = dr["ExtraPaymentFor"].ToString(),
                            AmountPaidByCustomer = dr["TotalAmountPaidByCustomer"].ToString(),
                            RoomRentPaid = dr["RoomRentPaid"].ToString()




                        });
                    }
                }
                else
                {
                    SqlCommand cmd2 = new SqlCommand("select a.*,b.ExtraAmount,b.PaidAmount,b.BillNo,b.ExtraPaymentFor,b.AdditionalAmount,b.TotalAmountPaidByCustomer from tbl_Customer_Master a join Payment b on b.CustomerId=a.id where a.id='" + CustomerId + "'", con);
                    SqlDataReader dr2 = cmd2.ExecuteReader();

                    while (dr2.Read())
                    {
                        data.Add(item: new GenerateBillModel()
                        {
                            id = int.Parse(dr2["id"].ToString()),
                            CustomerName = dr2["Name"].ToString(),
                            RoomNo = dr2["RommAlloted"].ToString(),
                            TotalPrice = dr2["PaidAmount"].ToString(),
                            BillNo = dr2["BillNo"].ToString(),
                            contact = dr2["Mobile"].ToString(),
                            CheckIn = DateTime.Parse(dr2["CheckinDate"].ToString()),
                            RoomRent = int.Parse(dr2["RoomRent"].ToString()),
                            bookingDuration = int.Parse(dr2["StayTime"].ToString()),
                            AdvanceGiven = dr2["AdvanceGiven"].ToString(),
                            ExtraAmount = dr2["ExtraAmount"].ToString() + "-" + dr2["ExtraAmount"].ToString(),
                            ExtramountFor = dr2["ExtraPaymentFor"].ToString(),
                            AmountPaidByCustomer = dr2["TotalAmountPaidByCustomer"].ToString(),
                            RoomRentPaid = dr2["RoomRentPaid"].ToString()

                        });
                    }

                }
            }
            return data.ToList();
        }



        public List<customer_Detail_Model> SearchUnPaidCustomer(customer_Detail_Model ObjCust)
        {
            var data = new List<customer_Detail_Model>();
            string query = "select * from tbl_customer_master where AllOk='0' order by id desc";
            if (ObjCust.CheckinDate != null && ObjCust.CheckOut != null && ObjCust.Mobile != null)
            {
                query = "select * from tbl_customer_master where AllOk='0' and (CheckinDate >= '" + DateTime.Parse(ObjCust.CheckinDate) + "' and CheckOut <='" + DateTime.Parse(ObjCust.CheckinDate) + "' and mobile='" + ObjCust.Mobile + "') order by id desc";
            }
            else if (ObjCust.CheckinDate != null && ObjCust.CheckOut != null)
            {
                query = "select * from tbl_customer_master where AllOk='0' and (CheckinDate >= '" + DateTime.Parse(ObjCust.CheckinDate) + "' and CheckOut <='" + DateTime.Parse(ObjCust.CheckinDate) + "') order by id desc";

            }
            else
            {
                query = "select * from tbl_customer_master where AllOk='0'  and mobile='" + ObjCust.Mobile + "' order by id desc";

            }
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    data.Add(item: new customer_Detail_Model()
                    {
                        id = int.Parse(dr["id"].ToString()),
                        RommAlloted = dr["RommAlloted"].ToString(),
                        Name = dr["Name"].ToString(),



                    });
                }
            }
            return data.ToList();

        }



        public bool AddGuest(customer_Detail_Model ObjCust, string filename, string filenme2)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("insert into Guest values('" + ObjCust.guest + "','" + ObjCust.Name + "','" + ObjCust.Age + "','" + ObjCust.Mobile + "','" + ObjCust.Gender + "','" + filename + "','','" + DateTime.Now.ToString() + "','" + filenme2 + "','" + ObjCust.IdType + "')", con);
                int x = cmd1.ExecuteNonQuery();
            }
            return true;

        }

        public List<customer_Detail_Model> GetGuest(string CustomerId)
        {
            var data = new List<customer_Detail_Model>();
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from Guest where CustomerId='" + CustomerId + "'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    data.Add(item: new customer_Detail_Model()
                    {
                        Name = dr["Name"].ToString(),
                        Age = dr["Age"].ToString(),
                        Mobile = dr["mobile"].ToString(),
                        Gender = dr["gender"].ToString(),
                        IdProof = dr["idProof"].ToString(),
                        IdProof2 = dr["idproof2"].ToString(),
                        IdType = dr["IdType"].ToString(),



                    });
                }
            }
            return data.ToList();


        }


        public List<TaxDetailModel> GetAllTax()
        {
            var data = new List<TaxDetailModel>();

            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from taxmaster order by id desc", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    data.Add(item: new TaxDetailModel()
                    {
                        id = int.Parse(dr["id"].ToString()),
                        TaxName = dr["TaxName"].ToString(),
                        TaxAmount = int.Parse(dr["TaxAmount"].ToString())




                    });
                }
            }
            return data.ToList();

        }

        public bool AddTax(TaxDetailModel ObjTax)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("select * from taxmaster where TaxName=@Iname", con);

                cmd1.Parameters.AddWithValue("Iname", ObjTax.TaxName);

                SqlDataReader dr = cmd1.ExecuteReader();
                if (dr.HasRows)
                {
                    return false;
                }
                else
                {
                    dr.Close();
                    SqlCommand cmd = new SqlCommand("insert into taxmaster(TaxName,TaxAmount) values(@Iname,@Price)", con);
                    cmd.Parameters.AddWithValue("Iname", ObjTax.TaxName);
                    cmd.Parameters.AddWithValue("Price", ObjTax.TaxAmount);
                    int i = cmd.ExecuteNonQuery();
                    return true;
                }
            }

        }

        public bool UpdateTax(TaxDetailModel objtax)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("update taxmaster set TaxName='" + objtax.TaxName + "' , TaxAmount='" + objtax.TaxAmount + "' where id='" + objtax.id + "'", con);
                int x = cmd1.ExecuteNonQuery();
            }
            return true;
        }

        public List<RoomDetailModel> GetallRoom()
        {
            var data = new List<RoomDetailModel>();
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("select * from RoomMaster", con);

                SqlDataReader dr = cmd1.ExecuteReader();
                while (dr.Read())
                {
                    data.Add(new RoomDetailModel()
                    {
                        id = int.Parse(dr["id"].ToString()),
                        RoomNo = dr["RoomNo"].ToString(),
                        addedon = dr["addedOn"].ToString(),
                        status = dr["status"].ToString()
                    });
                }
                return data.ToList();
            }

        }

        public bool AddRoom(RoomDetailModel ObjRoom)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("select * from RoomMaster where RoomNo=@Iname", con);

                cmd1.Parameters.AddWithValue("Iname", ObjRoom.RoomNo);

                SqlDataReader dr = cmd1.ExecuteReader();
                if (dr.HasRows)
                {
                    return false;
                }
                else
                {
                    dr.Close();
                    SqlCommand cmd = new SqlCommand("insert into RoomMaster(RoomNo,addedOn) values(@Iname,@Price)", con);
                    cmd.Parameters.AddWithValue("Iname", ObjRoom.RoomNo);
                    cmd.Parameters.AddWithValue("Price", DateTime.Now.ToShortDateString());
                    int i = cmd.ExecuteNonQuery();
                    return true;
                }
            }

        }

        public bool DeleteRoom(RoomDetailModel ObjRoom)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("delete from RoomMaster where id='" + ObjRoom.id + "'", con);
                int x = cmd1.ExecuteNonQuery();
            }
            return true;
        }

        public List<RoomDetailModel> GetRoomAvailablity()
        {
            var data = new List<RoomDetailModel>();
            using (SqlConnection con = new SqlConnection(connStr))
            {

                string status = "1";

                con.Open();
                SqlCommand cmd1 = new SqlCommand("select * from RoomMaster order by RoomNo", con);

                SqlDataReader dr = cmd1.ExecuteReader();
                while (dr.Read())
                {

                    SqlCommand cmd2 = new SqlCommand("select * from tbl_Customer_master where RommAlloted='" + dr["RoomNo"].ToString() + "' and AllOk='0'", con);
                    SqlDataReader dr2 = cmd2.ExecuteReader();
                    if (dr2.HasRows)
                    {
                        status = "0";
                    }
                    else
                    {
                        status = "1";
                    }
                    data.Add(new RoomDetailModel()
                    {
                        id = int.Parse(dr["id"].ToString()),
                        RoomNo = dr["RoomNo"].ToString(),
                        addedon = dr["addedOn"].ToString(),
                        status = status
                    });
                }
                return data.ToList();
            }

        }

        public List<ItemDetailModel> GetAllCategory()
        {
            var data = new List<ItemDetailModel>();
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("select * from categorymaster", con);

                SqlDataReader dr = cmd1.ExecuteReader();
                while (dr.Read())
                {
                    data.Add(new ItemDetailModel()
                    {
                        id = int.Parse(dr["id"].ToString()),
                        CategoryName = dr["CategoryName"].ToString(),

                    });
                }
                return data.ToList();
            }
        }
        public List<ItemDetailModel> GetAllSubCategory(ItemDetailModel ObjItem)
        {
            SqlCommand cmd1 = null;
            var data = new List<ItemDetailModel>();
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                if (ObjItem.categoryId != 0)
                {
                    cmd1 = new SqlCommand("select a.*,c.*,a.id as cid from SubcategoryMaster a join categoryMaster c on a.CategoryId=c.id where a.CategoryId='" + ObjItem.categoryId + "' ", con);

                }
                else
                {
                    cmd1 = new SqlCommand("select a.*,c.*,a.id as cid from SubcategoryMaster a join categoryMaster c on a.CategoryId=c.id ", con);
                }
                SqlDataReader dr = cmd1.ExecuteReader();
                while (dr.Read())
                {
                    data.Add(new ItemDetailModel()
                    {
                        id = int.Parse(dr["cid"].ToString()),
                        CategoryName = dr["CategoryName"].ToString(),
                        SubCategoryName = dr["SubCateGoryName"].ToString()
                    });
                }
                return data.ToList();
            }
        }


        public List<Barchartmodel> GetChart()
        {
            var data = new List<Barchartmodel>();
            using (SqlConnection con = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT count(*) as Total, DATENAME(month,CheckinDate) AS DatePartString, sum(case when AllOk = '1' then 1 else 0 end) As totalpaid, sum(case when AllOk = '0' then 1 else 0 end) As pendingcount FROM tbl_customer_master where DATENAME(year, CheckinDate)= year(SYSDATETIME()) group by DATENAME(month, CheckinDate)", con))
                {
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        data.Add(new Barchartmodel()
                        {
                            totalcustomer = int.Parse(dr["Total"].ToString()),
                            totalUnpaid = dr["pendingcount"].ToString(),
                            TotalPaid = dr["totalpaid"].ToString(),
                            MonthName = dr["DatePartString"].ToString()
                        });



                    }
                }
                return data.ToList();
            }
        }

        public bool AddCategory(ItemDetailModel ObjItem)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("select * from categoryMaster where categoryName=@Iname", con);

                cmd1.Parameters.AddWithValue("Iname", ObjItem.CategoryName);

                SqlDataReader dr = cmd1.ExecuteReader();
                if (dr.HasRows)
                {
                    return false;
                }
                else
                {
                    dr.Close();
                    SqlCommand cmd = new SqlCommand("insert into categoryMaster(CategoryName,status) values(@Iname,'1')", con);
                    cmd.Parameters.AddWithValue("Iname", ObjItem.CategoryName);

                    int i = cmd.ExecuteNonQuery();
                    return true;
                }
            }
        }

        public bool UpdateCustomer(customer_Detail_Model ObjCust)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                string query = "update tbl_Customer_Master set RommAlloted='" + ObjCust.RommAlloted + "',RoomRent='" + ObjCust.RoomRent + "',StayTime='" + ObjCust.StayingFor + "' where BookingId='" + ObjCust.BookingId + "'";
                if (ObjCust.RommAlloted == null)
                {
                    query = "update tbl_Customer_Master set RoomRent='" + ObjCust.RoomRent + "',StayTime='" + ObjCust.StayingFor + "' where BookingId='" + ObjCust.BookingId + "'";
                }

                string query2 = "update tbl_Customer_Master set AdvanceGiven='" + ObjCust.AdvanceGiven + "' where BookingId='" + ObjCust.BookingId + "'";



                SqlCommand cmd = new SqlCommand(query, con);
                SqlCommand cmd2 = new SqlCommand(query2, con);

                int i = cmd.ExecuteNonQuery();
                int ii = cmd2.ExecuteNonQuery();
                return true;
            }
        }

        public string GetLastBill(string Mobile)
        {
            string lastbill = "0";
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("select * from tbl_Customer_Master where mobile='" + Mobile + "' order by id desc", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    SqlCommand cmd2 = new SqlCommand("select CustomerId, sum(cast(PaidAmount as int ) - TotalAmountPaidByCustomer) as tsum from Payment where CustomerId='" + dr["id"].ToString() + "' and Status='2' group by CustomerId", con);
                    SqlDataReader dr2 = cmd2.ExecuteReader();
                    while (dr2.Read())
                    {
                        lastbill = dr2["tsum"].ToString();
                    }




                }

                return lastbill;
            }

        }

        public bool DeleteCustomer(int id)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("delete from tbl_Customer_Master where id='" + id + "'", con);
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        public List<customer_Detail_Model> GetCustomerHistory()
        {
            var data = new List<customer_Detail_Model>();
            string query = "select a.*,b.*, b.PaymentMode as pmode from tbl_customer_master  a  join Payment b on b.CustomerId=a.id order  by a.id desc";



            string amountpaid = "";
            int extra = 0;
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (dr["amountPaid"].ToString() == null || dr["amountPaid"].ToString() == "")

                    {
                        SqlCommand cmdg = new SqlCommand("select sum(Tprice) as Tprice from Customer_Food_Expence where CustomerId='" + dr["id"].ToString() + "'", con);

                        SqlDataReader drr = cmdg.ExecuteReader();
                        if (drr.Read())
                        {
                            amountpaid = drr["Tprice"].ToString();
                        }
                    }
                    else
                    {
                        amountpaid = dr["amountpaid"].ToString();
                    }

                    if (string.IsNullOrEmpty(dr["ExtraCharge"].ToString()) == false)
                    {
                        extra = int.Parse(dr["ExtraCharge"].ToString());
                    }
                    data.Add(item: new customer_Detail_Model()
                    {
                        id = int.Parse(dr["id"].ToString()),
                        Name = dr["Name"].ToString(),
                        Age = dr["Age"].ToString(),
                        Gender = dr["Gender"].ToString(),
                        TotalMember = int.Parse(dr["TotalMember"].ToString()),
                        IdProof = dr["IdProof"].ToString(),
                        CustomerType = dr["CustomerType"].ToString(),
                        RommAlloted = dr["RommAlloted"].ToString(),
                        RoomRent = int.Parse(dr["RoomRent"].ToString()),
                        CheckinDate = dr["CheckinDate"].ToString(),
                        AllOk = int.Parse(dr["AllOk"].ToString()),
                        CheckOut = dr["CheckOut"].ToString(),
                        amountPaid = amountpaid,
                        Mobile = dr["Mobile"].ToString(),
                        IdProof2 = dr["IdProof2"].ToString(),
                        AddressDetail = dr["Adress"].ToString(),
                        AlternateContact = dr["AlternateContact"].ToString(),
                        AdvanceGiven = dr["AdvanceGiven"].ToString(),
                        Email = dr["Email"].ToString(),
                        IdType = dr["IdType"].ToString(),
                        CheckoutBy = dr["CheckoutBy"].ToString(),
                        StayingFor = dr["StayTime"].ToString(),
                        BookingId = dr["BookingId"].ToString(),
                        PaymentMode = dr["pmode"].ToString(),
                        RoomRentPaid = int.Parse(dr["RoomRentPaid"].ToString()),
                        Absconding = dr["AbsCond"].ToString(),
                        ExtraCharge = extra,
                        TotalBill = dr["PaidAmount"].ToString(),
                        PaidAmount = dr["TotalAmountPaidByCustomer"].ToString(),
                        status = int.Parse(dr["Status"].ToString())




                    });
                }
            }
            return data.ToList();
        }

        public bool UpdateSettlement(int CustomerId)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("update Payment set Status='3'  where CustomerId='" + CustomerId + "'", con);
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        public string GetLastId()
        {
            string LastId = "0";
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("select * from Payment order by id desc", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    LastId = dr["id"].ToString();



                }

                return LastId;
            }
        }
        public string GetLastBookingId()
        {
            string LastId = "0";
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("select * from tbl_Customer_Master order by id desc", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {

                    LastId = dr["id"].ToString();



                }

                return LastId;
            }
        }

        public bool DeleteTax(int id)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("delete  from TaxMaster  where id='" + id + "'", con);
                int x = cmd1.ExecuteNonQuery();
            }
            return true;
        }

    }
}
