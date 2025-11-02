namespace CompanyPost.Domain.Interface
{
	public interface ISharedContractProperty  : IHasSharedProperty
	{
		public string Value { get; set; }
		public string Details { get; set; }
		public string ContractNumber { get; set; } 
		public string? Notes { get; set; }
		public DateTime Contract_Date { get; set; }
		public string purchase_order_ref { get; set; }
		public Guid ProjectId { get; set; }
		public Publisher Projects { get; set; }
		public Guid PersonOrgId { get; set; }
		public Publisher PersonOrgs { get; set; }
		public Currency Currency { get; set; }
		public Guid CreatedById { get; set; }
		public SysUsers CreatedBy { get; set; }
		public Guid WorkTypeId { get; set; }
		public WorkType WorkType { get; set; }
	}
}