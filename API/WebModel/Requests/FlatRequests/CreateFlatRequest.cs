using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModel.Requests.OwnerRequests;
using WebModel.Responses.OwnerResponses;

namespace WebModel.Requests.FlatRequests
{
    public class CreateFlatRequest
    {
        public int Floor { get; set; }
        public int RoomNumber { get; set; }
        public AssignOwnerToFlatRequest? Owner { get; set; }
        public int TotalBaths { get; set; }
        public int TotalRooms { get; set; }
        public bool HasTerrace { get; set; }
    }
}