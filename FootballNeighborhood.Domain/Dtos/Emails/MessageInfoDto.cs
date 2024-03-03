namespace FootballNeighborhood.Domain.Dtos.Emails;

public class MessageInfoDto
{
    public List<string> Tos { get; set; } = default!;
    public List<AttachmentDetailsDto>? Attachments { get; set; } = null;
    public string Title { get; set; } = default!;
    public string Message { get; set; } = default!;
}
