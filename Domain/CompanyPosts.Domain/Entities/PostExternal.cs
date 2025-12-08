namespace CompanyPost.Domain.Entities;
public class PostExternal : PostBaseEntity
{
    public ICollection<PostExternalAttachment> Attachments = new List<PostExternalAttachment>();
}