namespace BowlingGame.Presentation.RestAPI.Interfaces
{
    public interface IDto<TModel>
    {
        TModel ToModel();

        void FromModel(TModel model);
    }
}
