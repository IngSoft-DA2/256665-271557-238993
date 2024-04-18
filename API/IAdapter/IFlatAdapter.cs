using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModel.Responses.FlatResponses;

namespace IAdapter
{
    public interface IFlatAdapter
    {
        public IEnumerable<GetFlatResponse> GetAllFlats();
        public CreateFlatResponse CreateFlat(Guid idOfBuilding);
    }
}
