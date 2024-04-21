using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModel.Requests.FlatRequests;
using WebModel.Responses.FlatResponses;

namespace IAdapter
{
    public interface IFlatAdapter
    {
        public IEnumerable<GetFlatResponse> GetAllFlats(Guid buildingId);
        
        public CreateFlatResponse CreateFlat(CreateFlatRequest flatToCreate);
        public GetFlatResponse GetFlatById(Guid flatId);
    }
}
