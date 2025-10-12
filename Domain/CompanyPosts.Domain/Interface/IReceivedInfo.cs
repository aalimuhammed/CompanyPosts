namespace CompanyPost.Domain.Interface;
public interface IReceivedInfo
{
	public Guid RecievedFromId { get; set; }
	public Publisher RecievedFrom { get; set; }
}