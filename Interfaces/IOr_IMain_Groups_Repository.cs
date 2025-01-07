namespace ERP.PURCHASES.Interfaces
{
    public interface IMain_Groups_Repository : IRepository<Or_Maingroup>
    {

        public  Task UpdateMainGroupAsync(Update_maingroupDto update_maingroupDto, Guid id);

        public Task UpdateSubGroupAsync(UpdateSubgroupDto update_SubgroupDto, Guid id);
    }
}
