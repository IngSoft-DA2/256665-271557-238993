using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModel.Responses.OwnerResponses;

namespace WebModel.Responses.FlatResponses
{
    public class GetFlatResponse
    {
        public Guid Id { get; set; }
        public int Floor { get; set; }
        public int RoomNumber { get; set; }
        public OwnerResponse Owner { get; set; }
        public int TotalBaths { get; set; }
        public int TotalRooms { get; set; }



    }
}
