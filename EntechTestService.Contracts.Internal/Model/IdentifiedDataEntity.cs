namespace EntechTestService.Contracts.Internal.Model
{
    public class IdentifiedDataEntity<TData>
    {
        public readonly int Id;

        public readonly TData Data;

        public IdentifiedDataEntity(int id, TData data)
        {
            Id = id;
            Data = data;
        }
    }
}
