namespace INDIACom.Models
{
    public class MemberModel
    {
        public int ID { get; set; }  // Primary Key (Auto-increment)
        public string Salutation { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string CountryID { get; set; }
        public string State { get; set; }
        public string StateID { get; set; }
        public string City { get; set; }
        public string CityID { get; set; }
        public string Pincode { get; set; }
        public string Email { get; set; }
        public int Mobile { get; set; }
        public string EventID { get; set; }
        public int Event { get; set; }
        public int CSI_No { get; set; }
        public string IEEE_No { get; set; }
        public string Organisation { get; set; }
        public string Category { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }




    }



}