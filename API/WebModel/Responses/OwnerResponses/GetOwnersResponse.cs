using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModel.Responses.FlatResponses;

namespace WebModel.Responses.OwnerResponses
{
    public class GetOwnerResponse
    {
        public Guid Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }

        public override bool Equals(object objectToCompare)
        {
            GetOwnerResponse? toCompare = objectToCompare as GetOwnerResponse;
                
            return
                Id == toCompare.Id &&
                Firstname == toCompare.Firstname &&
                Lastname == toCompare.Lastname &&
                Email == toCompare.Email;
        }
    }
}