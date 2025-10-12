using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyPost.Application.CQRS.Handlers.Query.GetContractMaxSerialNumber
{
	internal sealed class GetContractMaxSerialNumberHandler
		: IRequestHandler<GetContractMaxSerialNumberQuery, int>
	{
		private readonly IUnitOfWork _unitOfWork;
		public GetContractMaxSerialNumberHandler(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<int> Handle(GetContractMaxSerialNumberQuery request, CancellationToken cancellationToken)
		{
			var contractRepository = _unitOfWork.Repository<Contracts>();
			var maxSerialNumber = await
				contractRepository.FindAllAsync(cancellationToken: cancellationToken);
			return maxSerialNumber.Any() ? maxSerialNumber.Max(x => x.SerialNumber) + 1 : 1;
		}
	}
}
