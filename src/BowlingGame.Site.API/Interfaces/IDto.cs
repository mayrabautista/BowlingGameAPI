namespace BowlingGame.API.Interfaces
{
    public interface IDto<TModel>
    {
        TModel ToModel();

        void FromModel(TModel model);
    }
}
