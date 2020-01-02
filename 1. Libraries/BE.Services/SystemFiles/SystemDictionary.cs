using System.Collections.Generic;

namespace BE.Services.SystemFiles
{
    public class SystemDictionary
    {
        public Dictionary<string, List<string>> PhoneNumberInternalDictionary = new Dictionary<string, List<string>>();
        public Dictionary<string, List<string>> EmailInternalDictionary = new Dictionary<string, List<string>>();

        public SystemDictionary()
        {
            PhoneNumberDictionary();
            EmailDictionary();
        }

        public void PhoneNumberDictionary()
        {
            List<string> objList = new List<string>()
            {
                "Phone",
                "PhoneNumber",
                "Phone_Number",
                "Phone Number",
                "PhoneNo",
                "Ph.",
                "Ph.No.",
                "HomePhone",
                "Mobile",
                "MobileNumber",
                "Mobile_Number",
                "Mobile Number",
                "MobileNo",
                "Mob.",
                "Mob.No",
                "CompanyPhone"
            };
            PhoneNumberInternalDictionary.Add("Phone", new List<string>(objList));
        }

        public void EmailDictionary()
        {
            List<string> objList = new List<string>()
            {
                "Email",
                "EmailId",
                "Email_Id",
                "EmailAddress",
                "CompanyEmailAddress",
                "CompanyEmail",
                "OfficialEmailAddress",
                "OfficialEmailId",
                "OfficialEmail",
                "PersonalEmail"
            };
            EmailInternalDictionary.Add("Email", new List<string>(objList));
        }
    }
}
