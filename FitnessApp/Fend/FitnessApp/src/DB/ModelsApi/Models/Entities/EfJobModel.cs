namespace ModelsApi.Models.Entities
{
    public class EfJobModel
    {
        public long EfJobId { get; set; }
        public EfJob? Job { get; set; }

        public long EfModelId { get; set; }
        public EfModel? Model { get; set; }
    }
}
