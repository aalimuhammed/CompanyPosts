namespace CompanyPost.Domain.Interface;
public interface IHasSharedProperty
{
	public int SerialNumber { get; set; }
	public Departments Department { get; set; }
}
