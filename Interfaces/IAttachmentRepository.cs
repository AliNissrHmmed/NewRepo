namespace ERP 
{
    public interface IAttachmentRepository:IRepository<Attachment>
{
        Task<Attachment> CreateAttachmentAsync(AttachmentDto attachmentDto);
        public Task gettallattachAsync();
        public Task<Attachment> UpdateAttachmentAsync(UpdateAttachmentDto updateAttachmentDto, Guid attachmentId);
     }
}
